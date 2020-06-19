import React, { Component } from 'react'
import { connect } from 'react-redux'
import { navigate } from "gatsby"
import NavbarView from './navbar-view'
import { navbarFetch, logout, widgetMessagesFetch } from '../../state/actions'

class Navbar extends Component {

  constructor(props) {
    super(props);
    this.handleLogout = this.handleLogout.bind(this);
  }

  componentDidMount() {
    this.props.fetch(this.props.user);
    this.props.fetchMessage(this.props.user);
  }

  handleLogout(event) {
    this.props.logout();
    navigate('/');
  }

  render() {
    return <NavbarView componentNavbar={this.props.componentNavbar} user={this.props.user}
      componentWidgetMessages={this.props.componentWidgetMessages}
      handleLogout={this.handleLogout} />
  }
}

function mapStateToProps(state) {
  const { componentNavbar, user, componentWidgetMessages } = state;
  return { componentNavbar, user, componentWidgetMessages }
}

const mapDispatchToProps = dispatch => {
  return {
    fetch: (user) => dispatch(navbarFetch(user)),
    logout: () => dispatch(logout()),
    fetchMessage: (user) => dispatch(widgetMessagesFetch(user))
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(Navbar)