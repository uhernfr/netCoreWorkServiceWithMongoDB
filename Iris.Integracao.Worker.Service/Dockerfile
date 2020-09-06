#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.


FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

#INSTALL LIBAIO1 & UNZIP (NEEDED FOR STRONG-ORACLE)
RUN apt-get update \
 && apt-get install -y libaio1 \
 && apt-get install -y build-essential \
 && apt-get install -y unzip \
 && apt-get install -y curl \
 && apt-get install -y apt-utils

#ADD ORACLE INSTANT CLIENT
RUN mkdir -p /opt/oracle

COPY ["Iris.Integracao.Worker.Service/instantclient-19.3.zip", "/opt/oracle"]
#COPY ["Portaria.IntegracaoHC.Api/instantclient-19.3.zip", "/opt/oracle/"]
RUN cd /opt/oracle && chmod +x instantclient-19.3.zip && unzip instantclient-19.3.zip

RUN mv /opt/oracle/instantclient_19_3 /opt/oracle/instantclient 

RUN unset NLS_LANG
ENV LD_LIBRARY_PATH="/opt/oracle/instantclient"
ENV OCI_HOME="/opt/oracle/instantclient"
ENV OCI_LIB_DIR="/opt/oracle/instantclient"
ENV OCI_VERSION=12
ENV NLS_LANG=PORTUGUESE_BRAZIL.WE8MSWIN1252

RUN echo '/opt/oracle/instantclient/' | tee -a /etc/ld.so.conf.d/oracle_instant_client.conf && ldconfig


FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Iris.Integracao.Worker.Service/Iris.Integracao.Worker.Service.csproj", "Iris.Integracao.Worker.Service/"]
COPY ["Iris.Integracao.Oracle/Iris.Integracao.Oracle.csproj", "Iris.Integracao.Oracle/"]
COPY ["Iris.Integracao.MongoDB/Iris.Integracao.MongoDB.csproj", "Iris.Integracao.MongoDB/"]
COPY ["Iris.Integracao.InfraHC/Iris.Integracao.InfraHC.csproj", "Iris.Integracao.InfraHC/"]
RUN dotnet restore "Iris.Integracao.Worker.Service/Iris.Integracao.Worker.Service.csproj"
COPY . .
WORKDIR "/src/Iris.Integracao.Worker.Service"
RUN dotnet build "Iris.Integracao.Worker.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Iris.Integracao.Worker.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Iris.Integracao.Worker.Service.dll"]