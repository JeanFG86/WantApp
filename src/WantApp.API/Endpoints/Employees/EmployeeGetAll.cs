using Dapper;
using Npgsql;

namespace WantApp.API.Endpoints.Employees;

public class EmployeeGetAll
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(int? page, int? rows, IConfiguration configuration)
    {
        //using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
        //{
        //    connection.Open();
        //    connection.Query<>
        //}

        var db = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));

        var query = @"select u.""Email"", c.""ClaimValue"" as Name
                        from ""AspNetUsers"" u
                        inner join ""AspNetUserClaims"" c on u.""Id"" = c.""UserId""
                        and c.""ClaimType"" = 'Name' OFFSET(@page - 1) * @rows FETCH NEXT @rows ROWS ONLY";

        var employees = db.Query<EmployeeResponse>(query, new { page, rows });

        return Results.Ok(employees);
    }
}
