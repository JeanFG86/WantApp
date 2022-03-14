using Dapper;
using Microsoft.AspNetCore.Authorization;
using Npgsql;
using WantApp.API.Infra.Data;

namespace WantApp.API.Endpoints.Employees;


public class EmployeeGetAll
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "Employee005Police")]
    public static IResult Action(int? page, int? rows, QueryAllUsersWithClaimName query)
    {        
        return Results.Ok(query.Execute(page.Value, rows.Value));
    }
}
