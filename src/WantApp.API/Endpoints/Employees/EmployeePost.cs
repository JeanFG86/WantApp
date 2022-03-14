using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace WantApp.API.Endpoints.Employees;

public class EmployeePost
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static IResult Action(EmployeeRequest employeeRequest, HttpContext http, UserManager<IdentityUser> userManager)
    {
        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var newUser = new IdentityUser { UserName =  employeeRequest.email, Email = employeeRequest.email };
        var result = userManager.CreateAsync(newUser, employeeRequest.password).Result;

        var userClaims = new List<Claim>
        {
            new Claim("EmployeeCode", employeeRequest.employeeCode),
            new Claim("Name", employeeRequest.name),
            new Claim("CreatedBy", userId),
        };

        if (!result.Succeeded)
            return Results.BadRequest(result.Errors.ConvertToProblemDetails());

        var calimResult = userManager.AddClaimsAsync(newUser, userClaims).Result;

        if (!calimResult.Succeeded)
            return Results.BadRequest(result.Errors.First());

        return Results.Created($"/employees/{newUser.Id}", newUser.Id);
    }
}
