# Используем официальный образ .NET SDK для сборки приложения
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем файл решения и проекты
COPY GraphQL_lesson.sln .
COPY StorageServiceWebAPI/StorageServiceWebAPI.csproj StorageServiceWebAPI/
COPY DataBase/DataBase.csproj DataBase/
COPY Shared/Shared.csproj Shared/

# Восстанавливаем зависимости
RUN dotnet restore StorageServiceWebAPI/StorageServiceWebAPI.csproj

# Копируем весь код в контейнер
COPY . .

# Публикуем проект
RUN dotnet publish StorageServiceWebAPI/StorageServiceWebAPI.csproj -c Release -o /app/out

# Используем официальный образ .NET Runtime для запуска приложения
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Устанавливаем переменную окружения для указания порта
ENV ASPNETCORE_URLS=http://+:80

# Копируем опубликованные файлы из предыдущего этапа
COPY --from=build /app/out .

# Открываем порт 80
EXPOSE 80

# Указываем точку входа приложения
ENTRYPOINT ["dotnet", "StorageServiceWebAPI.dll"]
