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

- **Step 2: Restore, Build**
Execute os comandos abaixo para build do projeto (necessário ter .net sdk)
>dotnet restore
>
>dotnet build


- **Step 2: Executar compose**

Comando para baixar imagem e iniciar os containers:
>***docker-compose up***

- **Step 3: Teste**

É  possivel acompanhar o log no proprio docker

==**netcoreworkservicewithmongodb**==
>
- worker_service (serviço)
>
- mongodb (database)

**Conect ao mongodb localhost:27018 e verifique a inclusao de registros.**

