# Build mərhələsi
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Layihələri kopyala və bərpa et
COPY . ./
RUN dotnet restore TaskManager/TaskManager.csproj

# Publish et
RUN dotnet publish TaskManager/TaskManager.csproj -c Release -o out

# Runtime mərhələsi
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/out .

# Portu Railway-ə uyğunlaşdır
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "TaskManager.dll"]
