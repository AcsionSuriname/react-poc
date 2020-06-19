import React from "react"
import PropTypes from "prop-types"
import "./style.css"
import Navbar from "../navbar"

const LayoutView = ({ children }) => {
  return (
    <>
      <Navbar />
      <div
        style={{
          margin: `0 auto`,
          maxWidth: 960,
          padding: `40px 1.0875rem 1.45rem`,
        }}
      >
        <main>{children}</main>
      </div>
    </>
  )
}

LayoutView.propTypes = {
  children: PropTypes.node.isRequired,
}

export default LayoutView
