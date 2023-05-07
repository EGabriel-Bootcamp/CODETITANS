FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source
#COPY . .
COPY *.csproj ./
#RUN dotnet restore "./UserApp/UserApp.Api.csproj" 
RUN dotnet restore *.csproj ./
RUN dotnet publish "./UserApp/UserApp.Api.csproj" -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
COPY --from=build /app ./
EXPOSE 80
ENTRYPOINT ["dotnet", "UserApp.Api.dll"]


# Dockerfile
# FROM mcr.microsoft.com/dotnet/sdk:6.0-focal  AS build-env
# WORKDIR /app

# COPY *.csproj ./
# RUN dotnet restore

# COPY . ./
# RUN dotnet publish -c Release -o out 

# FROM mcr.microsoft.com/dotnet/aspnet:6.0
# WORKDIR /app
# COPY --from=build-env /app/out .

# ENTRYPOINT ["dotnet", "aspnetcoreapp.dll"]

# FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
# WORKDIR /app
# EXPOSE 80

# ENV ASPNETCORE_URLS=http://+:80

# FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
# WORKDIR /src
# COPY ["UserApp.Api/UserApp.Api.csproj", "UserApp.Api/"]
# RUN dotnet restore "UserApp.Api\UserApp.Api.csproj"
# COPY . .
# WORKDIR "/src/UserApp.Api"
# RUN dotnet build "UserApp.Api.csproj" -c Release -o /app/build

# FROM build AS publish
# RUN dotnet publish "UserApp.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "UserApp.Api.dll"]

