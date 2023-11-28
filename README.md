## About The Project

SolarWatch is project that provides the user the ability to get information about sunrise and sunset times on a specified location at a specified time. </br>
The app features an authentication system and stores information on a database via a REST API.</br>
</br>
There is an alternative PostgreSQL version available in the Backend/PostgreSQL-version branch. I made it because the services I used to deploy the application provided PostgreSQL databases.</br>
Unfortunately the database providers only provided a certain amount of time to use them and since then it has ran out.


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
2. Run the Docker Compose file with the following environmental variables set:</br>
ASPNETCORE_CONNECTIONSTRING="Server=mssql,1433;Database=SolarWatchApi;User Id=sa;Password=/yourDatabasePassword/;trustServerCertificate=true;"</br>
ASPNETCORE_GEOLOCATORAPIKEY="/yourAPIKey/"</br>
DATABASE_PASSWORD="/thePasswordYouProvidedInTheConnectionString/"</br>
ASPNETCORE_VALIDISSUER="apiWithAuthToBackend"</br>
ASPNETCORE_VALIDAUDIENCE="apiWithAuthToBackend"</br>
ASPNETCORE_ISSUERSIGNINGKEY="/yourIssuerSigningKey/"</br>
ASPNETCORE_ADMINEMAIL="/anEmailToLoginWith/"</br>
ASPNETCORE_ADMINPASSWORD="/aPasswordToLogInWith/"</br>
3. Access the frontend page at http://localhost:3000/

