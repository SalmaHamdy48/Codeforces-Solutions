USE ShopDB;
GO

CREATE TABLE Category (
    CategoryId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE Product (
    ProductId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    CategoryId INT FOREIGN KEY REFERENCES Category(CategoryId)
);
GO
--view
CREATE VIEW vw_ShowProductsWithCategory AS
select P.ProductId , P.Name AS Product_Name, P.Price , C.CategoryId  ,C.Name AS Category_Name
from Product P
INNER JOIN Category C
ON P.CategoryId = C.CategoryId
GO

--stored procedures for category
--delete
CREATE PROCEDURE sp_DeleteCategory @Id INT
AS
BEGIN
  DELETE FROM Category WHERE CategoryId = @Id
END;
GO

--UPDATE
CREATE PROCEDURE sp_UpdateCategory @Id INT , @Name NVARCHAR(100)
AS
BEGIN
  UPDATE Category 
  SET Name = @Name WHERE CategoryId = @Id
end;
GO

--create 
CREATE PROCEDURE sp_CreateCategory @Name NVARCHAR(100)
AS
BEGIN
    INSERT INTO Category (Name) VALUES (@Name);
    SELECT SCOPE_IDENTITY() AS CategoryId;
END;
GO

--get category
CREATE PROCEDURE sp_GetCategoryById 
    @Id INT
AS
BEGIN
    SELECT CategoryId , Name FROM Category WHERE CategoryId = @Id;
END;
GO

--get all categories
CREATE PROCEDURE sp_GetAllCategories
AS
BEGIN
    SELECT CategoryId, Name 
    FROM Category;
END;
GO



--sp for product
--insert/create
CREATE PROCEDURE sp_InsertProduct 
    @Name NVARCHAR(100),
    @Price DECIMAL(18,2),
    @CategoryId INT
AS
BEGIN
    INSERT INTO Product (Name, Price, CategoryId)
    VALUES (@Name, @Price, @CategoryId);

    SELECT SCOPE_IDENTITY() AS ProductId;
END;
GO

--delete
CREATE PROCEDURE sp_DeleteProduct @Id INT
AS
BEGIN
    DELETE FROM Product WHERE ProductId = @Id;
END;
GO

--update 
CREATE PROCEDURE sp_UpdateProduct
    @Id INT,
    @Name NVARCHAR(100),
    @Price DECIMAL(18,2),
    @CategoryId INT
AS
BEGIN
    UPDATE Product SET Name = @Name, Price = @Price, CategoryId = @CategoryId WHERE ProductId = @Id;
END;
GO

--GET PRODUCT
CREATE PROCEDURE sp_GetProductById
    @Id INT
AS
BEGIN
    SELECT ProductId, Product_Name, Price, CategoryId, Category_Name 
    FROM vw_ShowProductsWithCategory 
    WHERE ProductId = @Id;
END;
GO

--get all products
CREATE PROCEDURE sp_GetAllProducts
AS
BEGIN
    SELECT ProductId, Product_Name, Price, CategoryId, Category_Name 
    FROM vw_ShowProductsWithCategory;
END;
GO

--scalar fun
CREATE FUNCTION fn_GetCategoryCount()
RETURNS INT
AS
BEGIN
    RETURN (

SELECT COUNT(*) FROM Category);
END;
GO

select * from Category
select * from Product

EXEC sp_CreateCategory @Name = 'Test';

DELETE FROM Product;
DELETE FROM Category;
DBCC CHECKIDENT ('Category', RESEED, 0);
DBCC CHECKIDENT ('Product', RESEED, 0);

EXEC sp_CreateCategory @Name = 'Test'

EXEC sp_GetAllCategories;