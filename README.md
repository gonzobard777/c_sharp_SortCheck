# Универсальная сортировка

Источник: [The power of Entity Framework Core and LINQ Expression Tree combined](https://medium.com/@erickgallani/the-power-of-entity-framework-core-and-linq-expression-tree-combined-6b0d72cf41db)

Для проверки используется таблица Cols.  
По всем колонкам упорядоченная по возрастанию выглядит следующим образом:

![](./pic/table-cols.png)

но в базу данных записи были внесены в хаотичном порядке:

![](./pic/table-cols-in-bd.png)

## Варианты реализации

### 1. Направление сортировки определяется значением перечисления `SortDirection`

Это вариант из статьи, в которой расписан подход универсальной сортировки.  
[Ссылка](https://github.com/gonzobard777/c_sharp_SortCheck/blob/master/ConsoleApp/Database/SortExt.cs)

### 2. Направление сортировки определяется булевым параметром `desc`

Этот вариант ориентирован на ситуацию, когда с клиента может прийти параметр `desc`:

- если передан `desc=true`, тогда сортировка по Убыванию
- если не передан параметр `desc`, либо передан `desc=false`, тогда сортировка по Возрастанию

[Ссылка](https://github.com/gonzobard777/c_sharp_SortCheck/blob/master/ConsoleApp/Database/Sort2Ext.cs)

### Поднятие и подключение к БД PostreSQL

1. Установить [докер](https://www.docker.com/)
2. В корне проекта лежит файл `docker-compose.yaml`
2. В терминале надо перейти в папку проекта и выполнить команду:

```shell
docker-compose up
```

3. Строка подключения к БД:

```
"host=127.0.0.1;port=6748;database=db;username=root;password=12345"
```