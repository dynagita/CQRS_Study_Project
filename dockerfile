FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build

COPY ./ ./

ENV PATH $PATH:/root/.dotnet/tools

CMD dotnet restore WritableRESTAPI.sln;dotnet tool install --global dotnet-ef --version 3.1.29;dotnet ef database update --project ./WritableRESTAPI/WritableRESTAPI.csproj