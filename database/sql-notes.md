# SQL notes
> Учебная БД: PracticeDB. Скрипт создания — `init.sql`.

## Task 01 — Создание базы и таблиц
### SQL
sql
CREATE DATABASE PracticeDB;
GO
USE PracticeDB;
GO
CREATE TABLE Departments (
    DepartmentId   INT PRIMARY KEY IDENTITY(1,1),
    DepartmentName NVARCHAR(100) NOT NULL
);
CREATE TABLE Employees (
    EmployeeId   INT PRIMARY KEY IDENTITY(1,1),
    EmployeeName NVARCHAR(100) NOT NULL,
    Salary       DECIMAL(10,2) NOT NULL,
    HireDate     DATE NOT NULL,
    DepartmentId INT NOT NULL,
    ManagerId    INT NULL,
    CONSTRAINT FK_Employees_Departments FOREIGN KEY (DepartmentId) REFERENCES Departments(DepartmentId),
    CONSTRAINT FK_Employees_Manager FOREIGN KEY (ManagerId) REFERENCES Employees(EmployeeId)
);
CREATE TABLE Projects (
    ProjectId   INT PRIMARY KEY IDENTITY(1,1),
    ProjectName NVARCHAR(100) NOT NULL,
    StartDate   DATE NOT NULL
);
CREATE TABLE EmployeeProjects (
    EmployeeId INT NOT NULL,
    ProjectId  INT NOT NULL,
    Hours      INT NOT NULL DEFAULT 0,
    CONSTRAINT PK_EmployeeProjects PRIMARY KEY (EmployeeId, ProjectId),
    CONSTRAINT FK_EP_Employees FOREIGN KEY (EmployeeId) REFERENCES Employees(EmployeeId),
    CONSTRAINT FK_EP_Projects  FOREIGN KEY (ProjectId)  REFERENCES Projects(ProjectId)
);
### Результат
Все 4 таблицы успешно созданы. Связи между ними настроены.

### Вывод
База данных создана, структура таблиц соответствует требованиям задания.

### Task 02 — INSERT: заполнение данными
Данные вставлены
### Результат
Данные успешно добавлены. SELECT COUNT(*) показывает 4 отдела, 10 сотрудников, 5 проектов и 14 назначений.

### Вывод
База заполнена тестовыми данными, готовыми для выполнения запросов.


### Task 04 — ORDER BY + TOP


SELECT TOP 3 EmployeeName, Salary 
FROM Employees 
ORDER BY Salary DESC;


SELECT TOP 5 EmployeeName, HireDate 
FROM Employees 
ORDER BY HireDate ASC;

SELECT EmployeeName, DepartmentId, Salary 
FROM Employees 
ORDER BY DepartmentId ASC, Salary DESC;
### Результат
Алексей Лебедев	95000.00
Анна Иванова	86500.00
Пётр Сидоров	84200.00

Алексей Лебедев	2018-05-25
Пётр Сидоров	2019-07-01
Анна Иванова	2020-03-15
Ольга Смирнова	2020-09-05
Мария Кузнецова	2021-11-20


Анна Иванова	1	86500.00
Пётр Сидоров	1	84200.00
Мария Кузнецова	1	65500.00
Иван Тестов	1	60000.00
Иван Петров	1	52800.00
Ольга Смирнова	2	66000.00
Дмитрий Орлов	2	58000.00
Елена Волкова	2	52000.00
Сергей Морозов	3	50000.00
Алексей Лебедев	4	95000.00

### Вывод
Операторы ORDER BY и TOP позволяют сортировать данные и ограничивать количество выводимых строк.


### Task 05 — Агрегация: COUNT, SUM, AVG, MIN, MAX

SELECT COUNT(*) AS TotalEmployees FROM Employees;
SELECT SUM(Salary) AS TotalSalary FROM Employees;
SELECT ROUND(AVG(Salary), 2) AS AvgSalary FROM Employees;
SELECT MIN(Salary) AS MinSalary, MAX(Salary) AS MaxSalary FROM Employees;
SELECT COUNT(*) AS RichEmployees FROM Employees WHERE Salary > 50000;

### Результат
10

670000.00

67000.000000

50000.00	95000.00

9
### Вывод
Агрегатные функции используются для вычисления статистических показателей по всей таблице.

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


## Task 11 — Self-join: иерархия руководителей
### SQL
SELECT 
    e.EmployeeName, 
    m.EmployeeName AS ManagerName
FROM Employees e
LEFT JOIN Employees m ON e.ManagerId = m.EmployeeId;

### Результат
Анна Иванова	NULL
Пётр Сидоров	NULL
Мария Кузнецова	Пётр Сидоров
Иван Петров	Анна Иванова
Ольга Смирнова	NULL
Дмитрий Орлов	Ольга Смирнова
Елена Волкова	NULL
Сергей Морозов	NULL
Наталья Соколова	Алексей Лебедев
Алексей Лебедев	NULL

### Вывод
 у сотрудников с ManagerId появится имя руководителя, у остальных — NULL


## Task 12 — Подзапросы в WHERE
### SQL

SELECT EmployeeName, Salary
FROM Employees
WHERE Salary > (SELECT AVG(Salary) FROM Employees);

SELECT EmployeeName, Salary
FROM Employees
WHERE Salary = (SELECT MAX(Salary) FROM Employees);

### Результат
Анна Иванова	65000.00
Пётр Сидоров	72000.00
Ольга Смирнова	61000.00
Алексей Лебедев	90000.00

Алексей Лебедев	90000.00
### Вывод
выведутся сотрудники с зарплатой выше средней и максимальной



## Task 13 — Скалярные подзапросы в SELECT
### SQL
SELECT 
    EmployeeName, 
    Salary, 
    DepartmentId,
    (SELECT AVG(Salary) FROM Employees e2 WHERE e2.DepartmentId = e1.DepartmentId) AS AvgDeptSalary,
    ROUND(Salary - (SELECT AVG(Salary) FROM Employees e2 WHERE e2.DepartmentId = e1.DepartmentId), 2) AS Diff
FROM Employees e1;

### Результат
Анна Иванова	65000.00	1	60000.000000	5000.000000
Пётр Сидоров	72000.00	1	60000.000000	12000.000000
Мария Кузнецова	55000.00	1	60000.000000	-5000.000000
Иван Петров	48000.00	1	60000.000000	-12000.000000
Ольга Смирнова	61000.00	2	57000.000000	4000.000000
Дмитрий Орлов	58000.00	2	57000.000000	1000.000000
Елена Волкова	52000.00	2	57000.000000	-5000.000000
Сергей Морозов	45000.00	3	44000.000000	1000.000000
Наталья Соколова	43000.00	3	44000.000000	-1000.000000
Алексей Лебедев	90000.00	4	90000.000000	0.000000

### Вывод
скалярный подзапрос и разница



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



## Task 15 —  UNION / UNION ALL
### SQL
SELECT EmployeeName, Salary FROM Employees WHERE Salary > 60000
UNION
SELECT EmployeeName, Salary FROM Employees WHERE HireDate < '2022-01-01';

SELECT EmployeeName, Salary FROM Employees WHERE Salary > 60000
UNION ALL
SELECT EmployeeName, Salary FROM Employees WHERE HireDate < '2022-01-01';

### Результат
Алексей Лебедев	90000.00
Анна Иванова	65000.00
Мария Кузнецова	55000.00
Ольга Смирнова	61000.00
Пётр Сидоров	72000.00
Сергей Морозов	45000.00

Анна Иванова	65000.00
Пётр Сидоров	72000.00
Ольга Смирнова	61000.00
Алексей Лебедев	90000.00
Анна Иванова	65000.00
Пётр Сидоров	72000.00
Мария Кузнецова	55000.00
Ольга Смирнова	61000.00
Сергей Морозов	45000.00
Алексей Лебедев	90000.00

### Вывод
union убирает дубликаты, а union all - нет


## Task 16 — UPDATE: изменение данных
### SQL

SELECT EmployeeName, Salary FROM Employees WHERE DepartmentId = 1;
UPDATE Employees SET Salary = Salary * 1.10 WHERE DepartmentId = 1;
SELECT EmployeeName, Salary FROM Employees WHERE DepartmentId = 1; 


UPDATE Employees SET Salary = Salary + 5000 WHERE HireDate < '2022-01-01';


SELECT EmployeeName, HireDate FROM Employees WHERE EmployeeId = 4;
UPDATE Employees SET HireDate = '2023-03-01' WHERE EmployeeId = 4;
SELECT EmployeeName, HireDate FROM Employees WHERE EmployeeId = 4; 

### Результат 
Анна Иванова	65000.00
Пётр Сидоров	72000.00
Мария Кузнецова	55000.00
Иван Петров	48000.00

Анна Иванова	71500.00
Пётр Сидоров	79200.00
Мария Кузнецова	60500.00
Иван Петров	52800.00

Иван Петров	2023-02-10

Иван Петров	2023-03-01
### Вывод
данные изменились


## Task 17 — DELETE: удаление данных
### SQL

DELETE FROM EmployeeProjects WHERE Hours = 0;


DELETE FROM EmployeeProjects WHERE EmployeeId = 9;
DELETE FROM Employees WHERE EmployeeId = 9;

SELECT * INTO TempEmployees FROM Employees;
DELETE FROM TempEmployees;
SELECT COUNT(*) FROM TempEmployees; 
DROP TABLE TempEmployees;
### Результат (до и после)
0
### Вывод
Ничего, пустой результат


## Task 18 — Транзакции
### SQL

BEGIN TRANSACTION;
UPDATE Employees SET Salary = Salary + 10000 WHERE EmployeeId = 1;
SELECT EmployeeName, Salary FROM Employees WHERE EmployeeId = 1;
ROLLBACK TRANSACTION;
SELECT EmployeeName, Salary FROM Employees WHERE EmployeeId = 1; 


BEGIN TRANSACTION;
UPDATE Employees SET Salary = Salary + 10000 WHERE EmployeeId = 1;
COMMIT TRANSACTION;
SELECT EmployeeName, Salary FROM Employees WHERE EmployeeId = 1; 

### Результат
Анна Иванова	86500.00

Анна Иванова	76500.00

Анна Иванова	86500.00

### Вывод
Транзакция позволяет безопасно тестировать изменения. ROLLBACK отменил все изменения, сделанные внутри транзакции.



## Task 19 — Представления (VIEW)
### SQL


CREATE VIEW vw_EmployeeDetails AS
SELECT 
    e.EmployeeName, 
    d.DepartmentName, 
    p.ProjectName, 
    ep.Hours
FROM Employees e
INNER JOIN Departments d ON e.DepartmentId = d.DepartmentId
LEFT JOIN EmployeeProjects ep ON e.EmployeeId = ep.EmployeeId
LEFT JOIN Projects p ON ep.ProjectId = p.ProjectId;
GO

SELECT * FROM vw_EmployeeDetails;
GO

DROP VIEW vw_EmployeeDetails;
GO

### Результат
Анна Иванова	Разработка	CRM System	120
Анна Иванова	Разработка	Data Warehouse	80
Пётр Сидоров	Разработка	CRM System	150
Пётр Сидоров	Разработка	AI Assistant	60
Мария Кузнецова	Разработка	Mobile App	100
Иван Петров	Разработка	Mobile App	90
Иван Петров	Разработка	Legacy Refactor	40
Ольга Смирнова	Аналитика	Data Warehouse	110
Дмитрий Орлов	Аналитика	Data Warehouse	70
Дмитрий Орлов	Аналитика	AI Assistant	50
Елена Волкова	Аналитика	Mobile App	95
Сергей Морозов	Тестирование	Legacy Refactor	130
Алексей Лебедев	Менеджмент	CRM System	30
Алексей Лебедев	Менеджмент	AI Assistant	200

### Вывод
Представление (VIEW) — это сохраненный запрос, который выглядит как виртуальная таблица. К нему можно обращаться так же, как к обычной таблице.


## Task 20 —  Индексы: создание и анализ
### SQL

SET STATISTICS IO ON;
SELECT * FROM Employees WHERE DepartmentId = 1 AND Salary > 50000;


CREATE INDEX IX_Employees_DepartmentId ON Employees(DepartmentId);
CREATE INDEX IX_Employees_Salary ON Employees(Salary);


SET STATISTICS IO ON;
SELECT * FROM Employees WHERE DepartmentId = 1 AND Salary > 50000;
### Результат
Было:
Table 'Employees'. Scan count 1, logical reads 2, physical reads 1, page server reads 0, read-ahead reads 0, page server read-ahead reads 0, lob logical reads 0, lob physical reads 0, lob page server reads 0, lob read-ahead reads 0, lob page server read-ahead reads 0.
Стало:
Table 'Employees'. Scan count 1, logical reads 2, physical reads 0, page server reads 0, read-ahead reads 0, page server read-ahead reads 0, lob logical reads 0, lob physical reads 0, lob page server reads 0, lob read-ahead reads 0, lob page server read-ahead reads 0.

### Вывод
если посмотреть на вкладку "сообщения", то можно увидеть строку "logical reads". после создания индексов это число должно уменьшится.


## Task 21 — Две хранимые процедуры
### SQL
DROP PROCEDURE IF EXISTS sp_GetEmployeesByDepartment;
GO

CREATE PROCEDURE sp_GetEmployeesByDepartment
    @DeptId INT
AS
BEGIN
    SELECT EmployeeName, Salary 
    FROM Employees 
    WHERE DepartmentId = @DeptId;
END;
GO

EXEC sp_GetEmployeesByDepartment @DeptId = 1;
GO

DROP PROCEDURE IF EXISTS sp_GetAvgSalaryByDept;
GO

CREATE PROCEDURE sp_GetAvgSalaryByDept
    @DeptId INT,
    @AvgSalary DECIMAL(10,2) OUTPUT
AS
BEGIN
    SELECT @AvgSalary = AVG(Salary) 
    FROM Employees 
    WHERE DepartmentId = @DeptId;
END;
GO

DECLARE @Avg DECIMAL(10,2);
EXEC sp_GetAvgSalaryByDept @DeptId = 1, @AvgSalary = @Avg OUTPUT;
SELECT @Avg AS AverageSalary;

### Результат
Анна Иванова	86500.00
Пётр Сидоров	84200.00
Мария Кузнецова	65500.00
Иван Петров	52800.00

### Вывод
созданы 2 процедуры

## Task 22 — Функции: скалярная и табличная
### SQL
DROP FUNCTION IF EXISTS fn_GetAnnualSalary;
GO

CREATE FUNCTION fn_GetAnnualSalary (@MonthlySalary DECIMAL(10,2))
RETURNS DECIMAL(10,2)
AS
BEGIN
    RETURN @MonthlySalary * 12;
END;
GO

SELECT EmployeeName, Salary, dbo.fn_GetAnnualSalary(Salary) AS AnnualSalary 
FROM Employees;
GO

DROP FUNCTION IF EXISTS fn_GetEmployeesBySalaryRange;
GO

CREATE FUNCTION fn_GetEmployeesBySalaryRange (@Min DECIMAL(10,2), @Max DECIMAL(10,2))
RETURNS TABLE
AS
RETURN
(
    SELECT EmployeeName, Salary 
    FROM Employees 
    WHERE Salary BETWEEN @Min AND @Max
);
GO

SELECT * FROM dbo.fn_GetEmployeesBySalaryRange(50000, 80000);
GO

### Результат
Анна Иванова	86500.00	1038000.00
Пётр Сидоров	84200.00	1010400.00
Мария Кузнецова	65500.00	786000.00
Иван Петров	52800.00	633600.00
Ольга Смирнова	66000.00	792000.00
Дмитрий Орлов	58000.00	696000.00
Елена Волкова	52000.00	624000.00
Сергей Морозов	50000.00	600000.00
Алексей Лебедев	95000.00	1140000.00

Мария Кузнецова	65500.00
Иван Петров	52800.00
Ольга Смирнова	66000.00
Дмитрий Орлов	58000.00
Елена Волкова	52000.00
Сергей Морозов	50000.00

## Task 23 —  Оконные функции 
### SQL
SELECT 
    EmployeeName, 
    DepartmentId, 
    Salary,
    ROW_NUMBER() OVER(ORDER BY Salary DESC) AS RowNum,
    RANK() OVER(PARTITION BY DepartmentId ORDER BY Salary DESC) AS RankInDept,
    DENSE_RANK() OVER(PARTITION BY DepartmentId ORDER BY Salary DESC) AS DenseRankInDept
FROM Employees;

### Результат
Анна Иванова	1	86500.00	2	1	1
Пётр Сидоров	1	84200.00	3	2	2
Мария Кузнецова	1	65500.00	5	3	3
Иван Петров	1	52800.00	7	4	4
Ольга Смирнова	2	66000.00	4	1	1
Дмитрий Орлов	2	58000.00	6	2	2
Елена Волкова	2	52000.00	8	3	3
Сергей Морозов	3	50000.00	9	1	1
Алексей Лебедев	4	95000.00	1	1	1

## Task 24 —  Оконные агрегаты
### SQL
SELECT 
    EmployeeName, 
    DepartmentId, 
    Salary,
    SUM(Salary) OVER(PARTITION BY DepartmentId) AS TotalDeptSalary,
    AVG(Salary) OVER(PARTITION BY DepartmentId) AS AvgDeptSalary,
    Salary - AVG(Salary) OVER(PARTITION BY DepartmentId) AS DiffFromAvg
FROM Employees;

### Результат
Анна Иванова	1	86500.00	289000.00	72250.000000	14250.000000
Пётр Сидоров	1	84200.00	289000.00	72250.000000	11950.000000
Мария Кузнецова	1	65500.00	289000.00	72250.000000	-6750.000000
Иван Петров	1	52800.00	289000.00	72250.000000	-19450.000000
Ольга Смирнова	2	66000.00	176000.00	58666.666666	7333.333334
Дмитрий Орлов	2	58000.00	176000.00	58666.666666	-666.666666
Елена Волкова	2	52000.00	176000.00	58666.666666	-6666.666666
Сергей Морозов	3	50000.00	50000.00	50000.000000	0.000000
Алексей Лебедев	4	95000.00	95000.00	95000.000000	0.000000

## Task 25 —  CTE
### SQL
SELECT EmployeeName, Salary
FROM Employees
WHERE Salary > (SELECT AVG(Salary) FROM Employees);

WITH AvgSalary AS (
    SELECT AVG(Salary) AS AvgSal FROM Employees
)
SELECT e.EmployeeName, e.Salary
FROM Employees e
CROSS JOIN AvgSalary a
WHERE e.Salary > a.AvgSal;

WITH DeptAvg AS (
    SELECT DepartmentId, AVG(Salary) AS AvgSal
    FROM Employees
    GROUP BY DepartmentId
)
SELECT TOP 3 DepartmentId, AvgSal
FROM DeptAvg
ORDER BY AvgSal DESC;
### Результат
Пётр Сидоров	84200.00
Анна Иванова	86500.00
Алексей Лебедев	95000.00

Пётр Сидоров	84200.00
Анна Иванова	86500.00
Алексей Лебедев	95000.00

4	95000.000000
1	72250.000000
2	58666.666666

## Task 26 —  CASE (условная логика)
### SQL
SELECT 
    EmployeeName, 
    Salary,
    CASE 
        WHEN Salary > 60000 THEN 'High'
        WHEN Salary BETWEEN 40000 AND 60000 THEN 'Medium'
        ELSE 'Low'
    END AS SalaryLevel,
    HireDate,
    CASE
        WHEN HireDate < '2020-01-01' THEN 'Senior'
        WHEN HireDate BETWEEN '2020-01-01' AND '2022-12-31' THEN 'Mid'
        ELSE 'Junior'
    END AS Tenure
FROM Employees;

### Результат
Анна Иванова	86500.00	High	2020-03-15	Mid
Пётр Сидоров	84200.00	High	2019-07-01	Senior
Мария Кузнецова	65500.00	High	2021-11-20	Mid
Иван Петров	52800.00	Medium	2023-03-01	Junior
Ольга Смирнова	66000.00	High	2020-09-05	Mid
Дмитрий Орлов	58000.00	Medium	2022-04-18	Mid
Елена Волкова	52000.00	Medium	2023-06-01	Junior
Сергей Морозов	50000.00	Medium	2021-12-12	Mid
Алексей Лебедев	95000.00	High	2018-05-25	Senior

## Task 27 —   Работа с датами
### SQL
SELECT 
    EmployeeName,
    DATEDIFF(YEAR, HireDate, GETDATE()) AS YearsOfService,
    DATEDIFF(MONTH, HireDate, GETDATE()) AS MonthsOfService,
    DATEADD(YEAR, 1, HireDate) AS DateAfterOneYear,
    FORMAT(HireDate, 'dd.MM.yyyy') AS FormattedHireDate
FROM Employees;

### Результат
Анна Иванова	6	80	2021-03-15	15.03.2020
Пётр Сидоров	7	88	2020-07-01	01.07.2019
Мария Кузнецова	5	60	2022-11-20	20.11.2021
Иван Петров	3	44	2024-03-01	01.03.2023
Ольга Смирнова	6	74	2021-09-05	05.09.2020
Дмитрий Орлов	4	55	2023-04-18	18.04.2022
Елена Волкова	3	41	2024-06-01	01.06.2023
Сергей Морозов	5	59	2022-12-12	12.12.2021
Алексей Лебедев	8	102	2019-05-25	25.05.2018

## Task 28 —  C# + SQL: подключение и SELECT (Асинхронно)
### Задача
Подключиться к базе данных из C# и вывести список сотрудников.

### Код C# (Task28_CSharpSql.cs)
using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using CSharpPraktikum.Common;

namespace CSharpPraktikum.Tasks.Level3;

public class Task28_CSharpSql : ITask
{
    public string Title => "28. C# + SQL: подключение и SELECT";
    public string Description => "Асинхронное подключение к PracticeDB и чтение сотрудников.";
    public string[] Requirements => new[] { "OpenAsync", "SqlDataReader", "Обработка SqlException" };

    public void Run()
    {
      
        RunAsync().GetAwaiter().GetResult();
    }

    private async Task RunAsync()
    {
        string connStr = Environment.GetEnvironmentVariable("PRACTICE_DB_CONNECTION")
                         ?? throw new Exception("Переменная PRACTICE_DB_CONNECTION не найдена!");
        try
        {
            using var connection = new SqlConnection(connStr);

         
            await connection.OpenAsync();

            using var cmd = new SqlCommand("SELECT EmployeeName, Salary FROM Employees", connection);
            using var reader = await cmd.ExecuteReaderAsync();

            Console.WriteLine("Сотрудники (получено асинхронно):");
            while (await reader.ReadAsync())
            {
                Console.WriteLine($"{reader.GetString(0)}: {reader.GetDecimal(1)} руб.");
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"Ошибка SQL: {ex.Message}");
        }
    }
}

### Результат выполнения
Сотрудники (получено асинхронно):
Анна Иванова: 86500,00 руб.
Пётр Сидоров: 84200,00 руб.
Мария Кузнецова: 65500,00 руб.
Иван Петров: 52800,00 руб.
Ольга Смирнова: 66000,00 руб.
Дмитрий Орлов: 58000,00 руб.
Елена Волкова: 52000,00 руб.
Сергей Морозов: 50000,00 руб.
Алексей Лебедев: 95000,00 руб.
Иван Тестов: 60000,00 руб.

## Task 29 — C# + SQL: параметризованный INSERT
### Задача
Добавить нового сотрудника или отдел из C#.

### Код C# (Task29_CSharpInsert.cs)
using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using CSharpPraktikum.Common;

namespace CSharpPraktikum.Tasks.Level3;

public class Task29_CSharpInsert : ITask
{
    public string Title => "29. C# + SQL: параметризованный INSERT";
    public string Description => "Добавление сотрудника в базу с защитой от SQL-инъекций.";
    public string[] Requirements => new[] { "Параметризованный запрос", "SELECT для подтверждения" };

    public void Run()
    {
        RunAsync().GetAwaiter().GetResult();
    }

    private async Task RunAsync()
    {
        string connStr = Environment.GetEnvironmentVariable("PRACTICE_DB_CONNECTION")
                         ?? throw new Exception("Переменная PRACTICE_DB_CONNECTION не найдена!");
        try
        {
            using var connection = new SqlConnection(connStr);
            await connection.OpenAsync();

            string insertQuery = @"
                INSERT INTO Employees (EmployeeName, Salary, HireDate, DepartmentId) 
                VALUES (@Name, @Salary, @HireDate, @DeptId)";

            using var cmd = new SqlCommand(insertQuery, connection);

            
            cmd.Parameters.AddWithValue("@Name", "Иван Тестов");
            cmd.Parameters.AddWithValue("@Salary", 60000m);
            cmd.Parameters.AddWithValue("@HireDate", DateTime.Today);
            cmd.Parameters.AddWithValue("@DeptId", 1);

            int rows = await cmd.ExecuteNonQueryAsync();
            Console.WriteLine($"Успешно добавлено строк: {rows}");


            using var checkCmd = new SqlCommand("SELECT EmployeeName, Salary FROM Employees WHERE EmployeeName = 'Иван Тестов'", connection);
            using var reader = await checkCmd.ExecuteReaderAsync();

            Console.WriteLine("\nПроверка (новый сотрудник):");
            while (await reader.ReadAsync())
            {
                Console.WriteLine($"{reader.GetString(0)}: {reader.GetDecimal(1)} руб.");
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"Ошибка SQL: {ex.Message}");
        }
    }
}

### Результат выполнения
Успешно! Добавлено строк: 1

## Task 30 — C# + SQL: мини-отчёт по отделам
### Задача
вывести текстовый отчет

### Код C# (Task30_CSharpReport.cs)
using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using CSharpPraktikum.Common;

namespace CSharpPraktikum.Tasks.Level3;

public class Task30_CSharpReport : ITask
{
    public string Title => "30. C# + SQL: мини-отчёт по отделам";
    public string Description => "Отчёт по отделам: количество сотрудников и средняя зарплата.";
    public string[] Requirements => new[] { "LEFT JOIN", "GROUP BY", "Форматированный вывод" };

    public void Run()
    {
        RunAsync().GetAwaiter().GetResult();
    }

    private async Task RunAsync()
    {
        string connStr = Environment.GetEnvironmentVariable("PRACTICE_DB_CONNECTION")
                         ?? throw new Exception("Переменная PRACTICE_DB_CONNECTION не найдена!");
        try
        {
            using var connection = new SqlConnection(connStr);
            await connection.OpenAsync();

            string query = @"
                SELECT 
                    d.DepartmentName, 
                    COUNT(e.EmployeeId) AS EmpCount, 
                    AVG(e.Salary) AS AvgSalary 
                FROM Departments d 
                LEFT JOIN Employees e ON d.DepartmentId = e.DepartmentId 
                GROUP BY d.DepartmentName";

            using var cmd = new SqlCommand(query, connection);
            using var reader = await cmd.ExecuteReaderAsync();

            Console.WriteLine("==================================================");
            Console.WriteLine("ОТЧЕТ ПО ОТДЕЛАМ");
            Console.WriteLine("==================================================");
            Console.WriteLine($"{"Отдел",-15} | {"Сотрудников",12} | {"Средняя ЗП",12}");
            Console.WriteLine(new string('-', 46));

            while (await reader.ReadAsync())
            {
                string dept = reader.GetString(0);
                int count = reader.GetInt32(1);

  
                string avgStr = reader.IsDBNull(2)
                    ? "N/A"
                    : reader.GetDecimal(2).ToString("F2");

                Console.WriteLine($"{dept,-15} | {count,12} | {avgStr,12}");
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"Ошибка SQL: {ex.Message}");
        }
    }
}


### Результат выполнения
==================================================
ОТЧЕТ ПО ОТДЕЛАМ
==================================================
Отдел           |  Сотрудников |   Средняя ЗП
----------------------------------------------
Аналитика       |            3 |     58666,67
Менеджмент      |            1 |     95000,00
Новый отдел (из C#) |            0 |          N/A
Разработка      |            5 |     69800,00
Тестирование    |            1 |     50000,00

