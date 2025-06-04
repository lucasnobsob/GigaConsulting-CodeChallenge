# GigaConsulting-CodeChallenge

## Stack Técnica
- .NET 9.0
- .NET WebApi
- .NET Identity
- Entity Framework 9.0
- .NET Core Native DI
- AutoMapper
- FluentValidator
- MediatR
- Swagger UI
- MySQL
- xUnit
- Moq
- Fluent Assertions
- Polly
- Refit

## Design Patterns
- Domain Driven Design
- Domain Events
- Domain Notification
- CQRS
- Event Sourcing
- Unit Of Work
- Repository & Generic Repository
- Inversion of Control / Dependency injection
- ORM
- Mediator
- Specification Pattern
- Options Pattern

Este projeto é composto por:

- **Backend:** API desenvolvida em .NET 9

---

## 🚀 Como rodar o projeto

### ✅ Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/) (caso deseje usar containers)

---

## 🐳 Rodando o projeto com Docker

1. Clone o repositório:
   ```bash
   git clone https://github.com/lucasnobsob/GigaConsulting-CodeChallenge.git
   cd seu-repositorio
   ```

2. Construa e suba os containers:
   ```bash
   docker-compose up
   ```

> A API estará disponível em `http://localhost:8080`

---

## 🧪 Rodando o projeto sem Docker

1. Clone o repositório:
   ```bash
   git clone https://github.com/lucasnobsob/GigaConsulting-CodeChallenge.git
   cd seu-repositorio
   ```

### 🔧 Rodando o Backend (.NET 9)

2. Restaure os pacotes e rode a aplicação:
   ```bash
   dotnet restore
   dotnet run
   ```

> A API estará disponível em `http://localhost:44376` conforme especificado no `launchSettings.json`.

---

## 📄 Licença

Este projeto está licenciado sob a [GNU License](LICENSE).
