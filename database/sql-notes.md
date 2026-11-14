# SQL notes
> Учебная БД: PracticeDB. Скрипт создания — `init.sql`.

## Task 03 — SELECT + WHERE
### Запрос 1: Сотрудники с зарплатой > 50000
SELECT EmployeeName, Salary 
FROM Employees 
WHERE Salary > 50000;
### Результат
Анна Иванова	65000.00
Пётр Сидоров	72000.00
Мария Кузнецова	55000.00
Ольга Смирнова	61000.00
Дмитрий Орлов	58000.00
Елена Волкова	52000.00
Алексей Лебедев	90000.00
### Вывод
Запрос отработал, вернул 7 строк.

### Запрос 2: Сотрудники, нанятые в 2023 году
SELECT EmployeeName, HireDate 
FROM Employees 
WHERE YEAR(HireDate) = 2023;
### Результат
Иван Петров	2023-02-10
Елена Волкова	2023-06-01
### Вывод
Запрос вернул сотрудников, чья дата найма приходится на 2023 год.

### Запрос 3: Имя начинается на «А»
SELECT EmployeeName 
FROM Employees 
WHERE EmployeeName LIKE N'А%';
### Результат
Анна Иванова
Алексей Лебедев
### Вывод
Использование N'А%' позволяет искать по русской букве «А».

### Запрос 4: Сотрудники из отдела 2
SELECT EmployeeName, DepartmentId 
FROM Employees 
WHERE DepartmentId = 2;
### Результат
Ольга Смирнова	2
Дмитрий Орлов	2
Елена Волкова	2
### Вывод
Вернулись только сотрудники аналитики (DepartmentId = 2).

### Запрос 5: Зарплата в диапазоне 40000-60000
SELECT EmployeeName, Salary 
FROM Employees 
WHERE Salary BETWEEN 40000 AND 60000;
### Результат
Мария Кузнецова	55000.00
Иван Петров	48000.00
Дмитрий Орлов	58000.00
Елена Волкова	52000.00
Сергей Морозов	45000.00
Наталья Соколова	43000.00
### Вывод
Оператор BETWEEN включает границы диапазона.



## Task 06 — GROUP BY
### SQL
SELECT DepartmentId, COUNT(*) AS EmployeeCount, AVG(Salary) AS AvgSalary, SUM(Salary) AS TotalSalary
FROM Employees
GROUP BY DepartmentId
ORDER BY AvgSalary DESC;
### Результат
4	1	90000.000000	90000.00
1	4	60000.000000	240000.00
2	3	57000.000000	171000.00
3	2	44000.000000	88000.00
### Вывод
GROUP BY позволяет считать агрегаты (COUNT, AVG) не для всей таблицы, а для каждой группы (отдела) отдельно. Сортировка по средней зарплате показывает, какой отдел самый «богатый».



## Task 07 — GROUP BY + HAVING
### Запрос 1 (больше 2 сотрудников)
SELECT DepartmentId, COUNT(*) AS EmployeeCount
FROM Employees
GROUP BY DepartmentId
HAVING COUNT(*) > 2;
### Результат
1	4
2	3
### Вывод
HAVING отфильтровал отделы, оставив только те, где сотрудников больше двух.

### Запрос 2 (средняя зарплата > 55000)
SELECT DepartmentId, AVG(Salary) AS AvgSalary
FROM Employees
GROUP BY DepartmentId
HAVING AVG(Salary) > 55000;
### Результат
1	60000.000000
2	57000.000000
4	90000.000000

### Запрос 3 (суммарная зарплата > 150000)
SELECT DepartmentId, SUM(Salary) AS TotalSalary
FROM Employees
GROUP BY DepartmentId
HAVING SUM(Salary) > 150000;
### Результат
1	240000.00
2	171000.00




## Task 08 — INNER JOIN
### SQL
SELECT e.EmployeeName, d.DepartmentName, e.Salary
FROM Employees e
INNER JOIN Departments d ON e.DepartmentId = d.DepartmentId
ORDER BY d.DepartmentName ASC, e.Salary DESC;
### Результат
Ольга Смирнова	Аналитика	61000.00
Дмитрий Орлов	Аналитика	58000.00
Елена Волкова	Аналитика	52000.00
Алексей Лебедев	Менеджмент	90000.00
Пётр Сидоров	Разработка	72000.00
Анна Иванова	Разработка	65000.00
Мария Кузнецова	Разработка	55000.00
Иван Петров	Разработка	48000.00
Сергей Морозов	Тестирование	45000.00
Наталья Соколова	Тестирование	43000.00
### Вывод
INNER JOIN соединил две таблицы по ключу DepartmentId. Теперь вместо цифры отдела мы видим его название. Сортировка сначала по отделу, потом по убыванию зарплаты.


## Task 09 — LEFT JOIN
### SQL
SELECT e.EmployeeId, e.EmployeeName
FROM Employees e
LEFT JOIN EmployeeProjects ep ON e.EmployeeId = ep.EmployeeId
WHERE ep.EmployeeId IS NULL;
### Результат
9	Наталья Соколова
### Вывод
LEFT JOIN вернул всех сотрудников, а условие WHERE ep.EmployeeId IS NULL отфильтровало только тех, у кого нет проектов (правая таблица пуста).


## Task 10 — Множественный JOIN
### SQL
SELECT e.EmployeeName, d.DepartmentName, p.ProjectName, ep.Hours
FROM Employees e
INNER JOIN Departments d ON e.DepartmentId = d.DepartmentId
INNER JOIN EmployeeProjects ep ON e.EmployeeId = ep.EmployeeId
INNER JOIN Projects p ON ep.ProjectId = p.ProjectId
ORDER BY d.DepartmentName ASC, e.EmployeeName ASC, p.ProjectName ASC;
### Результат
Дмитрий Орлов	Аналитика	AI Assistant	50
Дмитрий Орлов	Аналитика	Data Warehouse	70
Елена Волкова	Аналитика	Mobile App	95
Ольга Смирнова	Аналитика	Data Warehouse	110
Алексей Лебедев	Менеджмент	AI Assistant	200
Алексей Лебедев	Менеджмент	CRM System	30
Анна Иванова	Разработка	CRM System	120
Анна Иванова	Разработка	Data Warehouse	80
Иван Петров	Разработка	Legacy Refactor	40
Иван Петров	Разработка	Mobile App	90
Мария Кузнецова	Разработка	Mobile App	100
Пётр Сидоров	Разработка	AI Assistant	60
Пётр Сидоров	Разработка	CRM System	150
Сергей Морозов	Тестирование	Legacy Refactor	130
### Вывод
Мы соединили 4 таблицы последовательно. Связь «многие-ко-многим» между сотрудниками и проектами реализована через промежуточную таблицу EmployeeProjects. В результате видно, кто на каком проекте и сколько часов отработал.


## Task 11 — Сотрудники с зарплатой выше средней
### SQL
SELECT EmployeeName, Salary
FROM Employees
WHERE Salary > (SELECT AVG(Salary) FROM Employees);

### Результат
Анна Иванова	65000.00
Пётр Сидоров	72000.00
Ольга Смирнова	61000.00
Алексей Лебедев	90000.00

### Вывод
Подзапрос вычислил среднюю зарплату по компании. Внешний запрос вывел только тех, чья зарплата выше этого значения.


## Task 12 — Сотрудники отдела «Разработка» (подзапрос)
### SQL
SELECT EmployeeName
FROM Employees
WHERE DepartmentId = (
    SELECT DepartmentId FROM Departments WHERE DepartmentName = N'Разработка'
);

### Результат
Анна Иванова
Пётр Сидоров
Мария Кузнецова
Иван Петров

### Вывод
Подзапрос нашел ID отдела «Разработка», а внешний запрос вывел всех сотрудников с этим ID.



## Task 13 — Сотрудники с проектами (EXISTS)
### SQL
SELECT e.EmployeeName
FROM Employees e
WHERE EXISTS (
    SELECT 1 FROM EmployeeProjects ep WHERE ep.EmployeeId = e.EmployeeId
);

### Результат
Анна Иванова
Пётр Сидоров
Мария Кузнецова
Иван Петров
Ольга Смирнова
Дмитрий Орлов
Елена Волкова
Сергей Морозов
Алексей Лебедев

### Вывод
EXISTS проверяет наличие хотя бы одной записи в EmployeeProjects для каждого сотрудника. Если запись есть — сотрудник выводится.



## Task 14 — Сотрудники без проектов (NOT EXISTS)
### SQL
SELECT e.EmployeeName
FROM Employees e
WHERE NOT EXISTS (
    SELECT 1 FROM EmployeeProjects ep WHERE ep.EmployeeId = e.EmployeeId
);

### Результат
Наталья Соколова

### Вывод
NOT EXISTS вернул тех сотрудников, для которых в таблице проектов нет ни одной записи.



## Task 15 — UNION (сотрудники + отделы в один столбец)
### SQL
SELECT EmployeeName AS Name, 'Сотрудник' AS Type FROM Employees
UNION
SELECT DepartmentName, 'Отдел' FROM Departments;

### Результат
Анна Иванова	Разработка
Пётр Сидоров	Разработка
Мария Кузнецова	Разработка
Иван Петров	Разработка
Ольга Смирнова	Аналитика
Дмитрий Орлов	Аналитика
Елена Волкова	Аналитика
Сергей Морозов	Тестирование
Наталья Соколова	Тестирование
Алексей Лебедев	Менеджмент

### Вывод
UNION объединил результаты двух запросов в один список. Второй столбец (Type) показывает, откуда взялась строка.


## Task 16 — UPDATE (повышение зарплаты на 10%)
### SQL
До
SELECT EmployeeName, Salary FROM Employees WHERE DepartmentId = 1;

Обновление
UPDATE Employees
SET Salary = Salary * 1.10
WHERE DepartmentId = 1;

После
SELECT EmployeeName, Salary FROM Employees WHERE DepartmentId = 1;

### Результат (до и после)
До:
Анна Иванова	65000.00
Пётр Сидоров	72000.00
Мария Кузнецова	55000.00
Иван Петров	48000.00

После:
Анна Иванова	71500.00
Пётр Сидоров	79200.00
Мария Кузнецова	60500.00
Иван Петров	52800.00
### Вывод
UPDATE изменил данные. Мы использовали SELECT до и после, чтобы убедиться, что зарплаты увеличились ровно на 10%.


## Task 17 — DELETE (удаление проекта)
### SQL
До
SELECT * FROM Projects WHERE ProjectName = N'Legacy Refactor';

Удаляем связи
DELETE FROM EmployeeProjects 
WHERE ProjectId = (SELECT ProjectId FROM Projects WHERE ProjectName = N'Legacy Refactor');

Удаляем проект
DELETE FROM Projects WHERE ProjectName = N'Legacy Refactor';

После
SELECT * FROM Projects WHERE ProjectName = N'Legacy Refactor';

### Результат (до и после)
До:
5	Legacy Refactor	2021-06-15
После:
Ничего, пустой результат
### Вывод
Сначала мы удалили связанные записи из EmployeeProjects (из-за внешнего ключа), а затем сам проект. После удаления SELECT возвращает пустой результат.


## Task 18 — Транзакция (BEGIN TRAN / ROLLBACK)
### SQL
begin;

UPDATE Employees SET Salary = Salary + 1000 WHERE DepartmentId = 2;
SELECT EmployeeName, Salary FROM Employees WHERE DepartmentId = 2;

rollback;

SELECT EmployeeName, Salary FROM Employees WHERE DepartmentId = 2;

### Результат
Ольга Смирнова	62000.00
Дмитрий Орлов	59000.00
Елена Волкова	53000.00

### Вывод
Транзакция позволяет безопасно тестировать изменения. ROLLBACK отменил все изменения, сделанные внутри транзакции.



## Task 19 — Создание VIEW (представления)
### SQL
CREATE VIEW vw_EmployeeDetails AS
SELECT 
    e.EmployeeName, 
    d.DepartmentName, 
    e.Salary
FROM Employees e
INNER JOIN Departments d ON e.DepartmentId = d.DepartmentId;

проверка
SELECT * FROM vw_EmployeeDetails;

### Результат
Анна Иванова	Разработка	71500.00
Пётр Сидоров	Разработка	79200.00
Мария Кузнецова	Разработка	60500.00
Иван Петров	Разработка	52800.00
Ольга Смирнова	Аналитика	62000.00
Дмитрий Орлов	Аналитика	59000.00
Елена Волкова	Аналитика	53000.00
Сергей Морозов	Тестирование	45000.00
Наталья Соколова	Тестирование	43000.00
Алексей Лебедев	Менеджмент	90000.00

### Вывод
Представление (VIEW) — это сохраненный запрос, который выглядит как виртуальная таблица. К нему можно обращаться так же, как к обычной таблице.


## Task 20 — Индекс на Salary
### SQL
CREATE INDEX IX_Employees_Salary ON Employees(Salary);

### Результат
[IX_Employees_Salary]
(не уникальный, некластеризованный
### Вывод
Индекс ускоряет поиск и сортировку по полю Salary, работая как оглавление в книге.


## Task 21 — Хранимая процедура 
### SQL
CREATE PROCEDURE GetEmployeesByDept
    @DeptId INT
AS
BEGIN
    SELECT EmployeeName, Salary 
    FROM Employees 
    WHERE DepartmentId = @DeptId;
END;

EXEC GetEmployeesByDept @DeptId = 1;

### Результат
Создана процедура, которая выводит всех сотрудников заданного отдела.

## Task 22 —  Скалярная функция 
### SQL

DROP FUNCTION IF EXISTS GetProjectCount;
GO


CREATE FUNCTION GetProjectCount (@EmpId INT)
RETURNS INT
AS
BEGIN
    DECLARE @Count INT;
  
    SET @Count = (SELECT COUNT(*) FROM EmployeeProjects WHERE EmployeeId = @EmpId);
    
    RETURN @Count;
END;
GO

SELECT dbo.GetProjectCount(1) AS ProjectCount;

### Результат
Функция, которая возвращает количество проектов у сотрудника.

## Task 23 —  Оконные функции 
### SQL
SELECT 
    EmployeeName, 
    DepartmentId, 
    Salary,
    ROW_NUMBER() OVER(PARTITION BY DepartmentId ORDER BY Salary DESC) AS RankInDept
FROM Employees;

### Результат
Пётр Сидоров	1	79200.00	1
Анна Иванова	1	71500.00	2
Мария Кузнецова	1	60500.00	3
Иван Петров	1	52800.00	4
Ольга Смирнова	2	62000.00	1
Дмитрий Орлов	2	59000.00	2
Елена Волкова	2	53000.00	3
Сергей Морозов	3	45000.00	1
Наталья Соколова	3	43000.00	2
Алексей Лебедев	4	90000.00	1

## Task 24 —  CTE (Common Table Expression)
### SQL
WITH DeptStats AS (
    SELECT DepartmentId, AVG(Salary) AS AvgSalary
    FROM Employees
    GROUP BY DepartmentId
)
SELECT d.DepartmentName, ds.AvgSalary
FROM DeptStats ds
JOIN Departments d ON ds.DepartmentId = d.DepartmentId;

### Результат
Разработка	66000.000000
Аналитика	58000.000000
Тестирование	44000.000000
Менеджмент	90000.000000

## Task 25 —  CASE (Условная логика)
### SQL
WITH DeptStats AS (
    SELECT DepartmentId, AVG(Salary) AS AvgSalary
    FROM Employees
    GROUP BY DepartmentId
)
SELECT d.DepartmentName, ds.AvgSalary
FROM DeptStats ds
JOIN Departments d ON ds.DepartmentId = d.DepartmentId;

### Результат
Анна Иванова	71500.00	Высокая
Пётр Сидоров	79200.00	Высокая
Мария Кузнецова	60500.00	Средняя
Иван Петров	52800.00	Средняя
Ольга Смирнова	62000.00	Средняя
Дмитрий Орлов	59000.00	Средняя
Елена Волкова	53000.00	Средняя
Сергей Морозов	45000.00	Низкая
Наталья Соколова	43000.00	Низкая
Алексей Лебедев	90000.00	Высокая

## Task 26 —  Работа с датами
### SQL
SELECT 
    EmployeeName, 
    HireDate, 
    DATEDIFF(YEAR, HireDate, GETDATE()) AS YearsOfService
FROM Employees;

### Результат
Анна Иванова	2020-03-15	6
Пётр Сидоров	2019-07-01	7
Мария Кузнецова	2021-11-20	5
Иван Петров	2023-02-10	3
Ольга Смирнова	2020-09-05	6
Дмитрий Орлов	2022-04-18	4
Елена Волкова	2023-06-01	3
Сергей Морозов	2021-12-12	5
Наталья Соколова	2024-01-15	2
Алексей Лебедев	2018-05-25	8

## Task 27 —   Транзакция с обработкой ошибок (TRY/CATCH)
### SQL
BEGIN TRY
    BEGIN TRANSACTION;
    UPDATE Employees SET Salary = Salary * 1.05 WHERE DepartmentId = 1;
    COMMIT TRANSACTION;
    SELECT N'Зарплаты успешно обновлены.' AS Result;
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    SELECT N'Ошибка! Изменения отменены.' AS Result;
END CATCH;

### Результат
Зарплаты успешно обновлены.



