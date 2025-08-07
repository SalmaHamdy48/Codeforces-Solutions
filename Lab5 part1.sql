--part-1
--1
SELECT COUNT(*) AS StudentsWithAge
FROM Student
WHERE St_Age IS NOT NULL;

--2
SELECT DISTINCT Ins_name
FROM Instructor

--3
SELECT S.St_Id ,  CONCAT(S.St_Fname , ' ' , S.St_Lname) , D.Dept_Name
FROM Student S
inner join Department D
on S.Dept_Id = D.Dept_Id

--4
SELECT I.Ins_Name , D.Dept_Name
FROM Instructor I
left join Department D
on I.Dept_Id = D.Dept_Id

--5
SELECT CONCAT(S.St_Fname , ' ' , S.St_Lname) , C.Crs_Name
FROM Student S
inner join Stud_Course SC
on S.St_Id = SC.St_Id
inner join Course C
on C.Crs_Id = SC.Crs_Id
WHERE SC.Grade IS NOT NULL

--6
SELECT Count(C.Crs_Id) As Count_of_Courses
FROM Course C
inner join Topic T
on T.Top_Id = C.Top_Id
Group by T.Top_Name

--7
SELECT  MAX(Salary) AS MaxSalary, MIN(Salary) AS MinSalary
FROM Instructor

--8
SELECT Ins_Name, Salary
FROM Instructor
WHERE Salary < (SELECT AVG(ISNULL(Salary, 0)) FROM Instructor)

--9
SELECT TOP 1 D.Dept_Name
FROM Department D
INNER JOIN Instructor I
ON D.Dept_Id = I.Dept_Id
WHERE I.Salary = (SELECT MIN(Salary) FROM Instructor WHERE Salary IS NOT NULL)


--10
SELECT TOP 2 Salary
FROM Instructor
WHERE Salary IS NOT NULL
ORDER BY Salary DESC;

--11
SELECT Ins_Name, COALESCE(Salary, 0 ) AS Amount
FROM Instructor;

--12
SELECT AVG(ISNULL(Salary, 0)) AS AverageSalary
FROM Instructor;

--13
SELECT S.St_Fname, S.St_super
FROM Student S;

--14
WITH RankedSalaries AS (
    SELECT 
        Ins_Name,
        Dept_Id,
        Salary,
        DENSE_RANK() OVER (PARTITION BY Dept_Id ORDER BY Salary DESC) AS RankInDept
    FROM Instructor
    WHERE Salary IS NOT NULL
)
SELECT *
FROM RankedSalaries
WHERE RankInDept <= 2;


--15
WITH RandomStudents AS (
    SELECT 
        St_Id,
        St_Fname,
        St_Lname,
        Dept_Id,
        ROW_NUMBER() OVER (PARTITION BY Dept_Id ORDER BY NEWID()) AS RandRank
    FROM Student
)
SELECT *
FROM RandomStudents
WHERE RandRank = 1;




