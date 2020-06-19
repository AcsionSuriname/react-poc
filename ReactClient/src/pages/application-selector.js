import React  from "react"
import { Link } from "gatsby"

import Layout from "../components/layout"
import SEO from "../components/seo"

const ApplicationSelector = () => (<Layout>
  <div className="modal-dialog modal-dialog-centered">
    <SEO title="Login page" />
    <div className="modal-content">
      <div className="modal-header">
        <h4 className="modal-title applications-title">Applications</h4>
      </div>
      <div className="modal-body">
        <ul className="applications-list">
          <li className="list-group-item"><Link to="/page-2/">Care Groups</Link></li>
          <li className="list-group-item"><Link to="/page-2/">Care Providers</Link></li>
          <hr />
          <li className="list-group-item"><Link to="/page-2/">Population Health</Link></li>
          <li className="list-group-item"><Link to="/page-2/">Configurations</Link></li>
          <li className="list-group-item"><Link to="/page-2/">Health Insurers</Link></li>
          <li className="list-group-item"><Link to="/page-2/">Patient Portal</Link></li>
        </ul>
      </div>
    </div>
  </div>
</Layout>);

export default ApplicationSelector;
