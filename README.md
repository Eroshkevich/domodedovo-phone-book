# Задание

## Необходимо написать два приложения:
1. Консольное приложение, которое при запуске загружает с https://randomuser.me/ 1000 пользователей и добавляет их в БД
2. WEB API, по запросу предоставляет информацию из БД по пользователю (ФИО, дата рождения, фото), фото можно в виде ссылки на источник. Так же необходимо реализовать постраничный вывод пользователей (например, порциями по 10) и сортировку по дате и ФИО.

## В качестве дополнительного функционала можно реализовать:
1. Хранение изображений в БД или файловой системе
2. Аутентификацию OAuth2
3. Web интерфейс тестирования запросов к WEB API (ввод параметров и вывод
результата на страницу)

# PhoneBook

## Окружение
1. Установить [.NET Core 3.1 SDK (v3.1.401)](https://download.visualstudio.microsoft.com/download/pr/547f9f81-599a-4b58-9322-d1d158385df6/ebe3e02fd54c29487ac32409cb20d352/dotnet-sdk-3.1.401-win-x64.exe)
2. Установить Entity Framework Core .NET Command-line Tools 3.1.8 выполнив  
`dotnet tool install --global dotnet-ef --version 3.1.8`
3. Установить [Microsoft SQL Server 2017 Express](https://www.microsoft.com/ru-RU/download/details.aspx?id=55994)

## Развёртывание БД
Перед первым запуском приложения необходимо развернуть базу данных. Для этого необходимо:
1. Перейти в директорию  
`..\src`
2. Выполнить  
`dotnet ef database update -s Domodedovo.PhoneBook.WebAPI -p Domodedovo.PhoneBook.Data`

## Инструкция по запуску
### UserLoader
#### Загрузка пользователей

1. Перейти в директорию  
`..\src\Domodedovo.PhoneBook.UserLoader`
2. Выполнить  
`dotnet run -- -cmd fetch`

В результате в БД будет загружено 1000 пользователей с сайта www.randomuser.me.

Чтобы указать другое количество пользователей на шаге 2 необходимо выполнить комманду:  
`dotnet run -- -cmd fetch -ucount <количество>`

### Сохранение фотографий

Для сохранения фотографий при загрузке пользователей необходимо на шаге 2 выполнить комманду:  
`dotnet run -- -cmd fetch -ucount <количество> -pload <опции загрузки>`  
Доступные `<опции загрузки>`:
- `_db` для сохранения фотографий в БД
- `_fs` для сохранения фотографий в Файловой системе (относительный путь к файловому хранилищу может быть указан в конфигурационном файле `appsettings.json`)
- `_hard` для игнорирования исключений при загрузке файлов (в случае если при загрузке или сохранении файлов возникнут исключения пользователи всё равно будут сохранены в БД)

Перечисленные `<опции загрузки>` можно совместить.  
Пример комманды:
`dotnet run -- -cmd fetch -ucount 10 -pload _db_fs_hard`

### WebAPI
#### Запуск приложения
1. Перейти в директорию
`..\src\Domodedovo.PhoneBook.WebAPI`
2. Выполнить
`dotnet run`

#### Интерфейс тестировния запросов
1. Запустить WebAPI
2. Перейти в браузере на https://localhost:5001/swagger/index.html

#### Получение списка пользователей
1. Запустить WebAPI
2. Выполнить GET запрос https://localhost:5001/api/user (информация по параметрам запроса приведена в [интерфейсе тестирования запросов](https://localhost:5001/swagger/index.html))

#### OAuth авторизация в GitHub
1. Запустить WebAPI
2. Выполнить GET запрос https://localhost:5001/api/auth

При успешной аутентификации отображается приветствие