using WantApp.API.Domain.Products;
using WantApp.API.Infra.Data;

namespace WantApp.API.Endpoints.Categories;

public class CategoryPost
{
    public static string Template => "/categories";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(CategoryRequest categoryRequest, ApplicationDbContext context)
    {
        var category = new Category
        {
            Name = categoryRequest.Name,
            CreateBy = "Test",
            CreateOn = DateTime.Today,
            EditedBy = "Test",
            EditedOn = DateTime.Today
        };

        context.Categories.Add(category);
        context.SaveChanges();

        return Results.Created($"/categories/{category.Id}", category.Id);
    }
}
