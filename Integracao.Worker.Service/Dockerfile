#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.


FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Integracao.Worker.Service/Integracao.Worker.Service.csproj", "Integracao.Worker.Service/"]
COPY ["Integracao.MongoDB/Integracao.MongoDB.csproj", "Integracao.MongoDB/"]
RUN dotnet restore "Integracao.Worker.Service/Integracao.Worker.Service.csproj"
COPY . .
WORKDIR "/src/Integracao.Worker.Service"
RUN dotnet build "Integracao.Worker.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Integracao.Worker.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Integracao.Worker.Service.dll"]