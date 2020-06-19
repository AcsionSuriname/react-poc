import React, { Component } from 'react'
import { connect } from 'react-redux'
import MessagesView from './messages-view'
import { widgetMessagesFetch } from '../../state/actions'

// This should hold componentdidmount api call
// This should hold references to the actions
// This should hold the filter


class WidgetMessages extends Component {
    constructor(props) {
        super(props)
    }

    componentDidMount() {
        //const { dispatch, selectedSubreddit } = this.props
        //dispatch(fetchPostsIfNeeded(selectedSubreddit))
    }

    componentDidUpdate(prevProps) {
        //if (this.props.selectedSubreddit !== prevProps.selectedSubreddit) {
        //    const { dispatch, selectedSubreddit } = this.props
        //    dispatch(fetchPostsIfNeeded(selectedSubreddit))
        //}
    }

    render() {
        return <MessagesView />
    }
}

function mapStateToProps(state) {
    const { componentWidgetMessages } = state;

    return { componentWidgetMessages }
}

export default connect(mapStateToProps)(WidgetMessages)