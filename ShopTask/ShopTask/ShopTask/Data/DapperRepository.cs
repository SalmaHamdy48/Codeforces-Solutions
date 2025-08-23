using System.Data;
using System.Data.SqlClient;
using Dapper;
using ShopTask.Models;

namespace ShopTask.Data;

public class DapperRepository
{
    private readonly string _connectionString =
        "Server=DESKTOP-VIMI624;Database=ShopDB;Trusted_Connection=True;TrustServerCertificate=True;";

    private IDbConnection Connection => new SqlConnection(_connectionString);

    // ---------- Category ----------
    public int CreateCategory(string name)
    {
        using var db = Connection;
        return db.ExecuteScalar<int>(
            "sp_CreateCategory",
            new { Name = name },
            commandType: CommandType.StoredProcedure
        );
    }

    public IEnumerable<Category> GetAllCategories()
    {
        using var db = Connection;
        return db.Query<Category>(
            "sp_GetAllCategories",
            commandType: CommandType.StoredProcedure
        );
    }

    public Category? GetCategoryById(int id)
    {
        using var db = Connection;
        return db.QueryFirstOrDefault<Category>(
            "sp_GetCategoryById",
            new { Id = id },
            commandType: CommandType.StoredProcedure
        );
    }

    public void UpdateCategory(int id, string name)
    {
        using var db = Connection;
        db.Execute(
            "sp_UpdateCategory",
            new { Id = id, Name = name },
            commandType: CommandType.StoredProcedure
        );
    }

    public void DeleteCategory(int id)
    {
        using var db = Connection;
        db.Execute(
            "sp_DeleteCategory",
            new { Id = id },
            commandType: CommandType.StoredProcedure
        );
    }

    // ---------- Product ----------
    public int CreateProduct(string name, decimal price, int categoryId)
    {
        using var db = Connection;
        return db.ExecuteScalar<int>(
            "sp_InsertProduct",
            new { Name = name, Price = price, CategoryId = categoryId },
            commandType: CommandType.StoredProcedure
        );
    }

    public IEnumerable<Product> GetAllProducts()
    {
        using var db = Connection;
        return db.Query<Product>(
            "sp_GetAllProducts",
            commandType: CommandType.StoredProcedure
        );
    }

    public Product? GetProductById(int id)
    {
        using var db = Connection;
        return db.QueryFirstOrDefault<Product>(
            "sp_GetProductById",
            new { Id = id },
            commandType: CommandType.StoredProcedure
        );
    }

    public void UpdateProduct(int id, string name, decimal price, int categoryId)
    {
        using var db = Connection;
        db.Execute(
            "sp_UpdateProduct",
            new { Id = id, Name = name, Price = price, CategoryId = categoryId },
            commandType: CommandType.StoredProcedure
        );
    }

    public void DeleteProduct(int id)
    {
        using var db = Connection;
        db.Execute(
            "sp_DeleteProduct",
            new { Id = id },
            commandType: CommandType.StoredProcedure
        );
    }
}
