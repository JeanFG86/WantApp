using WantApp.API.Domain.Users;

namespace WantApp.API.Endpoints.Employees;

public class EmployeePost
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => ActionAsync;

    [Authorize(Policy = "EmployeePolice")]
    public static async Task<IResult> ActionAsync(EmployeeRequest employeeRequest, HttpContext http, UserCreator userCreator)
    {
        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

        var userClaims = new List<Claim>
        {
            new Claim("EmployeeCode", employeeRequest.employeeCode),
            new Claim("Name", employeeRequest.name),
            new Claim("CreatedBy", userId),
        };

        (IdentityResult identity, string userId) result = await userCreator.Create(employeeRequest.email, employeeRequest.password, userClaims);

        if (!result.identity.Succeeded)
            return Results.ValidationProblem(result.identity.Errors.ConvertToProblemDetails());

        return Results.Created($"/employees/{result.userId}", result.userId);
    }
}
