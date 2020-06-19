const isBrowser = typeof window !== `undefined`;

export const setTokens = (userToken, refreshToken) => {
  // this function is only useful on client-side
  if (isBrowser) {
    window.localStorage.setItem('userToken', userToken);
    window.localStorage.setItem('refreshToken', refreshToken);
  }
}

export const getTokens = () => {
  // this function is only useful on client-side
  if (!isBrowser) return null;

  return {
    userToken: window.localStorage.getItem('userToken'),
    refreshToken: window.localStorage.getItem('refreshToken')
  };
}

export const delTokens = () => {
  // this function is only useful on client-side
  if (isBrowser) {
    window.localStorage.removeItem('userToken');
    window.localStorage.removeItem('refreshToken');
  }
}
