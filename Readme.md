# Play Microservices

## Run mongo in docker - `docker run -d --rm --name mongo -p 27017:27017 mongodbdata:/data/db mongo`

## Create nuget package from project - `dotnet pack -o <output folder path>`
- Specific version - `dotnet pack -o <output folder path> /p:Version=1.0.1`

## Add nuget package source - `dotnet nuget add source <path to packages folder> -n <name for source>`