using ShopTask.Data;
using ShopTask.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

class Program
{
    static void Main()
    {
        //Entity Framework
        using (var context = new AppDbContext())
        {
            Console.WriteLine("Entity Framework (LINQ) ");

            // Create 
            if (!context.Categories.Any(c => c.Name == "Electronics"))
            {
                var category = new Category { Name = "Electronics" };
                context.Categories.Add(category);
                context.SaveChanges();
            }

            var catId = context.Categories
                               .Where(c => c.Name == "Electronics")
                               .Select(c => c.CategoryId)
                               .FirstOrDefault();

            if (!context.Products.Any(p => p.Name == "Laptop"))
            {
                var product = new Product
                {
                    Name = "Laptop",
                    Price = 15000m,
                    CategoryId = catId
                };
                context.Products.Add(product);
                context.SaveChanges();
            }

            Console.WriteLine("\n--- EF After Create ---");
            foreach (var c in context.Categories.ToList())
                Console.WriteLine($"[EF] Category: {c.CategoryId} - {c.Name}");

            foreach (var p in context.Products.ToList())
                Console.WriteLine($"[EF] Product: {p.ProductId} - {p.Name} - {p.Price} - {p.CategoryId}");

            // Read 
            var singleCat = context.Categories.FirstOrDefault(c => c.CategoryId == catId);
            var singleProd = context.Products.FirstOrDefault(p => p.Name == "Laptop");

            Console.WriteLine($"\n[EF] Read Single: Category = {singleCat?.Name ?? "N/A"}, Product = {singleProd?.Name ?? "N/A"}");

            // Update 
            if (singleCat != null)
                singleCat.Name = "Electronics & phones";

            if (singleProd != null)
            {
                singleProd.Name = "Gaming Laptop";
                singleProd.Price = 18000m;
            }
            context.SaveChanges();

            Console.WriteLine("\n--- EF After Update ---");
            foreach (var c in context.Categories.ToList())
                Console.WriteLine($"[EF] Category: {c.CategoryId} - {c.Name}");

            foreach (var p in context.Products.ToList())
                Console.WriteLine($"[EF] Product: {p.ProductId} - {p.Name} - {p.Price}");

            // Delete
            if (singleProd != null)
                context.Products.Remove(singleProd);

            if (singleCat != null)
            {
                var category = context.Categories
                                      .Include(c => c.Products)
                                      .FirstOrDefault(c => c.CategoryId == singleCat.CategoryId);

                if (category != null && category.Products.Any())
                {
                    Console.WriteLine("Cannot delete Category because it is related to Products");
                }
                else if (category != null)
                {
                    context.Categories.Remove(category);
                }
            }

            context.SaveChanges();

            Console.WriteLine("\n--- EF After Delete ---");
            foreach (var c in context.Categories.ToList())
                Console.WriteLine($"[EF] Category: {c.CategoryId} - {c.Name}");

            foreach (var p in context.Products.ToList())
                Console.WriteLine($"[EF] Product: {p.ProductId} - {p.Name} - {p.Price}");
        }

        //Dapper
        var repo = new DapperRepository();
        Console.WriteLine("\n===== Dapper =====");

        int catIdDapper;
        if (!repo.GetAllCategories().Any(c => c.Name == "Clothes"))
        {
            catIdDapper = repo.CreateCategory("Clothes");
        }
        else
        {
            catIdDapper = repo.GetAllCategories().First(c => c.Name == "Clothes").CategoryId;
        }

        int prodIdDapper = repo.CreateProduct("T-Shirt", 99.99m, catIdDapper);

        Console.WriteLine("\n--- Dapper After Create ---");
        foreach (var c in repo.GetAllCategories())
            Console.WriteLine($"[Dapper] Category: {c.CategoryId} - {c.Name}");

        foreach (var p in repo.GetAllProducts())
            Console.WriteLine($"[Dapper] Product: {p.ProductId} - {p.Name} - {p.Price} - {p.Category_Name ?? "N/A"}");

        // Read 
        var catD = repo.GetCategoryById(catIdDapper);
        var prodD = repo.GetProductById(prodIdDapper);
        Console.WriteLine($"\n[Dapper] Read Single: Category = {catD?.Name ?? "N/A"}, Product = {prodD?.Name ?? "N/A"}");

        // Update
        repo.UpdateCategory(catIdDapper, "Men Clothes");
        repo.UpdateProduct(prodIdDapper, "White T-Shirt", 89.99m, catIdDapper);

        Console.WriteLine("\n--- Dapper After Update ---");
        foreach (var c in repo.GetAllCategories())
            Console.WriteLine($"[Dapper] Category: {c.CategoryId} - {c.Name}");

        foreach (var p in repo.GetAllProducts())
            Console.WriteLine($"[Dapper] Product: {p.ProductId} - {p.Name} - {p.Price} - {p.Category_Name ?? "N/A"}");

        // Delete
        repo.DeleteProduct(prodIdDapper);
        repo.DeleteCategory(catIdDapper);

        Console.WriteLine("\n--- Dapper After Delete ---");
        foreach (var c in repo.GetAllCategories())
            Console.WriteLine($"[Dapper] Category: {c.CategoryId} - {c.Name}");

        foreach (var p in repo.GetAllProducts())
            Console.WriteLine($"[Dapper] Product: {p.ProductId} - {p.Name} - {p.Price} - {p.Category_Name ?? "N/A"}");
    }
}
