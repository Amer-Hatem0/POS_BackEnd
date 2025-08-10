# ============ Build stage ============
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# نسخ ملفات المشاريع (عشان restore سريع)
COPY BRIXEL_core/BRIXEL_core.csproj BRIXEL_core/
COPY BRIXEL_infrastructure/BRIXEL_infrastructure.csproj BRIXEL_infrastructure/
COPY BRIXEL/BRIXEL.csproj BRIXEL/

RUN dotnet restore BRIXEL/BRIXEL.csproj

# نسخ كل السورس وبناء Publish
COPY . .
RUN dotnet publish BRIXEL/BRIXEL.csproj -c Release -o /app/out

# ============ Runtime stage ============
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# لازم يسمع على $PORT اللي Render بيضبطه
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT}

# (اختياري) تعطيل detailed errors على الإنتاج تلقائيًا
ENV ASPNETCORE_ENVIRONMENT=Production

COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "BRIXEL.dll"]
