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