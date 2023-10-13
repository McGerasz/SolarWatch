import React from "react";
import "../Style/Homepage.css"
import Cookies from "js-cookie";

const Layout = () => {
    return (
        <div className="Homepage">
            <div className="Homepagetext">Welcome to SolarWatch</div>
            {Cookies.get("jwtToken") ? (<h1>Try the Sunrise and Sunset feature</h1>) : (<h1>Please login or register</h1>)}
        </div>
            );
  };
  
  export default Layout;