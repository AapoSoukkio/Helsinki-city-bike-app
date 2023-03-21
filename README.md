# Project: Helsinki-city-bike-app

Made by: Aapo Soukkio

***

## Purpose of this project

This is my solution for the pre-assignment for Solita Dev Academy Finland 2023

You can find the original assignment here --> https://github.com/solita/dev-academy-2023-exercise


## About the project 

This is a web application that displays data from journeys made with city bikes in the Helsinki Capital area.
The application allows users to view information about bike stations and journeys, including details such as departure
and return stations, distance, and duration.

The application is built using Blazor WebAssembly and consists of a client and a server. The server uses
a SQLite database to store journey and station data.

Users can view a list of all of the journeys made in 2021 summer, as well as detailed information about individual bike
stations,including the total number of journeys starting from or ending at the station, the average distance of
those journeys, and the top 5 most popular return and departure stations.


## Technologies Used

- C# and .NET Core 6 for the server
- Blazor WebAssembly for the client
- Entity Framework Core for data access
- SQLite for the database

## Features

The application provides the following features:

### Journeys List View

The journey list view allows the user to see all the journeys made with city bikes in Helsinki. Journeys are separated
by month, and each month has its own page. It's possible to search for journeys by date, and pagination is included to
avoid overloading the browser.

### Station List View

The Station List View page allows users to search for bike stations by name, and to view a paginated list of all bike
stations. The list displays the name and address of each bike station and a link to a Station Details and Stats page
for each station.

### Station Details View

The application displays detailed information about a single station, including the station name, address, total number
of journeys starting from the station, total number of journeys ending at the station, and the average distance of a journey
starting from or ending at the station. It is also possible to view the station location on the map and see the top 5 most
popular return and departure stations for journeys starting from or ending at the station. 

## How to run the project

To run the project, you need Visual Studio 2022 installed on your machine. Here are the steps to run the project:

1. Download the SQLite database file to your local machine from this link: https://drive.google.com/file/d/1TctURFUU7Dm6GDwttHhvJW19gUsrdcYU/view?usp=sharing
2. Clone the project from GitHub to your local machine. You can also copy the project straight to the Visual Studio 2022
by following these instructions https://learn.microsoft.com/en-us/visualstudio/get-started/tutorial-open-project-from-repo?view=vs-2022
3. Copy the SQLite database file BikeApp.db under the Server folder
4. Open the solution file Solita.HelsinkiBikeApp.sln in Visual Studio 2022.
5. Set the Solita.HelsinkiBikeApp.Server project as the startup project (if it's not by default').
6. Press F5 or click the 'Run' button to start the server and launch the application in your default web browser

Note: The size of the database file is quite large and it may take some time to download. It is recommended to have a stable 
internet connection while downloading the file.


## Tests

Coming soon


## Helpful Links

> **Following material will help to understand this project and learn more about Blazor WASM.**


1. [ASP.NET Core Blazor](https://docs.microsoft.com/en-us/aspnet/core/blazor/?view=aspnetcore-6.0)
2. [Build a Blazor todo list app](https://docs.microsoft.com/en-us/aspnet/core/blazor/tutorials/build-a-blazor-app?view=aspnetcore-6.0&pivots=webassembly)
3. [Call a web API from ASP.NET Core Blazor](https://docs.microsoft.com/en-us/aspnet/core/blazor/call-web-api?view=aspnetcore-6.0&pivots=webassembly)
4. [ASP.NET Core Blazor routing and navigation](https://docs.microsoft.com/en-us/aspnet/core/blazor/fundamentals/routing?view=aspnetcore-6.0)
5. [ASP.NET Core Blazor forms and validation](https://docs.microsoft.com/en-us/aspnet/core/blazor/forms-validation?view=aspnetcore-6.0)