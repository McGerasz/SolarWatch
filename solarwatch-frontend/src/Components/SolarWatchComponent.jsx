import React, { useEffect, useState } from 'react';
import Cookies from 'js-cookie';
import { useNavigate } from "react-router-dom"; 
import { APIUrl } from '../Modules/APIUrl';


export default function SolarWatch() {
  const navigate = useNavigate();
  const [city, setCity] = useState("");
  const [date, setDate] = useState("");
  const [bearer, setBearer] = useState("");
  const [responseData, setResponseData] = useState("");

  useEffect(() => {
    const jwtToken = Cookies.get("jwtToken");
    if (!jwtToken) {
      console.error("JWT token was not found.");
      navigate("/login");
    }
    setBearer("Bearer " + jwtToken);
  }, []);

  async function fetchData() {
    try {
      const response = await fetch(`${APIUrl}api/SolarWatch/get?cityName=${city}&date=${date}`, {
        method: "GET",
        mode: 'cors',
        headers: {
          "Authorization": bearer,
          "Content-Type": "application/json"
        }
      });

      if (!response.ok) {
        throw new Error('Request failed with status ' + response.status);
      }

      const data = await response.json();
      setResponseData(data);
    } catch (error) {
      console.error(error);
    }
  }

  const handleSubmit = async (e) => {
    e.preventDefault();
    await fetchData();
  }

  return (
    <>
      <form onSubmit={handleSubmit} className="form-group">
        <label htmlFor="cityName"><h3>City:</h3></label> <br />
        <input
          type="text"
          id="city"
          name="city"
          placeholder="Please enter a city's name"
          onChange={(e) => setCity(e.target.value)}
          className="form-control"
        /><br />
        <label htmlFor="date"><h3>Date:</h3></label> <br />
        <input
          type="date"
          id="date"
          name="date"
          onChange={(e) => setDate(e.target.value)}
          className="form-control"
        /><br /><br /><br />
        <input type="submit" value="Get sunrise and sunset time" className="btn btn-light"/>
        <br /><br /><br /><br /><br />
      {responseData.sunriseTime && responseData.sunsetTime && (<div className='citydata'>
      <h2>Sunrise: {responseData.sunriseTime}</h2>
      <h2>Sunset: {responseData.sunsetTime}</h2>
      </div>)}
      </form>
    </>
  );
}