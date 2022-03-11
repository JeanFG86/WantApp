using Microsoft.AspNetCore.Mvc;
using WantApp.API.Domain.Products;
using WantApp.API.Infra.Data;

namespace WantApp.API.Endpoints.Categories;

public class CategoryPut
{
    public static string Template => "/categories/{id}";
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action([FromRoute] Guid id, CategoryRequest categoryRequest, ApplicationDbContext context)
    {
        var category = context.Categories.FirstOrDefault(c => c.Id == id);

        category.Name = categoryRequest.Name;
        category.Active = categoryRequest.Active;

        context.SaveChanges();

        return Results.Created($"/categories/{category.Id}", category.Id);
    }
}
