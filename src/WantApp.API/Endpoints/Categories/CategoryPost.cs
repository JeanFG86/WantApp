using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using WantApp.API.Domain.Products;
using WantApp.API.Infra.Data;

namespace WantApp.API.Endpoints.Categories;

public class CategoryPost
{
    public static string Template => "/categories";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => ActionAsync;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> ActionAsync(CategoryRequest categoryRequest, HttpContext http, ApplicationDbContext context)
    {
        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var category = new Category(categoryRequest.Name, "Test", "Test");


        if (!category.IsValid)
            return Results.ValidationProblem(category.Notifications.ConvertToProblemDetails());

        await context.Categories.AddAsync(category);
        await context.SaveChangesAsync();

        return Results.Created($"/categories/{category.Id}", category.Id);
    }
}
