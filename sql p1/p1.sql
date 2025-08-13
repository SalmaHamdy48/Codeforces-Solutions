--PART1
--1
CREATE VIEW STD_INFO AS
SELECT 
    S.St_Fname + ' ' + S.St_Lname AS FullName,
    C.Crs_Name
FROM Student  S
INNER JOIN Stud_Course SC
    ON S.St_Id = SC.St_Id
INNER JOIN Course C
    ON C.Crs_Id = SC.Crs_Id
WHERE SC.Grade > 50;

--2.Create an Encrypted view that displays manager names and the topics they teach. 
CREATE VIEW Manager_Topics
WITH ENCRYPTION
AS
SELECT I.Ins_Name AS ManagerName,T.Top_Name AS TopicName
FROM Department D
INNER JOIN Instructor I  ON D.Dept_Manager = I.Ins_Id
INNER JOIN Ins_Course IC ON I.Ins_Id = IC.Ins_Id
INNER JOIN Course C ON IC.Crs_Id = C.Crs_Id
INNER JOIN Topic T ON C.Top_Id = T.Top_Id;

--3.Create a view that will display Instructor Name, Department Name for the ‘SD’ or ‘Java’ Department 
CREATE VIEW Ins_info AS
SELECT I.Ins_Name , D.Dept_Name
FROM Instructor I 
INNER JOIN Department D ON D.Dept_Id = I.Dept_Id
WHERE D.Dept_Name IN ('SD' , 'Java' )

--4.Create a view “V1” that displays student data for student who lives in Alex or Cairo. 
--Note: Prevent the users to run the following query 
--Update V1 set st_address=’tanta’
--Where st_address=’alex’;
CREATE VIEW V1 
AS
SELECT *
FROM Student S
WHERE S.St_Address IN ('Alex' , 'Cairo')
WITH CHECK OPTION

UPDATE V1
SET St_Address = 'tanta'
WHERE St_Address = 'Alex'

--6
CREATE CLUSTERED INDEX CIX_Department_HireDate
ON Department(HireDate);


--7
CREATE UNIQUE INDEX UQ_Student_Age
ON Student(St_Age);




