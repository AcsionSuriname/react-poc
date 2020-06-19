import React, { Component } from 'react'
import { connect } from 'react-redux'
import TextView from './text-view'
import { widgetTextFetch } from '../../state/actions'

// This should hold componentdidmount api call
// This should hold references to the actions
// This should hold the filter


class WidgetText extends Component {

    componentDidMount() {
        this.props.fetch(this.props.id);
    }

    componentDidUpdate(prevProps) {
        //if (this.props.selectedSubreddit !== prevProps.selectedSubreddit) {
        //    const { dispatch, selectedSubreddit } = this.props
        //    dispatch(fetchPostsIfNeeded(selectedSubreddit))
        //}
    }

    render() {
        return <TextView componentWidgetText={this.props.componentWidgetText} />
    }
}

function mapStateToProps(state) {
    const { componentWidgetText } = state;
    return { componentWidgetText }
}

const mapDispatchToProps = dispatch => {
    return {
        fetch: (ids) => dispatch(widgetTextFetch(ids))
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(WidgetText)