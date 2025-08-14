--1
CREATE VIEW v_clerk AS
SELECT w.EmpNo, w.ProjectNo, w.Enter_Date
FROM Works_on w
WHERE w.Job = 'Clerk';

--select * from v_clerk;
GO
--2
CREATE VIEW v_without_budget AS
SELECT ProjectNo , ProjectName
FROM HR.Project 
where Budget IS NULL; 

GO
--3
CREATE VIEW v_count AS
SELECT P.ProjectName , COUNT(W.JOB) AS JOB_COUNT
FROM HR.Project P
INNER JOIN Works_on W
ON W.ProjectNo = P.ProjectNo
GROUP BY P.ProjectName;

--SELECT * FROM v_count;

--4
--Create view named ” v_project_p2” that will display the emp# s for the project# ‘p2’
--use the previously created view  “v_clerk”
CREATE VIEW v_project_p2 as
SELECT v.EmpNo
FROM v_clerk v
where ProjectNo = 2

--select * from v_project_p2

--5)modifey the view named  “v_without_budget”  to display all DATA in project p1 and p2 
ALTER VIEW v_without_budget AS
SELECT *
FROM HR.Project P
WHERE P.ProjectNo IN (1 , 2)

--SELECT * FROM v_without_budget

--6)Delete the views  “v_ clerk” and “v_count”
DROP VIEW v_clerk
DROP VIEW v_count

--7)Create view that will display the emp# and emp lastname who works on dept# is ‘d2’
CREATE VIEW V_EMP AS
SELECT E.EmpNo , E.EmpLname
FROM HR.Employee E
WHERE E.EmpNo = 2

--SELECT * FROM Department

--8)Display the employee  lastname that contains letter “J”
--Use the previous view created in Q#7
CREATE VIEW V_LETTERM AS
SELECT E.EmpLname
FROM V_EMP E
WHERE E.EmpLname LIKE '%J%';

--SELECT * FROM V_LETTERM


--9)	Create view named “v_dept” that will display the department# and department name
CREATE VIEW v_dept AS 
SELECT D.DeptNo , D.DeptName 
FROM Department D

--10)using the previous view try enter new department data where dept# is ’d4’ and dept name is ‘Development’
INSERT INTO Department (DeptNo, DeptName)
VALUES ('44', 'Development');

GO
--11
CREATE VIEW v_2006_check AS
SELECT w.EmpNo, w.ProjectNo, w.Enter_Date
FROM Works_on w
WHERE w.Enter_Date >= '2006-01-01' AND w.Enter_Date <= '2006-12-31'
WITH CHECK OPTION;