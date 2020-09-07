# PhoneBook
## Окружение
1. Установить [.NET Core 3.1 SDK (v3.1.401)](https://download.visualstudio.microsoft.com/download/pr/547f9f81-599a-4b58-9322-d1d158385df6/ebe3e02fd54c29487ac32409cb20d352/dotnet-sdk-3.1.401-win-x64.exe)
2. Установить Entity Framework Core .NET Command-line Tools 3.1.7 выполнив  
`dotnet tool install --global dotnet-ef --version 3.1.7`
3. Установить [Microsoft SQL Server 2017 Express](https://www.microsoft.com/ru-RU/download/details.aspx?id=55994)

## Развёртывание БД
Перед первым запуском приложения необходимо развернуть базу данных. Для этого необходимо:
1. Перейти в директорию  
`..\src`
2. Выполнить  
`dotnet ef database update -s Domodedovo.PhoneBook.WebAPI -p Domodedovo.PhoneBook.Data`

## Инструкция по запуску
### UserLoader
Для загрузки пользователей:
1. Перейти в директорию  
`..\src\Domodedovo.PhoneBook.UserLoader`
2. Выполнить  
`dotnet run -- -cmd fetch`

В результате в БД будет загружено 1000 пользователей с сайта www.randomuser.me.

Чтобы указать другое количество пользователей на шаге 2 необходимо выполнить комманду:  
`dotnet run -- -cmd fetch -ucount <количество>`