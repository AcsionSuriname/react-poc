import React from "react"

import Login from "../components/login"
import WidgetText from "../components/widget-text"
import SEO from "../components/seo"

const LoginPage = () => (
    <div className="row">
        <SEO title="Login page" />
        <div className="col-md">
            <WidgetText id="1" className="something" />
        </div>
        <div className="col-md">
            <Login ids="2,3,4,5,6" />
        </div>
    </div>
)

export default LoginPage
