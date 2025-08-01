
--1--
SELECT d.DNUMBER AS Dept_ID, d.DNAME AS Dept_Name,
       e.SSN AS Manager_ID,
       e.Fname + ' ' + e.Lname AS Manager_Name
FROM department d
JOIN employee e ON d.MGRSSN = e.SSN;

--2--
SELECT d.DNAME AS Department_Name, p.PNAME AS Project_Name
FROM department d
JOIN project p ON d.DNUMBER = p.DNUM;

--3--
SELECT dp.*, e.Fname + ' ' + e.Lname AS Employee_Name
FROM dependent dp
JOIN employee e ON dp.ESSN = e.SSN;

--4--
SELECT PNUMBER, PNAME, PLOCATION
FROM project
WHERE PLOCATION IN ('Cairo', 'Alex');

--5--
SELECT *
FROM project
WHERE PNAME LIKE 'a%';

--6--
SELECT *
FROM employee
WHERE DNO = 30 AND SALARY BETWEEN 1000 AND 2000;

--7--
SELECT e.Fname + ' ' + e.Lname AS Employee_Name
FROM employee e
JOIN works_on w ON e.SSN = w.ESSN
JOIN project p ON w.PNO = p.PNUMBER
WHERE e.DNO = 10 AND w.HOURS >= 10 AND p.PNAME = 'AL Rabwah';

--8--
SELECT e.Fname + ' ' + e.Lname AS Employee_Name
FROM employee e
JOIN employee s ON e.SUPERSSN = s.SSN
WHERE s.Fname = 'Kamel' AND s.Lname = 'Mohamed';

--9--
SELECT e.Fname + ' ' + e.Lname AS Employee_Name,
       p.PNAME AS Project_Name
FROM employee e
JOIN works_on w ON e.SSN = w.ESSN
JOIN project p ON w.PNO = p.PNUMBER
ORDER BY p.PNAME;

--10--
SELECT p.PNUMBER, d.DNAME AS Dept_Name,
       e.Lname AS Manager_LName, e.Address, e.BDATE
FROM project p
JOIN department d ON p.DNUM = d.DNUMBER
JOIN employee e ON d.MGRSSN = e.SSN
WHERE p.PLOCATION = 'Cairo';

--11--
SELECT *
FROM employee
WHERE SSN IN (SELECT MGRSSN FROM department);

--12--
SELECT e.*, d.Dependent_name, d.Sex, d.BDATE AS Dependent_Bdate, d.Relationship
FROM employee e
LEFT JOIN dependent d ON e.SSN = d.ESSN;

INSERT INTO employee (Fname, Lname, SSN, BDATE, ADDRESS, SEX, SALARY, SUPERSSN, DNO)
VALUES ('Salma', 'Hamdy', '102672', '2003-12-13', 'Cairo, Egypt', 'F', 3000, '123456789', 1);

INSERT INTO EMPLOYEE (FNAME, LNAME, SSN, BDATE, ADDRESS, SEX, DNO)
VALUES ('Farida', 'Hamdy', '102660', '2002-05-10', 'Cairo, Egypt', 'F', 2);

UPDATE EMPLOYEE
SET SALARY = SALARY * 1.2
WHERE SSN = '102672';






