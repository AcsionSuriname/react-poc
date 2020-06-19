import React, { Component } from 'react'
import { connect } from 'react-redux'
import { navigate } from "gatsby"
import LayoutView from './layout-view.js'
import { silentLogin } from '../../state/actions'
import { setTokens, getTokens, delTokens } from '../../utils/auth'

class Layout extends Component {

  constructor(props) {
    super(props);

    // Get the user data
    let tokens = getTokens();
    let logged_in = (tokens && tokens.userToken && this.props.user.id);

    // Silent login or if redirect to login page
    if (!logged_in && tokens && tokens.userToken)
      this.props.silentLogin();
    else if (!logged_in)
      navigate('/');

    this.state = {
      logged_in: logged_in,
      fetching: logged_in
    };
  }

  componentDidMount() {

  }

  componentDidUpdate(prevProps) {
    if (this.props.user.id && !prevProps.user.id) {
      // Get the user data
      let tokens = getTokens();
      let logged_in = (tokens && tokens.userToken && this.props.user.id);

      // Silent login or if redirect to login page
      if (!logged_in && tokens && tokens.userToken)
        this.props.silentLogin();
      else if (!logged_in)
        navigate('/');

      this.setState({
        logged_in: logged_in
      });
    }

  }


  render() {
    // only return the layout if logged in
    if (this.state.logged_in)
      return <LayoutView> { this.props.children }</LayoutView>

    return null;
  }
}

function mapStateToProps(state) {
  const { user } = state;
  return { user }
}

const mapDispatchToProps = dispatch => {
  return {
    silentLogin: () => dispatch(silentLogin())
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(Layout)
