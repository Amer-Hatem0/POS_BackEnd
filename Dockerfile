# ========== Build ==========
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy all source (to resolve ProjectReferences)
COPY . .

# Restore on the solution or project (solution is safer with dependencies)
RUN dotnet restore ./BRIXEL/BRIXEL.sln

# Publish the web project
RUN dotnet publish ./BRIXEL/BRIXEL.csproj -c Release -o /app/out

# ========== Run ==========
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy the output
COPY --from=build /app/out .

# Render passes PORT as an environment variable; let Kestrel listen on it
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://0.0.0.0:$PORT

# Render's default port is often 10000 (EXPOSE is optional)
EXPOSE 10000

# DLL name according to .csproj = BRIXEL.dll
ENTRYPOINT ["dotnet", "BRIXEL.dll"]