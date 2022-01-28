# Loyal Tech Assessment

ASP.Net core app to automatically generate Amazon product reviews

# Build and Run Instructions

## API Local

1. Navigate to `api` folder in the terminal
1. Run the below command in the terminal
   ```bash
   dotnet watch run
   ```
1. This will auto open to swagger

## API Docker Local

1. Navigate to the `api` folder in the terminal
1. Run the below commands
   ```bash
   docker build -t review-api .
   ```
   ```bash
   docker run -d -p 8081:5000 -e ASPNETCORE_ENVIRONMENT=Development --name review-api  review-api
   ```
   - The environment tag gets you swagger, something we don't want in QA and PROD
1. Navigate to http://localhost:8081/swagger/index.html

# Deliverables

## Core

1. ASP.Net core application
   - Single Endpoint `/API/generate`
   - Returns single json object containing autogenerated review
   - Randomized star rating b/t 1 and 5
1. Ingest/train data in program startup
   - Download one of the 5-core subsets from: https://nijianmo.github.io/amazon/index.html
1. Create private github repo w/ solution
   - Add `badampowell` as collaborator

## Extra Credit

1. Unit tests
1. User-friendly front-end
   - Blazor, react, html/js (not sure)
1. Host app on azure
   - [Azure Trial](https://azure.microsoft.com/en-us/free/)
1. Dockerize app
   - Probably do a docker-compose
1. Replace randomized start rating w/ one generated on the fly

# Setup

## API

1. Using .net 6 [minimal api](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0)
1. Creating new project
   ```bash
   dotnet new webapi -minimal
   ```
1. Running locally
   ```bash
   dotnet watch run
   ```
1. I was curious about adding custom css to swagger
   - Added a dark mode I found
   - Remove `app.UseStaticFiles();` from the `Program.cs` file to reomve the custom styling

## Docker

1. I used the docker extension in vs code along w/ terminal commands
   - [Walkthrough](https://code.visualstudio.com/docs/containers/quickstart-aspnet-core)
1. Docker Build
   ```bash
   docker build -t review-api .
   ```
1. Docker run
   ```bash
   docker run -d -p 8081:5000 -e ASPNETCORE_ENVIRONMENT=Development -e --name review-api  review-api
   ```
   - The environment tag is so I can get Swagger
1. Docker remove
   ```
   docker rm review-api
   ```
