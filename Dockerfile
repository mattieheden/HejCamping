# syntax=docker/dockerfile:1

################################################################################
# Stage 1: Build the application
################################################################################

# Use the .NET 8.0 SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /source
# Set up Microsoft package source
RUN dotnet nuget add source --name microsoft https://pkgs.dev.azure.com/dnceng/public/_packaging/dotnet-public/nuget/v3/index.json


# Copy the solution file and restore dependencies
COPY HejCamping.sln ./
COPY src/HejCamping.Application/HejCamping.Application.csproj src/HejCamping.Application/
COPY src/HejCamping.Domain/HejCamping.Domain.csproj src/HejCamping.Domain/
COPY src/HejCamping.Infrastructure/HejCamping.Infrastructure.csproj src/HejCamping.Infrastructure/
COPY src/HejCamping.Web/HejCamping.Web.csproj src/HejCamping.Web/

RUN dotnet nuget locals all --clear
# Restore the dependencies for all projects
RUN dotnet restore

# Copy the entire source code to the container
COPY . .

# Publish the application (HejCamping.Web) to the /app directory
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish src/HejCamping.Web/HejCamping.Web.csproj -c Release -o /app

################################################################################
# Stage 2: Run the application
################################################################################

# Use the ASP.NET runtime image (Alpine-based) for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final

# Install ICU package for globalization support
RUN apk add --no-cache icu-libs

# Set environment variable to enable globalization support
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# Set the working directory
WORKDIR /app

# Copy everything needed to run the app from the build stage
COPY --from=build /app .

EXPOSE 80
EXPOSE 443

# Use the non-privileged user defined in the base image
USER $APP_UID

# Set the entry point to run the application
ENTRYPOINT ["dotnet", "HejCamping.Web.dll"]
