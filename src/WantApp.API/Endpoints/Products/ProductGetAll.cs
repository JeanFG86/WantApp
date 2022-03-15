namespace WantApp.API.Endpoints.Products;

public class ProductGetAll
{
    public static string Template => "/products";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolice")]
    public static IResult Action(ApplicationDbContext context)
    {
        var products = context.Products.Include(p => p.Category).OrderBy(p => p.Name).ToList();

        var response = products.Select(p => new ProductResponse(p.Name, p.Category.Name, p.Description, p.HasStock, p.Active));

        return Results.Ok(response);
    }
}
