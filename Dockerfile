# syntax=docker/dockerfile:1

################################################################################
# Stage 1: Build the application
################################################################################

# Use the .NET 8.0 SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Copy the entire source code to the container
COPY . /source

# Set the working directory
WORKDIR /source

# This is the architecture you’re building for, passed in by the builder
ARG TARGETARCH

# Install bash for advanced shell scripting
RUN apt-get update && apt-get install -y bash

# Set the shell to use bash instead of the default shell
SHELL ["/bin/bash", "-c"]

# Build the application
# Replace 'amd64' with 'x64' since 'x64' is the canonical name in .NET
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish -a ${TARGETARCH/amd64/x64} --use-current-runtime --self-contained false -o /app

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
# Switch to a non-privileged user (defined in the base image)
USER $APP_UID

# Set the entry point to run the application
ENTRYPOINT ["dotnet", "HejCamping.dll"]
