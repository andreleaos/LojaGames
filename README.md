<h1 align="center"> Tech Challenge - Grupo 31 </h1>
<h3 align="center">Desenvolvimento do projeto GameStore.</h3>

## 📚 Sobre o projeto

O projeto tem como objetivo criar uma solução para gestão de produtos de uma loja de games online. A Solução contém o projeto de uma API que manipula uma base de dados SQL Server.
O projeto está sendo desenvolvido em grupo, com o objetivo de compartilhar conhecimentos e experiências e atender os requisitos avaliativos do Tech Challenge FIAP do curso postech ARQUITETURA DE SISTEMAS .NET COM AZURE.

## 📝 Conteúdo

- [Sobre o projeto](#-sobre-o-projeto)

## Configuração do ambiente

### 📋 Pré-requisitos

- [.NET 7.0](https://dotnet.microsoft.com/download/dotnet/7.0)
- [Sql Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)

### 🎲 Banco de dados
A configuração do banco de dados é feita através do arquivo appsettings.json, que fica na raiz do projeto GameStore.API. 
O arquivo já está configurado para o banco de dados **Sql Server** local, mas caso queira utilizar outro banco de dados, basta alterar a string de conexão. Você pode configurar também
a váriavel `lojaGamesDB` que pode conter o endereço do banco remoto, no caso deste projeto ele será publicado no Azure. Importante configurar também a flag `enable_connection_local_db` 
para habilitar a troca do banco apontando para nuvem ou para o servidor local.

```json
"FeatureFlags": {
    "enable_connection_local_db":  "true"
  },
  "ConnectionStrings": {
    "lojaGamesDB_local": "string_de_conexao_com_SQLServer",
    "lojaGamesDB": ""
  }
```

Ainda exite a configuração das imagens usando o container do BlobStorage do Azure.
```json
  //Definir qual a connection string na chave de acesso do storage account
  "ConnectionStorageAccount": "",

  //Definir o nome do container criado no storage account para receber os blobs storages
  "ContainerBlobStorage": "",
```

## 🚀 Como executar o projeto

```bash
# Clone este repositório
$ git clone https://github.com/andreleaos/LojaGames.git

# Acesse a pasta do projeto no terminal o projeto Web /cmd
$ cd ./LojaGames/GameStore/GameStore.Web [Executar o projeto Web]

# Acesse a pasta do projeto no terminal o projeto API /cmd
$ cd ./LojaGames/GameStore/GameStore.Api [Executar o projeto API]

# Execute a aplicação em modo de desenvolvimento
$ dotnet run

# O servidor inciará localmente na porta:5237 - acesse http://localhost:5237
```

## 🛠 Tecnologias

As seguintes ferramentas foram usadas na construção do projeto:

- [C#](https://docs.microsoft.com/pt-br/dotnet/csharp/) - Linguagem
- [.NET](https://docs.microsoft.com/pt-br/dotnet/) - Framework
- [ASP.NET](https://learn.microsoft.com/pt-br/aspnet/core/mvc/overview?view=aspnetcore-7.0) - Asp.NET Core MVC
- [ADO.NET](https://learn.microsoft.com/pt-br/dotnet/framework/data/adonet/) - ADO.NET
- [Swagger](https://swagger.io/) - Documentação da API

## ✒️ Colaborador(as/es)

- **Fernando Augusto Ribeiro Alves** - _Desenvolvedor_  - [Faralves](https://github.com/faralves)
- **André Leão da Silva** - _Desenvolvedor_ - [andreleaos](https://github.com/andreleaos)
- **André Bessa da Silva** - _Desenvolvedor_  - [bessax](https://github.com/bessax)
- **Liandro Freire dos Anjos** - _Desenvolvedor_  - [liandro](oliverliandro@gmail.com)
- **Diogo da Franca Rodrigues** - _Desenvolvedor_  - [diogo](diogo_f.rodrigues@hotmail.com)
