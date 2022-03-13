using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace WantApp.API.Endpoints.Employees;

public class EmployeePost
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(EmployeeRequest employeeRequest, UserManager<IdentityUser> userManager)
    {
        var user = new IdentityUser { UserName =  employeeRequest.email, Email = employeeRequest.email };
        var result = userManager.CreateAsync(user, employeeRequest.password).Result;

        var userClaims = new List<Claim>
        {
            new Claim("EmployeeCode", employeeRequest.employeeCode),
            new Claim("Name", employeeRequest.name)
        };

        if (!result.Succeeded)
            return Results.BadRequest(result.Errors.ConvertToProblemDetails());

        var calimResult = userManager.AddClaimsAsync(user, userClaims).Result;

        if (!calimResult.Succeeded)
            return Results.BadRequest(result.Errors.First());

        return Results.Created($"/employees/{user.Id}", user.Id);
    }
}
