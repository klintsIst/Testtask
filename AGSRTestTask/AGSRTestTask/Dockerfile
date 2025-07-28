FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AGSRTestTask/AGSRTestTask.WebAPI.csproj", "AGSRTestTask/"]
COPY ["AGSRTestTask/AGSRTestTask.Common.csproj", "AGSRTestTask/AGSRTestTask.Common/"]
COPY ["AGSRTestTask/AGSRTestTask.Core.csproj", "AGSRTestTask/AGSRTestTask.Core/"]
COPY ["AGSRTestTask/AGSRTestTask.Infrastructure.csproj", "AGSRTestTask/AGSRTestTask.Infrastructure/"]
RUN dotnet restore "./AGSRTestTask/AGSRTestTask.WebAPI.csproj"
COPY . .
WORKDIR "/src/AGSRTestTask"
RUN dotnet tool install --global dotnet-ef 
ENV PATH="$PATH:/root/.dotnet/tools"
RUN dotnet build "./AGSRTestTask.WebAPI.csproj" -c %BUILD_CONFIGURATION% -o /app/build
CMD dotnet ef database update --environment Development --project src/AGSRTestTask/AGSRTestTask.Infrastructure

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AGSRTestTask.WebAPI.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AGSRTestTask.WebAPI.dll"]