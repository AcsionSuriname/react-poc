import { combineReducers } from "redux";
import componentLogin from "./components/login";
import componentWidgetMessages from "./components/widget-messages";
import componentWidgetText from "./components/widget-text";
import componentNavbar from "./components/navbar";
import user from "./user";

export default combineReducers({ componentLogin, componentWidgetMessages, componentWidgetText, componentNavbar, user });
