# crm-krutos
## Что это такое?
Этот проект - бэкенд для визуального конструктора CRM-систем с CRUD и функцией drag & drop для элементов холста.
Использовались следующие технологии:
  1. .NET SDK 10.0+
  2. ASP.NET Core 10.0.2 + SignalR
  3. Entity Framework Core для Postgres SQL v10.0.1
  4. PostgreSQL 18.4

## Быстрый запуск
### Требования к окружению
  1. .NET SDK 10.0+
  2. Node.js 18.0+
  3. Установленный сервер СУБД PostgreSQL 15+

### Настройка бэкенда
  1. Перейдите в директорию проекта и укажите строку подключения к PostgreSQL в файле appsettings.json:
  `JSON
     "ConnectionStrings": {
       "DefaultConnection": "host=localhost;port=5433;database=crm_constructor;username=postgres;password=s"
     }
  `
  2. Выполните накат миграций базы данных в Bash: `dotnet ef database update --context ProjectsDbContext`;
  Запустите сервер ASP.NET Core: 'dotnet run';
Документация Swagger UI станет доступна по адресу: `http://localhost:5203/swagger`.

### Настройка фронтенда
  1. Перейдите в папку клиентского приложения и установите пакеты зависимостей в Bash: `npm install`;
  2. Запустите локальный сервер разработки: `npm run dev`
