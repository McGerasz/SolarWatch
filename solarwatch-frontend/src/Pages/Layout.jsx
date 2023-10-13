import React from "react";
import { Outlet, Link } from "react-router-dom";
import Navbar from "../Components/Navbar.jsx"


const Layout = () => {
  return (

    <div className="Layout">
        <Navbar/>
        <Outlet />
    </div>
  );
};

export default Layout;