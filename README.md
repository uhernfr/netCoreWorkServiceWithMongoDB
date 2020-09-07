# netCoreWorkServiceWithMongoDB
Projeto Exemplo da utilização de Worker Service para copia entre collections MongoDB com agendamento por minuto. 

### Resumo
Este projeto tem como objetivo:
- Demonstrar utilização de Worker services
- Demonstrar utilizacao de mongoDb com .Net Core

### Como executar o projeto

- **Step 1 -Efetuar Clone do Projeto**

>cd\
>
>mkdir repo
>
>cd repo
>
>git clone https://github.com/uhernfr/netCoreWorkServiceWithMongoDB.git
>
>cd Integracao.Worker.Service

Restore, Build
>dotnet restore
>
>dotnet build


- **Step 2: Executar compose**
> https://hub.docker.com/_/rabbitmq

Comando para baixar imagem e iniciar o container (Porta 27018):
>***docker-compose up***

- **Step 3: Executar projeto**
>dotnet run --p netCoreWorkServiceWithMongoDB

