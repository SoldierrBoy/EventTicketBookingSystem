# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Копіюємо файл проекту, вказуючи шлях до підпапки
COPY EventTicketBookingSystem/EventTicketSystem.csproj ./
RUN dotnet restore

# Копіюємо весь вміст папки проекту в робочу директорію
COPY EventTicketBookingSystem/ ./
RUN dotnet publish -c Release -o out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 8080
ENTRYPOINT ["dotnet", "EventTicketSystem.dll"]