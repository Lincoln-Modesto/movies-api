
# Api Movies

Api .NET CORE 5 + Entity Framework

## Instalação

* ### Banco de dados

    - Para facilitar, disponibilizei uma instância do MsSqlServer na AWS, já disponível e configurada em appsettings.json
    - Caso queira iniciar um container do MsSqlServer local, na raiz do projeto execute o comando: (certifique-se de ter o docker instalado na sua máquina)

    ```
    docker-compose up -f docker-compose-database.yml
    ```

* ### Api


## Schemas

- Movie: Entidade de Filme 
- Gender: Gêneros do Filme
- Location: Filmes que foram locados por terceiros (*Não explorado no projeto)

## Relacionamentos

- O Id do Gênero (PK) é uma Foreign Key(FK) do campo GenderId da tabela Movies;
- Existe uma migration para criação de uma tabela LocationsMovies, onde a inteção é permitir o relacionamento cruzado entre filmes e locações, possibilitando multiplas locações com múltiplos filmes. (*Não implementado por completo)

## Features

- [x] Listagem de Gêneros;
- [x] Listagem de Filmes;
- [x] Cadastro de Filmes;
- [x] Edição de Filmes;
- [x] Exclusão de Filmes;

## Estrutura de diretórios

```
├── movies-api
    │── Contracts
    ├── Controllers
    │   ├── MoviesController.cs
    │
    ├── DataContext
    ├── DTO
    ├── Entities
    ├── Migrations
    ├── Services
    └── ViewModel

```

## Endpoints
- #### Listagem de Gêneros
    /api/Movies/GetGenders
- #### Listagem de Filmes
    /api/Movies/GetMovies
- #### Cadastro de Filmes
    /api/Movies/CreateMovie
- #### Edição de Filmes
    /api/Movies/UpdateMovie/{id}
- #### Exclusão de Filmes
    /api/Movies/DeleteMovie/{id}
- #### Exclusão Multiplos Filmes
    /api/Movies/DeleteMultipleMovies

### Não deu tempo*

- Criar controller, services e regras de negócio para gêneros;
- Criar controller, services e regras de negócio para locações;

### Poderia ter*
- Variáveis de ambiente configuradas