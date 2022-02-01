# Loyal Tech Assessment

ASP.Net core app to automatically generate Amazon product reviews

# Build and Run Instructions

1. Clone repo
   ```bash
   git clone https://github.com/eventhorizn/loyal-tech-assessment.git
   ```
1. I'm overdoing it on the options below
   - docker-compose
   - docker manually
   - dotnet watch run manually
1. When running the api, I pulled the Markov Chain Depth and rows to train on into app settings
   - Limit memory usage
   - I'm guessing the results get better as these numbers go up (though depth doesn't)
   - Can be tweaked (and project rebuilt)
1. Run unit tests
   - From root run
   ```bash
   dotnet test loyal-tech-assessment.sln
   ```

## Docker Compose (Preferred Way)

1. From the root folder
   ```bash
   docker-compose up
   ```
1. Navigate to: http://localhost:5001/
1. To refresh
   ```bash
   docker-compose build
   ```

## API Docker Local

1. Navigate to the `api` folder in the terminal
   ```bash
   cd api
   ```
1. Run the below commands
   ```bash
   docker build -t review-api -f Dockerfile.local .
   ```
   ```bash
   docker run -d -p 5000:80 -e ASPNETCORE_ENVIRONMENT=Development --name review-api  review-api
   ```
   - The environment tag gets you swagger, something we don't want in QA and PROD
1. Navigate to http://localhost:5000/swagger/index.html

## UI Docker Local

1. Navigate to the `ui` folder in the terminal
1. Run the below commands
   ```bash
   docker build -t review-ui -f Dockerfile.local .
   ```
   ```bash
   docker run -d -p 5001:80 -e ASPNETCORE_ENVIRONMENT=DockerLocal --name review-ui --link review-api:review-api  review-ui
   ```
1. Navigate to http://localhost:5001
1. The API must be built first!

## API Local

1. Navigate to the `api` folder in the terminal
1. Run the below command in the terminal
   ```bash
   dotnet watch run
   ```
1. This will auto open to swagger
1. Make sure all images from above are not running

## UI Local

1. Navigate to the `ui` folder in the terminal
1. Run the below command in the terminal
   ```bash
   dotnet watch run
   ```
1. Make sure all images from above are not running

# Deployed Applications

1. WebApi deployed to Azure App Service (no Docker)
   - [Link](https://ghake-review-api.azurewebsites.net/swagger/index.html)
   - https://ghake-review-api.azurewebsites.net/api/generate
1. WebApi deployed to Azure App Service (Docker, with Azure Container Registry)
   - [Link](https://ghake-docker-review-api.azurewebsites.net/swagger/index.html)
   - It's impossible to prove just with a link, so I can walk through the process
   - https://ghake-docker-review-api.azurewebsites.net/api/generate
1. Blazor UI deployed to Azure App Service (no Docker)
   - [Link](https://ghake-review-ui.azurewebsites.net/)
1. Blazor UI deployed to Azure App Service (Docker, with Azure Contiainer Registry)
   - [Link](https://ghake-docker-review-ui.azurewebsites.net/)

# Deliverables

## Core

1. ASP.Net core application
   - Single Endpoint `/API/generate`
     - DONE
   - Returns single json object containing autogenerated review
     - DONE
   - Randomized star rating b/t 1 and 5
     - DONE
1. Ingest/train data in program startup
   - Download one of the 5-core subsets from: https://nijianmo.github.io/amazon/index.html
   - DONE
1. Create private github repo w/ solution
   - Add `badampowell` as collaborator
   - DONE

## Extra Credit

1. Unit tests
1. User-friendly front-end
   - Blazor
1. Host app on azure
   - [Azure Trial](https://azure.microsoft.com/en-us/free/)
   - DONE
1. Dockerize app
   - docker-compose locally
   - DONE
1. Replace randomized start rating w/ one generated on the fly
   - Using sentiment analysis

# Setup

1. Set up sln in base folder
   ```bash
   dotnet new sln
   ```
1. Build solution
   ```bash
   dotnet build
   ```

## API

1. Using .net 6 [minimal api](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0)
1. Creating new project
   ```bash
   dotnet new webapi -minimal
   ```
1. Add to Solution file
   ```bash
   dotnet sln add api/review-api.csproj
   ```
1. Running locally
   ```bash
   dotnet watch run
   ```
1. Swagger [Customization](https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-6.0&tabs=visual-studio#customize-and-extend)
   - Added [dark mode](https://dev.to/amoenus/turn-swagger-theme-to-the-dark-mode-4l5f)
   - Remove `app.UseStaticFiles();` from the `Program.cs` file to reomve the custom styling

### Reading .gz File

1. Using [SharpZipLib](https://www.nuget.org/packages/SharpZipLib/) to read the file
   ```bash
   dotnet add package SharpZipLib --version 1.3.3
   ```
   - Very fast reading of a data set
1. Using [Markov](https://www.nuget.org/packages/Markov/2.0.1-ci0025) to train my chain
   ```bash
   dotnet add package Markov --version 2.0.1-ci0025
   ```
   - Depth of 4 is max my computer could handle (16 gb memory)

## Blazor Front End

1. Creating new project
   ```bash
   dotnet new blazorserver
   ```
1. Add to Solution file
   ```bash
   dotnet sln add ui/review-ui.csproj
   ```
1. Running locally
   ```bash
   dotnet watch run
   ```

## Test

1. Create new project
   ```bash
   dotnet new mstest -o review-generator-test
   ```
1. Add to Solution file
   ```bash
   dotnet sln add review-generator-test/review-generator-test.csproj
   ```
1. Add reference to api
   ```bash
   dotnet add review-generator-test/review-generator-test.csproj reference api/review-api.csproj
   ```

## Docker Compose

1. Stay in the root folder
   ```bash
   docker-compose up
   ```
1. After making changes
   ```bash
   docker-compose build
   ```
   - Rebuilds your images
1. Navigate to http://localhost:5001/
   - This front end calls the api

## Docker

### API

1. I used the docker extension in vs code along w/ terminal commands
   - [Walkthrough](https://code.visualstudio.com/docs/containers/quickstart-aspnet-core)
1. Docker Build
   ```bash
   docker build -t review-api -f Dockerfile.local .
   ```
1. Docker run
   ```bash
   docker run -d -p 5000:80 -e ASPNETCORE_ENVIRONMENT=Development --name review-api  review-api
   ```
   - The environment tag is so I can get Swagger
1. Docker remove
   ```
   docker rm review-api
   ```
1. Navigate to http://localhost:5000/swagger/index.html

### UI

1. I used the docker extension in vs code along w/ terminal commands
   - [Walkthrough](https://code.visualstudio.com/docs/containers/quickstart-aspnet-core)
1. Docker Build
   ```bash
   docker build -t review-ui -f Dockerfile.local .
   ```
1. Docker run
   ```bash
   docker run -d -p 5001:80 -e ASPNETCORE_ENVIRONMENT=DockerLocal --name review-ui --link review-api:review-api  review-ui
   ```
1. Docker remove
   ```
   docker rm review-ui
   ```
1. Navigate to http://localhost:5001

## Azure

The goal is to have a CI/CD pipeline do this for me, but baby steps

### Manually publishing to Azure App Service

1. [Quickstart](https://docs.microsoft.com/en-us/azure/app-service/quickstart-dotnetcore?tabs=net60&pivots=development-environment-vs)
1. I use the `Azure Tools` extension in vscode
1. Need to create a:
   - Resource Group
   - Hosting Plan
   - App Service
   - App
1. BIG GOTCHA
   - vscode's implemenetation leaves a bit to be desired w/ the manual approach
   - Need to manually publish first `dotnet publish -c Release`
   - In the .vscode folder, create a `settings.json` file
     - "appService.deploySubpath": "bin\\Release\\net6.0\\publish"
   - This will mirror a `zip deploy` (Visual Studio was much smoother about this)
1. Since I have multiple apps in my repo, I need to open code on the app I'm deploying
   - Open api folder in it's one code instance

### Manually publishing to Azure Container Registry, then to Azure App Service

Using the following walkthrough: [Link](https://code.visualstudio.com/docs/containers/app-service)

1. Create application image
   ```bash
   docker build -t review-api -f Dockerfile.local .
   ```
   ```
   docker build -t review-ui -f Dockerfile.local .
   ```
1. Push image to container registry
   - Manually create a container registry in docker
   - Need to have the docker and azure extensions installed
   - Right click on tagged image, push
1. In Docker Explorer, navigate to image under Registries
   - Right click on tag, `Deploy to Azure App Service`
1. Subsequent updates follow this path:
   - Make updates locally
   - Push updates to registry
   - Restart app (which will pull most recent image)
