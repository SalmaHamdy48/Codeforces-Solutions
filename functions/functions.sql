
--1.Create a scalar function that takes date and returns Month name of that date.
CREATE FUNCTION dbo.GetMonthName (@InputDate DATE)
RETURNS NVARCHAR(20)
AS
BEGIN
    DECLARE @MonthName NVARCHAR(20);

    SET @MonthName = DATENAME(MONTH, @InputDate);

    RETURN @MonthName;
END;


--2.Create a multi-statements table-valued function that takes 2 integers and returns the values between them.
CREATE FUNCTION dbo.GetNumbersBetween
(
    @StartInt INT,
    @EndInt INT
)
RETURNS @Numbers TABLE (Value INT)
AS
BEGIN
    DECLARE @Current INT;
    SET @Current = @StartInt + 1;

    WHILE @Current < @EndInt
    BEGIN
        INSERT INTO @Numbers (Value)
        VALUES (@Current);

        SET @Current = @Current + 1;
    END

    RETURN;
END;

--3.Create inline function that takes Student No and returns Department Name with Student full name.
CREATE FUNCTION dbo.GetStudentDepartment
(
    @StudentId INT
)
RETURNS TABLE
AS
RETURN
(
    SELECT 
        S.St_Fname + ' ' + S.St_Lname AS FullName,
        D.Dept_Name AS DepartmentName
    FROM Student S
    JOIN Department D
        ON S.Dept_Id = D.Dept_Id
    WHERE S.St_Id = @StudentId
);

--4
CREATE FUNCTION dbo.CheckStudentName
(
    @StudentId INT
)
RETURNS NVARCHAR(100)
AS
BEGIN
    DECLARE @FirstName NVARCHAR(50);
    DECLARE @LastName NVARCHAR(50);
    DECLARE @Message NVARCHAR(100);

    
    SELECT 
        @FirstName = St_Fname,
        @LastName  = St_Lname
    FROM Student
    WHERE St_Id = @StudentId;

    IF @FirstName IS NULL AND @LastName IS NULL
        SET @Message = 'First name & last name are null';
    ELSE IF @FirstName IS NULL
        SET @Message = 'First name is null';
    ELSE IF @LastName IS NULL
        SET @Message = 'Last name is null';
    ELSE
        SET @Message = 'First name & last name are not null';

    RETURN @Message;
END;


--5.Create inline function that takes integer which represents manager ID and displays department name, Manager Name and hiring date 
CREATE FUNCTION dbo.GetManagerInfo
(
    @ManagerId INT
)
RETURNS TABLE
AS
RETURN
(
    SELECT 
        D.Dept_Name AS DepartmentName,
        I.Ins_Name AS ManagerName,
        D.Manager_hiredate
    FROM Department D
    JOIN Instructor I
        ON D.Dept_Manager = I.Ins_Id
    WHERE D.Dept_Manager = @ManagerId
);


--6
CREATE FUNCTION dbo.GetStudentNameByType
(
    @Type NVARCHAR(20)
)
RETURNS @Result TABLE (Name NVARCHAR(100))
AS
BEGIN
    IF @Type = 'first name'
    BEGIN
        INSERT INTO @Result
        SELECT ISNULL(St_Fname, 'No First Name')
        FROM Student;
    END
    ELSE IF @Type = 'last name'
    BEGIN
        INSERT INTO @Result
        SELECT ISNULL(St_Lname, 'No Last Name')
        FROM Student;
    END
    ELSE IF @Type = 'full name'
    BEGIN
        INSERT INTO @Result
        SELECT ISNULL(St_Fname, '') + ' ' + ISNULL(St_Lname, '')
        FROM Student;
    END

    RETURN;
END;

--7.Write a query that returns the Student No and Student first name without the last char
SELECT 
    St_Id AS StudentNo,
    LEFT(St_Fname, LEN(St_Fname) - 1) AS FirstNameWithoutLastChar
FROM Student;


--8.Wirte query to delete all grades for the students Located in SD Department 
DELETE SC
FROM Stud_Course SC
INNER JOIN Student S
    ON SC.St_Id = S.St_Id
INNER JOIN Department D
    ON S.Dept_Id = D.Dept_Id
WHERE D.Dept_Name = 'SD';
