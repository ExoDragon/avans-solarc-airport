FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copy necessary files and restore as distinct layer
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish PassengerEventHandler.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
COPY --from=build-env /app/out .

# Start
ENTRYPOINT ["dotnet", "PassengerEventHandler.dll"]