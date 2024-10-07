# Используем официальный образ .NET SDK для сборки приложения
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем файл решения и все проекты
COPY GraphQL_lesson.sln .
COPY GraphQL_lesson/GraphQL_lesson.csproj GraphQL_lesson/
COPY CategoryServiceWebAPI/CategoryServiceWebAPI.csproj CategoryServiceWebAPI/
COPY ProductServiceWebAPI/ProductServiceWebAPI.csproj ProductServiceWebAPI/
COPY StorageServiceWebAPI/StorageServiceWebAPI.csproj StorageServiceWebAPI/
COPY Shared/Shared.csproj Shared/
COPY DataBase/DataBase.csproj DataBase/

# Восстанавливаем зависимости
RUN dotnet restore GraphQL_lesson/GraphQL_lesson.csproj

# Копируем весь код в контейнер
COPY . .

# Публикуем главный проект
RUN dotnet publish GraphQL_lesson/GraphQL_lesson.csproj -c Release -o /app/publish

# Используем официальный образ .NET Runtime для запуска приложения
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Устанавливаем переменную окружения для указания порта
ENV ASPNETCORE_URLS=http://+:5000

# Копируем опубликованные файлы из предыдущего этапа
COPY --from=build /app/publish .

# Открываем порт 5000 для доступа к приложению
EXPOSE 5000

# Указываем точку входа приложения
ENTRYPOINT ["dotnet", "GraphQL_lesson.dll"]
