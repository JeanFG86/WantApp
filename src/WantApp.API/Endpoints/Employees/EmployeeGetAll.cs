namespace WantApp.API.Endpoints.Employees;


public class EmployeeGetAll
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => ActionAsync;

    [Authorize(Policy = "Employee005Police")]
    public static async Task<IResult> ActionAsync(int? page, int? rows, QueryAllUsersWithClaimName query)
    {        
        return Results.Ok(await query.ExecuteAsync(page.Value, rows.Value));
    }
}
