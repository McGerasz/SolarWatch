import React from 'react';
import ReactDOM from 'react-dom/client';
import {BrowserRouter, createBrowserRouter, RouterProvider, Route} from 'react-router-dom';
import reportWebVitals from './reportWebVitals';
import Layout from './Pages/Layout';
import Homepage from './Pages/Homepage'
import LoginPage from './Pages/LoginPage'
import RegisterPage from './Pages/RegisterPage';
import SolarWatchPage from './Pages/SolarWatchPage';


const router = createBrowserRouter([
  {
    path: "/",
    element: <Layout />,
    children: [
      {path: "/",
      element: <Homepage />},
      {
        path: "/login",
        element: <LoginPage />
      },
      {
        path: "register",
        element: <RegisterPage />
      },{
        path: "/solar-watch",
        element: <SolarWatchPage />
      }
    ]
  }
]);

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <RouterProvider router={router}/>
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
