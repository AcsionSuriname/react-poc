import {
  USER_UPDATE,
  USER_IS_LOGGED_IN
} from '../actions'

const initialState = {
  id: "",
  email: "",
  firstName: "",
  lastName: "",
  userName: "",
  preferredLanguage: "",
  isLoggedIn: false
};

export default function user(state = initialState, action) {
  switch (action.type) {
    case USER_UPDATE:
      return {
        ...state,
        id: action.payload.id,
        email: action.payload.email,
        firstName: action.payload.firstName,
        lastName: action.payload.lastName,
        userName: action.payload.userName,
        preferredLanguage: action.payload.preferredLanguage,
        isLoggedIn: !!action.payload.id
      };
    case USER_IS_LOGGED_IN:
      return {
        ...state,
        isLoggedIn: action.payload.isLoggedIn && !!state.id
      };
    default:
      return state;
  }
};
