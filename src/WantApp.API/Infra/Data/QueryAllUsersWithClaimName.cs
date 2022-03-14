using Dapper;
using Npgsql;
using WantApp.API.Endpoints.Employees;

namespace WantApp.API.Infra.Data;

public class QueryAllUsersWithClaimName
{
    private readonly IConfiguration configuration;

    public QueryAllUsersWithClaimName(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public async Task<IEnumerable<EmployeeResponse>> ExecuteAsync(int page, int rows)
    {
        var db = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));

        var query = @"select u.""Email"", c.""ClaimValue"" as Name
                        from ""AspNetUsers"" u
                        inner join ""AspNetUserClaims"" c on u.""Id"" = c.""UserId""
                        and c.""ClaimType"" = 'Name' OFFSET(@page - 1) * @rows FETCH NEXT @rows ROWS ONLY";

        return await db.QueryAsync<EmployeeResponse>(query, new { page, rows });
    }
}
