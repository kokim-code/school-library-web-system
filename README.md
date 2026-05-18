# School Library Web

WEB-система хранения и перемещения книг в библиотеке школы.

## Технологии
- .NET 8
- ASP.NET Core Blazor Server
- Entity Framework Core Code First
- PostgreSQL / SQLite
- Docker и docker-compose
- xUnit для тестов бизнес-логики

## Структура
- `SchoolLibrary.Web` - основное WEB-приложение.
- `Models` - сущности предметной области.
- `Data` - `LibraryDbContext` и конфигурация EF Core.
- `Services` - бизнес-логика выдачи, возврата и перемещения книг.
- `Components/Pages` - страницы Blazor.
- `SchoolLibrary.Tests` - примеры unit-тестов.

## Локальный запуск
```bash
dotnet restore
dotnet ef database update --project SchoolLibrary.Web
dotnet run --project SchoolLibrary.Web
```

## Запуск через Docker
```bash
docker compose up --build
```

После запуска приложение доступно по адресу: http://localhost:8080

## Ссылки для отчета
Перед сдачей заменить USERNAME на свой логин:
- GitHub/GitLab: https://github.com/USERNAME/school-library-web
- Docker Hub: https://hub.docker.com/r/USERNAME/school-library-web
