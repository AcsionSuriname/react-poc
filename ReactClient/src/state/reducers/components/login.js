import {
  LOGIN_FETCH_START,
  LOGIN_FETCH_END,
  LOGIN_SUBMIT_START,
  LOGIN_SUBMIT_END
} from '../../actions'

const initialState = {
  fetching: false,
  image: null,
  label_username: null,
  error_username: null,
  label_password: null,
  error_password: null,
  btn_login: null,
  error_login: null,
  success: false
};

export default function componentLogin(state = initialState, action) {
  switch (action.type) {
    case LOGIN_FETCH_START:
    case LOGIN_SUBMIT_START:
      return {
        ...state,
        fetching: true
      };
    case LOGIN_FETCH_END:
      return {
        ...state,
        fetching: false,
        image: action.payload.image,
        label_username: action.payload.label_username,
        error_username: action.payload.error_username,
        label_password: action.payload.label_password,
        error_password: action.payload.error_password,
        btn_login: action.payload.btn_login,
        success: false,
      };
    case LOGIN_SUBMIT_END:
      return {
        ...state,
        fetching: false,
        error_login: action.payload.error_login,
        success: action.payload.success
      };
    default:
      return state;
  }
};
