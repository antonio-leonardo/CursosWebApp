# CursosWebApp
Asp.NET Core Web Api v9.0 para CRUD de cursos

## Sobre
A WebAPI é experimmento em .NET 9.0 para cadastrar Cursos, com relacionamentos de +2 entidades: Instrutor e Plataforma (de Cursos)

## Para depurar (Visual Studio)
Basta definir o projeto asp.net core Cursos.WebApp como "Startup Project" e dar o play; no modo de depuração será criado uma database em memória.

## Testes Automatizados de Integração (xUnit)
Basta apertar o play no projeto Cursos.WebApp.IntegrationTest que será gerado um Banco de Dados em memória e executará as asserções.

## Conteinerização (Docker)
Pré-req: possuir Docker instalado/configurado em ambiente local<br />
Abrir prompt no raiz do projeto e executar o comandos abaixo para gerar a imagem:
```console
docker build -t cursoswebapp .
```
Caso o objetivo seja executar o projeto Docker em modo Desenvolvimento (com Swagger visível e Banco de Dados em Memória), a sugestão é utilzar o comando abaixo:
```console
docker run -p 8080:8080 -e ASPNETCORE_ENVIRONMENT=Development cursoswebapp
```
Caso você possua uma instância de Banco de Dados e gostaria de simular um ambiente Staging, Pré-Prod ou Prd, é necessário utilizar a linha de comando abaixo (lembre-se de incluir a string de conexão no arquivo appsettings.Production.json e não haverá mais o Swagger):
```console
docker run -p 8080:8080 -e ASPNETCORE_ENVIRONMENT=Production cursoswebapp
```
