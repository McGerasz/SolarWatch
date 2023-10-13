import { Link } from 'react-router-dom';
import Container from "react-bootstrap/Container";
import Nav from "react-bootstrap/Nav";
import Navbar from "react-bootstrap/Navbar";
import "bootstrap/dist/css/bootstrap.css";
import React, { useState, useEffect } from "react";
import Cookies from "js-cookie";
import '../Style/Navbar.css'

export default function NavigationBar() {
  const [jwtToken, setJwtToken] = useState("");

  useEffect(() => {
    const jwtToken = Cookies.get("jwtToken");
    
    setJwtToken(jwtToken);
  }, []);

 

  const handleLogout = () => {
    Cookies.remove("jwtToken");
    
    setJwtToken(""); 
  }

  return (
    <Navbar
      bg='dark'
      variant="dark"
      expand="lg"
      className='Navbar'
    >
      <Container>
        <Navbar.Toggle
          aria-controls="basic-navbar-nav"
          className="ml-auto"
        />
        <Navbar.Collapse id="basic-navbar-nav" className="justify-content-center">
          <Nav>
          <Nav.Item>
              <Link to="/" className='linkText'>
              Home&nbsp;&nbsp;&nbsp;&nbsp;|
              </Link>
            </Nav.Item>
            <Nav.Item>
              <Link to="/register" className='linkText'>
              &nbsp;&nbsp;&nbsp;&nbsp;Register&nbsp;&nbsp;&nbsp;&nbsp;|
              </Link>
            </Nav.Item>
              <Nav.Item>
                <Link to="/login" className='linkText'>
                &nbsp;&nbsp;&nbsp;&nbsp;Login&nbsp;&nbsp;&nbsp;&nbsp;|
                </Link>
              </Nav.Item>
              <Nav.Item>
                <Link to="/logout" className='linkText' onClick={handleLogout}>
                &nbsp;&nbsp;&nbsp;&nbsp;Logout
                </Link>
              </Nav.Item>
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
}