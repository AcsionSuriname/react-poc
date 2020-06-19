import React, { Component } from 'react'
import './style.css';

export default class LoginView extends Component {

  constructor(props) {
    super(props);

    this.state = {
      error_ui: false,
      username_input: "",
      password_input: "",
    };

    this.errorClassNames = this.errorClassNames.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    this.handleInputChange = this.handleInputChange.bind(this);
  }

  handleSubmit(event) {
    event.preventDefault();
    if (this.state.username_input && this.state.password_input) {
      this.props.handleSubmit(this.state.username_input, this.state.password_input);
    } else {
      this.setState({
        error_ui: true
      });
    }
  }

  handleInputChange(event) {
    const value = event.target.value
    const name = event.target.name
    this.setState({
      [name]: value,
    });
  }

  errorClassNames(name) {
    let classes = [];
    classes.push("text-danger");

    // Hide the span if the input is not empty
    if ((!this.state.error_ui && !this.props.componentLogin.error_login)
      || (name && !!this.state[name])
      || (!name && !this.props.componentLogin.error_login))
      classes.push("hidden");

    return classes.join(' ');
  }

  render() {

    // generate the classess
    return (
      <form id="login-form" method="post" action="" >
        <div className="form-group">
          <img src={this.props.componentLogin.image} className="logo" alt="logo" width="400" />
        </div>
        <div className="form-group">
          <p><span className={this.errorClassNames()}>{this.props.componentLogin.error_login}</span></p>
        </div>
        <div className="form-group">
          <label htmlFor="username_input">{this.props.componentLogin.label_username}</label>
          <input id="username_input" name="username_input" type="text"
            onChange={this.handleInputChange}
            className="form-control" aria-invalid="true" aria-describedby="username_error" />
          <span id="username_error" className={this.errorClassNames("username_input")}>{this.props.componentLogin.error_username}</span>
        </div>
        <div className="form-group">
          <label htmlFor="password_input">{this.props.componentLogin.label_password}</label>
          <input name="password_input" type="text" className="form-control"
            onChange={this.handleInputChange}
            aria-invalid="true" aria-describedby="password_error" />
          <span id="password_error" className={this.errorClassNames("password_input")}>{this.props.componentLogin.error_password}</span>
        </div>
        <div className="form-group">
          <button type="submit"
            onClick={(event) => this.handleSubmit(event)}
            className="btn btn-primary">{this.props.componentLogin.btn_login}</button>
        </div>
      </form>
    );

  }
}