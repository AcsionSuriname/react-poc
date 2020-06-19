import React from "react"
import { faHome, faSignOutAlt, faMapMarkerAlt, faEnvelope, faTasks, faUser } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Link, useStaticQuery, graphql } from "gatsby"
import Img from "gatsby-image"
import './style.css';

// TODO: rewrite view code to a menu that allows for multiple levels
var buildMenu = (output, menuItems) => {
  // make sure that the menu has items
  if (menuItems && menuItems.length > 0) {

    // loop through the items
    for (let item of menuItems) {
      if (item && item.name) {

        if (item.menuItems && item.menuItems.length > 0) {

          // build an array of the dropdown menu items
          let dropdownMenu = [];
          for (let subItem of item.menuItems) {
            if (subItem.name) {
              dropdownMenu.push(<Link key={subItem.id} to={subItem.to} className="nav-link" >{subItem.name}</Link>);
            }
          }

          // create a dropdown that is one level deep
          output.push(
            <li key={item.id} className="nav-item dropdown">
              <Link to={item.to} className="nav-link dropdown-toggle" role="button">{item.name}</Link>
              <div className="dropdown-menu">{dropdownMenu}</div>
            </li>);

        } else if (item.to) {
          // menu item with a link
          output.push(<li key={item.id} className="nav-item"><Link to={item.to} className="nav-link">{item.name}</Link></li>);
        } else {
          // menu item without a link
          output.push(<li key={item.id} className="nav-item">{item.name}</li>);
        }

      }
    }    
  }
  return (<ul className="navbar-nav mr-auto">{output}</ul>);
}

const NavbarView = ({ componentNavbar, user, componentWidgetMessages, handleLogout }) => {
  const data = useStaticQuery(graphql`
    query {
      placeholderImage: file(relativePath: { eq: "logo-curacao.png" }) {
        childImageSharp {
          fixed(width: 80, height: 40) {
            ...GatsbyImageSharpFixed
          }
        }
      }
    }
  `);

  // Define the items to the left of the menu
  let navbarLeftItems = [];
  navbarLeftItems.push(<Img key="left-0" fixed={data.placeholderImage.childImageSharp.fixed} />);
  navbarLeftItems.push(<Link key="left-1" to="/application-selector" className="navbar-brand home-icon">
    <FontAwesomeIcon icon={faHome} size="lg" /></Link>);

  // Define the menu
  let menuItems = buildMenu([], componentNavbar.menuItems);

  // Define the items to the right of the menu
  let navbarRightItems = [];
  navbarRightItems.push(<Link key="right-0" to="/application-selector" className="nav-link" ><FontAwesomeIcon icon={faUser} />{user.firstName} {user.lastName}</Link>);
  navbarRightItems.push(<span key="right-1" className="right-text"><FontAwesomeIcon icon={faMapMarkerAlt} /></span>);
  navbarRightItems.push(<span key="right-2" className="right-text"><FontAwesomeIcon icon={faEnvelope} /> ({componentWidgetMessages.unreadCount})</span>);
  navbarRightItems.push(<button key="right-3" type="button" className="btn btn-link nav-link" onClick={(event) => handleLogout(event)}>
    <span className="right-text"><FontAwesomeIcon icon={faSignOutAlt} />Logout</span></button>);
  navbarRightItems.push(<span key="right-4" className="right-text"><FontAwesomeIcon icon={faTasks} /></span>);

  return (
    <nav className="navbar fixed-top navbar-expand-lg navbar-light bg-light">
      {navbarLeftItems}
      <div className="collapse navbar-collapse" id="navbarSupportedContent">
        {menuItems}
        {navbarRightItems}
      </div>
    </nav>
  );
};

export default NavbarView
