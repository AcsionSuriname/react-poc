using APIServer.Models;
using APIServer.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.Helpers
{
    public static class AuthenticationHelper
    {
        public static int REFRESHTOKEN_DURATIONDAYS = 7;
        public static string REFRESHTOKEN_CLAIMNAME = "cc4d9c4c-bccb-4cc9-951a-d5824f2ebcaa";

        public static bool canAccessUserData(this ClaimsPrincipal user, String username = null)
        {
            // Each user has access to his own data (compare the provided username with the logged in user's claims)
            bool accessingOwnData = !String.IsNullOrEmpty(username) && username.Equals(user?.FindFirst(ClaimTypes.Name)?.Value);
            
            // Admin users have access to all users' data (check if the logged in user has admin role in their claims)
            bool isAdmin = AspNetUserClaim.ROLE.ADMIN.ToString().Equals(user?.FindFirst(ClaimTypes.Role)?.Value);

            return accessingOwnData || isAdmin;
        }

        public static AuthenticationResponse LoginResponse(IConfiguration config, LoginUser userTryingToLogin, User userInDB, string RemoteIpAddress)
        {

            // TODO: I don't know how INCA handles password encryption, so let's make our own:
            string passwordHash = HashPassword(userInDB);

            IPasswordHasher<LoginUser> passwordHasher = new PasswordHasher<LoginUser>();
            if (!String.IsNullOrEmpty(userTryingToLogin?.UserName) && !String.IsNullOrEmpty(userTryingToLogin?.Password) &&
                passwordHasher.VerifyHashedPassword(userTryingToLogin, passwordHash, userTryingToLogin.Password) == PasswordVerificationResult.Success)
            {
                return LoginResponse(config, userInDB, RemoteIpAddress);
            }

            return null;
        }

        public static AuthenticationResponse LoginResponse(IConfiguration config, User userInDB, string RemoteIpAddress)
        {
            //Create a jwt token (will be used to make requests)
            var jwtToken = CreateSessionToken(config, userInDB);

            // Create a refresh token (will be used to update the jwt token)
            // Technically, we don't need this, but eventually we might split auth server from resource server, which is when it makes sense.
            var refreshToken = CreateRefreshToken(config, RemoteIpAddress, userInDB.UserName);

            return new AuthenticationResponse
            {
                UserGID = userInDB.UserGID,
                UserName = userInDB.UserName,
                Token = jwtToken,
                RefreshToken = refreshToken
            };
        }

        public static string CreateSessionToken(IConfiguration config, User userInDB)
        {
            // Create a set of Claim objects to assign to the user
            HashSet<Claim> claimsSet = new HashSet<Claim>();

            // Always include username, given name, surname and role 
            if (!userInDB.AspNetUserClaims.Any(x => x.ClaimType == ClaimTypes.Name))
            {
                claimsSet.Add(new Claim(ClaimTypes.Name, userInDB.UserName ?? ""));
            }
            if (!userInDB.AspNetUserClaims.Any(x => x.ClaimType == ClaimTypes.GivenName))
            {
                claimsSet.Add(new Claim(ClaimTypes.GivenName, userInDB.FirstName ?? ""));
            }
            if (!userInDB.AspNetUserClaims.Any(x => x.ClaimType == ClaimTypes.Surname))
            {
                claimsSet.Add(new Claim(ClaimTypes.Surname, userInDB.LastName ?? ""));
            }
            if (!userInDB.AspNetUserClaims.Any(x => x.ClaimType == ClaimTypes.Locality))
            {
                claimsSet.Add(new Claim(ClaimTypes.Locality, userInDB.PreferredLanguage ?? ""));
            }
            if (!userInDB.AspNetUserClaims.Any(x => x.ClaimType == ClaimTypes.Role))
            {
                claimsSet.Add(new Claim(ClaimTypes.Role, AspNetUserClaim.ROLE.DEFAULT.ToString()));
            }

            // Add the claims stored in the db
            userInDB.AspNetUserClaims.ToList().ForEach(x => claimsSet.Add(new Claim(x.ClaimType, x.ClaimValue)));

            var claims = claimsSet.ToArray();

            //Read signing symmetric key
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config.GetSection("JWT:SigningKey").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Create a JWT token
            var jwtToken = new JwtSecurityToken(
                issuer: config.GetSection("JWT:Issuer").Value,
                audience: config.GetSection("JWT:Audience").Value,
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        public static string CreateRefreshToken(IConfiguration config, string RemoteIpAddress, string userName)
        {
            if (String.IsNullOrEmpty(RemoteIpAddress))
                return null;

            // Refresh tokens will stick around for a while, thus should contain no identifying information.
            var claims = new Claim[] { 
                            new Claim(REFRESHTOKEN_CLAIMNAME, RemoteIpAddress), 
                            new Claim(ClaimTypes.Name, userName) };

            //Read signing symmetric key
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config.GetSection("JWT:SigningKey").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create the token (only claim is the originating address)
            var refreshToken = new JwtSecurityToken(
                issuer: config.GetSection("JWT:Issuer").Value,
                audience: config.GetSection("JWT:Audience").Value,
                claims: claims,
                expires: DateTime.Now.AddDays(REFRESHTOKEN_DURATIONDAYS),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(refreshToken);
        }

        public static string HashPassword(User userInDB) {

            if (String.IsNullOrEmpty(userInDB?.UserName) || String.IsNullOrEmpty(userInDB?.NormalizedUserName))
                return null;

            LoginUser user = new LoginUser()
            {
                UserName = userInDB?.UserName,
                Password = userInDB?.NormalizedUserName
            };

            IPasswordHasher<LoginUser> passwordHasher = new PasswordHasher<LoginUser>();
            string hashedPassword = passwordHasher.HashPassword(user, user.Password);

            return hashedPassword;
        }

        public static JwtBearerOptions ProofOfConceptJwtBearerOptions(this JwtBearerOptions options, IConfiguration config)
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = config.GetSection("JWT:Issuer").Value,
                ValidAudience = config.GetSection("JWT:Audience").Value,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config.GetSection("JWT:SigningKey").Value))
            };

            return options;
        }

    }
}
