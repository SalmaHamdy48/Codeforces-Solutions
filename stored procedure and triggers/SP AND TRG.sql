
--1
CREATE PROCEDURE dbo.GetStudentsCountPerDepartment
AS
BEGIN
    SELECT 
        D.Dept_Name,
        COUNT(S.St_Id) AS StudentsCount
    FROM Department D
    LEFT JOIN Student S
        ON D.Dept_Id = S.Dept_Id
    GROUP BY D.Dept_Name;
END;
GO

--2
CREATE PROCEDURE dbo.CheckEmployeesInWebsite
AS
BEGIN
    DECLARE @EmpCount INT;
 
    SELECT @EmpCount = COUNT(*)
    FROM CompanyDB.dbo.WORKS_ON W
    INNER JOIN CompanyDB.dbo.PROJECT P ON W.PNO = P.PNUMBER
    WHERE P.PNAME = 'Website';

    IF @EmpCount >= 3
    BEGIN
        PRINT 'The number of employees in the project Website is 3 or more';
    END
    ELSE
    BEGIN
        PRINT 'The following employees work for the project Website';
        
        SELECT E.FNAME, E.LNAME
        FROM CompanyDB.dbo.EMPLOYEE E
        INNER JOIN CompanyDB.dbo.WORKS_ON W
            ON E.SSN = W.ESSN
        INNER JOIN CompanyDB.dbo.PROJECT P
            ON W.PNO = P.PNUMBER
        WHERE P.PNAME = 'Website';
    END
END;
GO

--EXECUTE dbo.CheckEmployeesInWebsite;

--SELECT *
--FROM CompanyDB.dbo.PROJECT;

--3
CREATE PROCEDURE dbo.ReplaceEmployeeInProject
    @OldEmpNo CHAR(9),
    @NewEmpNo CHAR(9),
    @ProjectNo INT
AS
BEGIN
    UPDATE CompanyDB.dbo.WORKS_ON
    SET ESSN = @NewEmpNo
    WHERE ESSN = @OldEmpNo
      AND PNO = @ProjectNo;

    PRINT 'Employee replaced successfully in the specified project.';
END;
GO

--4
ALTER TABLE CompanyDB.dbo.PROJECT
ADD Budget DECIMAL(18,2);

UPDATE CompanyDB.dbo.PROJECT
SET Budget = CASE 
                WHEN PNAME = 'Website' THEN 95000
                WHEN PNAME = 'HR System' THEN 200000
                ELSE 120000
             END;


CREATE TABLE CompanyDB.dbo.ProjectBudgetAudit
(
    ProjectNo INT,
    UserName NVARCHAR(100),
    ModifiedDate DATETIME,
    Budget_Old DECIMAL(18,2),
    Budget_New DECIMAL(18,2)
);

USE CompanyDB
GO 

CREATE TRIGGER trg_ProjectBudgetAudit
ON CompanyDB.dbo.PROJECT
AFTER UPDATE
AS
BEGIN
    -- ?????? ?? ??????? ??? ???? Budget
    IF UPDATE(Budget)
    BEGIN
        INSERT INTO CompanyDB.dbo.ProjectBudgetAudit
        (
            ProjectNo,
            UserName,
            ModifiedDate,
            Budget_Old,
            Budget_New
        )
        SELECT 
            i.PNUMBER,         
            'SALMA',      
            GETDATE(),          
            d.Budget,          
            i.Budget           
        FROM deleted d
        INNER JOIN inserted i
            ON d.PNUMBER = i.PNUMBER;
    END
END;
GO

UPDATE CompanyDB.dbo.PROJECT
SET Budget = 250000
WHERE PNAME = 'Website';

SELECT * 
FROM CompanyDB.dbo.ProjectBudgetAudit;
GO

--5
CREATE TRIGGER dbo.trg_PreventInsertDepartment
ON dbo.Department
AFTER INSERT
AS
BEGIN
PRINT 'You can’t insert a new record in that table'
ROLLBACK TRANSACTION;
END;
GO

--USE ITI 
--GO
--INSERT INTO dbo.Department (Dept_Id, Dept_Name)
--VALUES (25, 'Test Dept2');

--SELECT * FROM dbo.Department;


--6
USE CompanyDB
GO
CREATE TRIGGER dbo.trg_PreventInsertEmployeeInMarch
ON dbo.EMPLOYEE
AFTER INSERT
AS
BEGIN
IF MONTH(GETDATE()) = 3
PRINT 'YOU CANNOT INSERT IN THIS MONTH'
ROLLBACK TRANSACTION;
END;
GO


--7
CREATE TABLE dbo.Student_Audit (
    ServerUserName NVARCHAR(100),
    ActionDate DATETIME,
    Note NVARCHAR(500)
);

USE ITI
GO

CREATE TRIGGER trg_Student_Audit_Insert
ON dbo.Student
AFTER INSERT
AS
BEGIN
    DECLARE @UserName NVARCHAR(100) = SUSER_SNAME();
    DECLARE @TableName NVARCHAR(50) = 'Student';
    DECLARE @KeyValue NVARCHAR(50);

    SELECT @KeyValue = CAST(St_Id AS NVARCHAR)
    FROM inserted;

    INSERT INTO dbo.Student_Audit (ServerUserName, ActionDate, Note)
    VALUES (
        @UserName,
        GETDATE(),
        @UserName + ' Insert New Row with Key=' + @KeyValue + ' in table ' + @TableName
    );
END;
GO

--INSERT INTO dbo.Student (St_Id, St_Fname, St_Lname)
--VALUES (101, 'Omar', 'Ali');

--SELECT * FROM dbo.Student_Audit;

--8
CREATE TRIGGER trg_Student_Audit_Delete
ON dbo.Student
INSTEAD OF DELETE
AS
BEGIN
    DECLARE @UserName NVARCHAR(100) = SUSER_SNAME();

    INSERT INTO dbo.Student_Audit (ServerUserName, ActionDate, Note)
    SELECT 
        @UserName,
        GETDATE(),
        'try to delete Row with Key=' + CAST(d.St_Id AS NVARCHAR)
    FROM deleted d;
END;
GO

--DELETE FROM dbo.Student
--WHERE St_Id = 101;

--SELECT * FROM dbo.Student_Audit;






