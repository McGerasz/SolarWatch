## About The Project

SolarWatch is project that provides the user the ability to get information about sunrise and sunset times on a specified location at a specified time. </br>
The app features an authentication system and stores information on a database via a REST API.</br>
</br>
There is an alternative PostgreSQL version available in the Backend/PostgreSQL-version branch. I made it because the services I used to deploy the application provided PostgreSQL databases.</br>
Unfortunately the database providers only provided a certain amount of time to use them and since then it has ran out. That version will not work without additional configuration as it was specifacally made to be deployed!


### Built With

* C#
* ASP.NET Core
* Entity Framework Core
* NUnit
* JavaScript
* HTML/CSS
* ReactJs
* MSSQL
* Docker
* Github Workflows




### Prerequisites

Make sure Docker is installed</br>
Get an API Key from the following website: https://openweathermap.org/api/geocoding-api

### Installation

1. Clone the repository
2. Enter your Geolocator API Key into the docker compose file found in SolarWatch/SolarWatch and run it with `docker-compose up --build`
3. Access the frontend page at http://localhost:3000/

