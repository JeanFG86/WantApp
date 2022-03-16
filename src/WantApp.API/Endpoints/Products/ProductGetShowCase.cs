namespace WantApp.API.Endpoints.Products;

public class ProductGetShowCase
{
    public static string Template => "/products/showcase";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static IResult Action(ApplicationDbContext context, int page = 1, int row = 10, string orderBy = "name")
    {
        if (row > 10)
            return Results.Problem(title: "Row with max 10", statusCode: 400);

        var queryBase = context.Products.Include(p => p.Category).Where(p => p.HasStock && p.Category.Active).OrderBy(p => p.Name);

        if (orderBy == "name")
            queryBase = queryBase.OrderBy(p => p.Name);
        else if(orderBy == "price")
            queryBase = queryBase.OrderBy(p => p.Price);
        else
            return Results.Problem(title: "Order only by name or price", statusCode: 400);

        var queryFilter = queryBase.Skip((page - 1) * row).Take(row);
        

        var products = queryFilter.ToList();

        var response = products.Select(p => new ProductResponse(p.Name, p.Category.Name, p.Description, p.HasStock, p.Price, p.Active));

        return Results.Ok(response);
    }
}
