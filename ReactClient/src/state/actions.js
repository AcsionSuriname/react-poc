// This app has been designed in a way that you can't have duplicate action types.
// When you create a new action in a component-container.js, add it's name to this file.
// To avoid duplication, let's explicity define all action types here. 
// (Not sure how to make an app that allows for duplicate action types)
import fetch from 'cross-fetch'
import jwt_decode from 'jwt-decode'
import { setTokens, getTokens, delTokens } from '../utils/auth'

export const LOGIN_FETCH_START = "LOGIN_FETCH_START";
export const LOGIN_FETCH_END = "LOGIN_FETCH_END";
export const LOGIN_SUBMIT_START = "LOGIN_SUBMIT_START";
export const LOGIN_SUBMIT_END = "LOGIN_SUBMIT_END";
export const LOGOUT = "LOGOUT";
export const NAVBAR_FETCH_START = "NAVBAR_FETCH_START";
export const NAVBAR_FETCH_END = "NAVBAR_FETCH_END";
export const WIDGET_TEXT_FETCH_START = "WIDGET_TEXT_FETCH_START";
export const WIDGET_TEXT_FETCH_END = "WIDGET_TEXT_FETCH_END";
export const USER_UPDATE = "USER_UPDATE";
export const USER_IS_LOGGED_IN = "USER_IS_LOGGED_IN";
export const MESSAGES_FETCH_START = "MESSAGES_FETCH_START";
export const MESSAGES_FETCH_END = "MESSAGES_FETCH_END";


export function navbarFetch() {
  return dispatch => { dispatch({ type: NAVBAR_FETCH_START }); }
}

export function widgetMessagesFetch(user) {

  return dispatch => {

    // Change the state to loading
    dispatch({ type: MESSAGES_FETCH_START });

    // Get the jwt and user id
    let tokens = getTokens();
    let userid = (user && user.id) ? user.id : "unknown";

    // Query the API then update the state with its response
    return fetch('https://localhost:44385/api/message/' + userid,
      {
        method: "get",
        headers: {
          'Authorization': 'Bearer ' + tokens.userToken,
          'Accept': 'application/json',
          'Content-Type': 'application/json'
        }
      })
      .then(response => {
        if (response.status >= 400) {
          return {};
        }
        return response.json();
      })
      .then(json => {
        if (json) {
          dispatch({ type: MESSAGES_FETCH_END, payload: json })
        } else {
          dispatch({ type: MESSAGES_FETCH_END, payload: {} })
        }
      });
  }
}

export function widgetTextFetch(ids) {
  return dispatch => {

    // Change the state to loading
    dispatch({ type: WIDGET_TEXT_FETCH_START });

    // The component should know what the ids are (TODO: remove ids page??)
    let qparams = (!!ids && !!ids.trim()) ? ('?ids=' + (Array.isArray(ids) ? ids.join() : ids)) : '';

    // Query the API then update the state with its response
    return fetch('https://localhost:44385/api/content' + qparams)
      .then(response => {
        if (response.status >= 400) {
          return {};
        }
        return response.json();
      })
      .then(json => {
        if (json && json.length > 0) {
          dispatch({ type: WIDGET_TEXT_FETCH_END, payload: json[0] })
        } else {
          dispatch({ type: WIDGET_TEXT_FETCH_END, payload: {} })
        }
      });
  }
}

export function loginFetch(ids) {
  return dispatch => {

    // Change the state to loading
    dispatch({ type: LOGIN_FETCH_START });

    // The component should know what the ids are (TODO: remove ids page??)
    let qparams = (!!ids && !!ids.trim()) ? ('?ids=' + (Array.isArray(ids) ? ids.join() : ids)) : '';

    // Query the API then update the state with its response
    return fetch('https://localhost:44385/api/content' + qparams)
      .then(response => {
        if (response.status >= 400) {
          return {};
        }

        // convert the json response to values
        return response.json();
      })
      .then(json => {
        if (json && json.length > 0) {
          dispatch({
            type: LOGIN_FETCH_END,
            payload: {
              image: 'https://localhost:44385' + json[0].image,
              label_username: json[1].title,
              error_username: json[1].body,
              label_password: json[2].title,
              error_password: json[2].body,
              btn_login: json[4].title,
            }
          });
        } else {
          dispatch({ type: LOGIN_FETCH_END, payload: {} });
        }
      });
  }
}

export function login(username, password) {

  return dispatch => {

    // Change the state to loading
    dispatch({ type: LOGIN_SUBMIT_START });


    // Query the API then update the state with its response
    return fetch('https://localhost:44385/authentication/authenticate',
      {
        method: "post",
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json'
        },

        //make sure to serialize your JSON body
        body: JSON.stringify({
          username: username,
          password: password
        })
      })
      .then(response => {
        // handle errors
        if (response.status >= 400) {
          return response.json();
        }

        // convert the json response to values
        return response.json();
      })
      .then(json => {
        if (json && json.token) {

          // store the tokens to localstorage
          setTokens(json.token, json.refreshToken);

          // get the user info from the jwt token.
          let decoded = jwt_decode(json.token);
          let decodedUser = {
            id: json.userGID,
            email: "",
            firstName: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"],
            lastName: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname"],
            userName: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"],
            preferredLanguage: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/locality"]
          };

          // update the user
          dispatch({
            type: USER_UPDATE,
            payload: decodedUser
          });

          // finish the login process
          dispatch({
            type: LOGIN_SUBMIT_END,
            payload: { success: true }
          });
        } else {
          dispatch({
            type: LOGIN_SUBMIT_END,
            payload: {
              error_login: (json && json.errors && json.errors.length > 0) ? json.errors[0] : "unknown api error",
              success: false
            }
          });
        }
      });
  }
}

export function silentLogin() {

  return dispatch => {

    // Get the jwt
    let tokens = getTokens();

    // Query the API then update the state with its response
    return fetch('https://localhost:44385/authentication/silent',
      {
        method: "get",
        headers: {
          'Authorization': 'Bearer ' + tokens.refreshToken,
          'Accept': 'application/json',
          'Content-Type': 'application/json'
        }
      })
      .then(response => {
        // handle errors
        if (response.status >= 400) {
          return null;
        }

        // convert the json response to values
        return response.json();
      })
      .then(json => {
        if (json && json.token) {

          // store the tokens to localstorage
          setTokens(json.token, json.refreshToken);

          // get the user info from the jwt token.
          let decoded = jwt_decode(json.token);
          let decodedUser = {
            id: json.userGID,
            email: "",
            firstName: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"],
            lastName: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname"],
            userName: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"],
            preferredLanguage: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/locality"]
          };

          // update the user
          dispatch({
            type: USER_UPDATE,
            payload: decodedUser
          });

        } else {

          // update the user
          dispatch({
            type: USER_UPDATE,
            payload: {}
          });

        }
      });
  }
}

export function logout() {

  return dispatch => {

    // Delete the jwt from local storage
    delTokens();

    // update the user to nothing
    dispatch({
      type: USER_UPDATE,
      payload: {}
    });
  }
}

