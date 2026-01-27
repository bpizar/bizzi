# Use the official .NET Core SDK as a parent image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project file and restore any dependencies (use .csproj for the project name)
COPY /Jaygor.People.Api/*.csproj ./Jaygor.People.Api/
# RUN dotnet restore Jaygor.People.Api/Jaygor.People.Api.csproj

# Copy the rest of the application code
COPY . .

# Publish the application
RUN dotnet publish -c Release -o out Jaygor.People.Api/Jaygor.People.Api.csproj

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Expose the port your application will run on
EXPOSE 5000

# Start the application
ENTRYPOINT ["dotnet", "Jaygor.People.Api.dll"]