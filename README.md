# AnimeAPI

Uma API RESTful para gerenciar informações sobre animes. Ela permite criar, ler, modificar e deletar registros de animes, com funcionalidades de filtragem e paginação na listagem.

## Tecnologias Utilizadas
- Linguagem: C#;
- Framework: ASP.NET Core;
- ORM: Entity Framework Core;
- Padrão de Arquitetura: Clean Architecture (Onion Architecture);
- Validação: FluentValidation;
- Lidar com falhas: FluentResults;
- Testes Unitários: xUnit, NSubstitute, FluentAssertions;
- Documentação: Swagger;
- Outras: MediatR, AutoMapper.


### Layout do Projeto

```
|- src/
|   |
|   |- Application/                       // Camada de Aplicação contendo as funcionalidades CRUD, Interfaces e Mapas do AutoMapper
|   |-   |- Features/                     // Casos de Uso do CRUD
|   |-   |-   |- CreateAnime/             // Criar Anime
|   |-   |-   |- DeleteAnime/             // Deletar Anime
|   |-   |-   |- GetAnime/                // Listar Animes
|   |-   |-   |- UpdateAnime/             // Modificar Anime
|   |-   |- Interfaces/                   // Contratos utilizadas
|   |-   |- MappingProfiles/              // Profiles do AutoMapper
|   |- Domain/                            // Camada de Domínio contendo as Entidades
|   |-   |- Entities/                     // Entidades do Projeto
|   |- Infrastructure/                    // Camada de Infraestrutura contendo o contexto do banco de dados, as Migrations e Repository
|   |- |   |- Data                        // Contexto do Banco de Dados
|   |- |   |- Migrations                  // Migrations do Banco de Dados
|   |- |   |- Repositories                // Repositórios da API
|   |- WebApi/                            // Camada da API contendo o projeto e os Controllers
|   |- |   |- Controllers                 // Endpoints da API
|   |
|- test/                                  // Pasta contendo os Testes
|   |- Features/                          // Testes dos casos de uso
|   |-   |- CreateAnime/                  // Testes referentes à Criar Anime
|   |-   |- DeleteAnime/                  // Testes referentes à Deletar Anime
|   |-   |- GetAnime/                     // Testes referentes à Listar Animes
|   |-   |- UpdateAnime/                  // Testes referentes à Modificar Anime
```


##### Pré-Requisitos

É necessário antes ter um banco de dados SQL Server ativo, seguindo a configuração contida em /src/WebApi/appsettings.json, na variável ConnectionStrings.AnimeDbConnectionString,
fazendo mudanças caso seja necessário. Uma sugestão é levantar um container docker*:

```
$docker pull mcr.microsoft.com/mssql/server:2022-latest
$docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=<SuaSenhaForte>" -e "MSSQL_DATABASE=AnimeDb" -p 1433:1433 --name meu-sql-server -d mcr.microsoft.com/mssql/server:2022-latest
```

*É necessário ter o docker instalado para executar essa operação. Você pode encontrar como instalá-lo [aqui](https://docs.docker.com/engine/install/)

Instale todas as dependências utilizadas nos projetos. Você pode instalá-las de maneira automática com o Visual Studio ou ao executar o projeto via CLI. Mais informações na seção de Como executar o projeto.


## Como executar o projeto

[Instale o dotnet aqui](https://dotnet.microsoft.com/pt-br/download)
.Lembre-se de verificar por erros e testar sua conexão com seu banco de dados para prosseguir de maneira adequada.
Para executar, configure WebApi como Item de inicialização e pressione o atalho da sua IDE de escola , ou execute esse comando em /src/WebApi:

```
$dotnet run
```

### Endpoints 

Considere BASE_URL a url que seu projeto está sendo executado. O padrão é http//localhost:5238.

#### HTML

|Método HTTP|URL|Descrição|
|---|---|---|
|`GET`|BASE_URL/swagger/index.html | Página Swagger UI |

#### User Service

|HTTP Method|URL|Description|
|---|---|---|
|`POST`|BASEURL/api/Anime | Criar novo anime |
|`GET`|BASE_URL/api/Anime | Listar animes, filtrados ou não |
|`DELETE`|BASE_URL/api/Anime/{id} | Deletar Anime pelo ID |
|`PUT`|BASE_URL/api/Anime/{id} | Modificar anime pelo seu id e parâmetros |
