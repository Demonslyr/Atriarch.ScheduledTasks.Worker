
FROM mcr.microsoft.com/dotnet/aspnet:3.1-focal AS base

FROM mcr.microsoft.com/dotnet/sdk:3.1-focal AS build
WORKDIR /src
COPY ["./src/NuGet.Config", "src/"]
COPY ["src/Atriarch.ScheduledTasks.Worker.csproj", "src/"]
RUN dotnet restore "src/Atriarch.ScheduledTasks.Worker.csproj" --configfile "src/NuGet.Config"
COPY . .
RUN dotnet build "src/Atriarch.ScheduledTasks.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/Atriarch.ScheduledTasks.Worker.csproj" -c Release -o /app/publish

FROM base AS final
RUN mkdir -p /var/atriarch-cdn && mkdir -p /simc/tmp
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Atriarch.ScheduledTasks.Worker.dll"]