import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { APIUrl } from "../Modules/APIUrl";
import { Alert } from "bootstrap";

export default function RegisterComponent() {
    const navigate = useNavigate();
    const [email, setEmail] = useState("");
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [registerResponse, setRegisterResponse] = useState(null); 
    

    function SendRegister(email, username, password)
    {
        var regData = {
            "Email": email,
            "UserName": username,
            "Password": password
        }

        try {
          
            const response = fetch(`${APIUrl}Auth/Register`, {
              method: "POST",
              headers: {
                "Content-Type": "application/json",
              },
              body: JSON.stringify(regData),
            });
          
            response
              .then((result) => {
                if (result.ok) {
                    Alert("Successfully registered")
                    navigate("/login");
                } else {
                  setRegisterResponse("Error during register!");
                }
              })
              .catch((error) => {
                console.error(error);
              });
          } catch (error) {
            console.error(error);
          }
    }
    const handleSubmit = (e) => {
        e.preventDefault();
        SendRegister(email, username, password);
    }
    return(
        <>
        <form onSubmit={handleSubmit} className="form-group">
        <h1 className="headtext rounded">Register</h1><br /><br />
        <label htmlFor="email"><h4>E-mail:</h4></label>
        <input type="text" id="email" name="email" onChange={(e) => setEmail(e.target.value)} className="form-control"/><br />
        <label htmlFor="username"><h4>Username:</h4></label>
        <input type="text" id="username" name="username" onChange={(e) => setUsername(e.target.value)} className="form-control"/><br />
        <label htmlFor="password"><h4>Password:</h4></label>
        <input type="password" id="password" name="password" onChange={(e) => setPassword(e.target.value)} className="form-control"/><br />
        <input type="submit" value="Register" className="btn btn-dark"></input>
        {registerResponse && (
        <div className="registererror">
          <br /><h3>{registerResponse}</h3>
          <h4>Please try again</h4>
        </div>
      )}
        </form>

        </>
    )



}