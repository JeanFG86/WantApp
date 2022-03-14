using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WantApp.API.Infra.Data;

namespace WantApp.API.Endpoints.Categories;

public class CategoryPut
{
    public static string Template => "/categories/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static IResult Action([FromRoute] Guid id, HttpContext http, CategoryRequest categoryRequest, ApplicationDbContext context)
    {
        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var category = context.Categories.FirstOrDefault(c => c.Id == id);

        if(category == null)
            return Results.NotFound();

        category.EditInfo(category.Name, category.Active, userId);

        if (!category.IsValid)
            return Results.ValidationProblem(category.Notifications.ConvertToProblemDetails());


        context.SaveChanges();

        return Results.Created($"/categories/{category.Id}", category.Id);
    }
}
