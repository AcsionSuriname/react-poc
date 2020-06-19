import React, { Component } from 'react'
import { connect } from 'react-redux'
import { navigate } from "gatsby"
import LoginView from './login-view.js'
import { login, loginFetch } from '../../state/actions'

class Login extends Component {

  constructor(props) {
    super(props);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  componentDidMount() {
    this.props.fetch(this.props.ids);
  }

  componentDidUpdate(prevProps) {
    // provide user (1) we stopped fetching data and the (2) the login was successful
    if ((!this.props.componentLogin.fetching && prevProps.componentLogin.fetching)
      && this.props.componentLogin.success) {
      navigate('/application-selector');
    }
  }

  handleSubmit(username, password) {
    this.props.login(username, password);
  }

  render() {
    return <LoginView componentLogin={this.props.componentLogin}
      handleSubmit={this.handleSubmit} />
  }
}

function mapStateToProps(state) {
  const { componentLogin, user } = state;
  return { componentLogin, user }
}

const mapDispatchToProps = dispatch => {
  return {
    fetch: (ids) => dispatch(loginFetch(ids)),
    login: (username, password) => dispatch(login(username, password))
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(Login)