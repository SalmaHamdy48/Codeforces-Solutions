
--1 a
SELECT DEPENDENT_NAME AS name , SEX AS gender
FROM DEPENDENT
WHERE SEX = 'F'
UNION
SELECT FNAME , SEX 
FROM EMPLOYEE
WHERE SEX = 'F'

--1 b
SELECT DEPENDENT_NAME AS name , SEX AS gender
FROM DEPENDENT
WHERE SEX = 'm'
UNION
SELECT FNAME , SEX 
FROM EMPLOYEE
WHERE SEX = 'm'

--2
SELECT PNAME AS PRO_NAME , HOURS AS total_hours
FROM PROJECT P
INNER JOIN WORKS_ON W
ON P.PNUMBER = W.PNO

--3
--Display the data of the department which has the smallest employee ID over all employees' ID.
SELECT DNAME , DNUMBER , MGRSSN , MGRSTARTDATE
FROM department D 
INNER JOIN EMPLOYEE E
ON D.DNUMBER = E.DNO
WHERE E.SSN = 
         (SELECT MIN(SSN) 
         FROM EMPLOYEE)

--4
--For each department, retrieve the department name and the maximum, minimum and average salary of its employees.
SELECT D.DNAME , MAX(E.SALARY) AS MAX_SAL , MIN(E.SALARY) AS MIN_SAL , AVG(E.SALARY) AS AVG_SAL
FROM DEPARTMENT D
INNER JOIN EMPLOYEE E
ON D.DNUMBER = E.DNO
GROUP BY D.DNAME

--5
--List the last name of all managers who have no dependents.
SELECT E.LNAME
FROM EMPLOYEE E
INNER JOIN DEPARTMENT D ON E.SSN = D.MGRSSN
LEFT JOIN DEPENDENT DEP ON E.SSN = DEP.ESSN
WHERE DEP.ESSN IS NULL

--6
--For each department-- if its average salary is less than the average salary of all employees
-- display its number, name and number of its employees.

SELECT DNUMBER , DNAME , COUNT(E.SSN) AS NUM_OF_EMP
FROM DEPARTMENT D 
INNER JOIN EMPLOYEE E 
ON D.DNUMBER = E.DNO
GROUP BY D.DNUMBER, DNAME
HAVING AVG(E.SALARY) < (SELECT AVG(SALARY)
                       FROM EMPLOYEE)


--7
--Retrieve a list of employees and the projects they are working on ordered by department and within each department,
--ordered alphabetically by last name, first name.
SELECT E.FNAME , E.LNAME , P.PNAME
FROM EMPLOYEE E
INNER JOIN WORKS_ON W ON W.ESSN = E.SSN
INNER JOIN PROJECT P ON P.PNUMBER = W.PNO
INNER JOIN DEPARTMENT D ON E.DNO = D.DNUMBER
ORDER BY D.DNAME, E.LNAME, E.FNAME;

--8
--Try to get the max 2 salaries using subquery
SELECT SALARY 
FROM EMPLOYEE
WHERE SALARY IN (SELECT TOP 2 SALARY 
                FROM EMPLOYEE
                ORDER BY SALARY DESC)

--9
--Get the full name of employees that is similar to any dependent name
SELECT CONCAT(E.FNAME, ' ' , E.LNAME) AS FULL_NAME
FROM EMPLOYEE E
INNER JOIN DEPENDENT D
ON CONCAT(E.FNAME, ' ' , E.LNAME) = D.DEPENDENT_NAME


--10
--Try to update all salaries of employees who work in Project ‘Al Rabwah’ by 30% 
UPDATE E
SET E.SALARY = E.SALARY * 1.3
FROM EMPLOYEE E
INNER JOIN WORKS_ON W
ON W.ESSN = E.SSN
INNER JOIN PROJECT P 
ON P.PNUMBER = W.PNO
WHERE P.PNAME = 'Al Rabwah';

--11
--Display the employee number and name if at least one of them have dependents (use exists keyword) self-study.
SELECT E.SSN , E.FNAME , E.LNAME
FROM EMPLOYEE E
WHERE EXISTS (
    SELECT *
    FROM DEPENDENT D
    WHERE D.ESSN = E.SSN
);

--DML 
--1
--In the department table insert new department called "DEPT IT" ,
--with id 100, employee with SSN = 112233 as a manager for this department. The start date for this manager is '1-11-2006'
INSERT INTO DEPARTMENT (DNAME , DNUMBER , MGRSSN ,MGRSTARTDATE)
VALUES ('DEPT_IT' , 100 , 112233 , '1-11-2006' )

--2 
--Do what is required if you know that : Mrs.Noha Mohamed(SSN=968574)  moved to be the manager of the new department (id = 100),
--and they give you(your SSN =102672) her position (Dept. 20 manager) 
--A
--First try to update her record in the department table
UPDATE department
SET MGRSSN = 968574 , MGRSTARTDATE = CURRENT_DATE
WHERE DNUMBER = 100 

--B
--Update your record to be department 20 manager.
UPDATE department
SET MGRSSN = 102672
WHERE DNUMBER = 20 

--C
--Update the data of employee number=102660 to be in your teamwork (he will be supervised by you) (your SSN =102672)
UPDATE EMPLOYEE
SET SUPERSSN = 102672
WHERE SSN = 102660


--3
--Unfortunately the company ended the contract with Mr. Kamel Mohamed (SSN=223344) so try to delete
--his data from your database in case you know that you will be temporarily in his position.
--Hint: (Check if Mr. Kamel has dependents, works as a department manager, supervises any employees or works
--in any projects and handle these cases).
SELECT * FROM DEPENDENT WHERE Essn = 223344;
DELETE FROM DEPENDENT WHERE Essn = 223344;
SELECT * FROM DEPARTMENT WHERE MGRSSN = 223344;
UPDATE DEPARTMENT
SET MGRSSN = 102672 
WHERE MGRSSN = 223344;
SELECT * FROM EMPLOYEE WHERE SUPERSSN = 223344;
UPDATE EMPLOYEE
SET SUPERSSN = 102672
WHERE SUPERSSN = 223344;
SELECT * FROM WORKS_ON WHERE Essn = 223344;
DELETE FROM WORKS_ON WHERE Essn = 223344;
DELETE FROM EMPLOYEE WHERE Ssn = 223344;





