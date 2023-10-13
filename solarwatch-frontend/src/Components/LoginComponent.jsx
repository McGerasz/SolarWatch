import React, { useState } from "react";
import { useNavigate } from "react-router-dom"; 
import Cookies from "js-cookie";
import { APIUrl } from "../Modules/APIUrl";

export default function LoginForm() {
  const navigate = useNavigate(); 
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [loginResponse, setLoginResponse] = useState(null);
  const [jwtToken, setJwtToken] = useState(""); 

  async function login(email, password) {
    var loginCred = {
      Email: email,
      Password: password,
    };

    try {
      const response = await fetch(`${APIUrl}Auth/Login`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(loginCred),
      });

      if (response.ok) {
        response.json().then((data) => {
          const token = data.token;
          Cookies.set("jwtToken", token);
          setJwtToken(token);
          navigate("/solar-watch"); 
        });
      } else {
        setLoginResponse("Invalid username or password"); 
      }
    } catch (err) {
      console.error(err);
    }
  }

  const handleSubmit = (e) => {
    e.preventDefault();
    login(email, password);
  };

  return (
    <>
      <form onSubmit={handleSubmit} className="form-group">
        <label htmlFor="email"><h4>Email:</h4></label>
        <input
          type="text"
          id="email"
          name="email"
          onChange={(e) => setEmail(e.target.value)}
          className="form-control"
        />{" "}
        <br />
        <label htmlFor="password"><h4>Password:</h4></label>
        <input
          type="password"
          id="password"
          name="password"
          onChange={(e) => setPassword(e.target.value)}
          className="form-control"
        />{" "}
        <br />
        <input type="submit" value="Login" class="btn btn-dark" />
      </form>

      
      {loginResponse && (
        <div className="loginerror">
          <p>{loginResponse}</p>
        </div>
      )}
    </>
  );
}