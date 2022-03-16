namespace WantApp.API.Endpoints.Client;

public class ClientPost
{
    public static string Template => "/clients";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => ActionAsync;

    [AllowAnonymous]
    public static async Task<IResult> ActionAsync(ClientRequest clientRequest, HttpContext http, UserManager<IdentityUser> userManager)
    {
        var newUser = new IdentityUser { UserName =  clientRequest.email, Email = clientRequest.email };
        var result = await userManager.CreateAsync(newUser, clientRequest.password);

        var userClaims = new List<Claim>
        {
            new Claim("cpf", clientRequest.cpf),
            new Claim("Name", clientRequest.name),
        };

        if (!result.Succeeded)
            return Results.BadRequest(result.Errors.ConvertToProblemDetails());

        var calimResult = await userManager.AddClaimsAsync(newUser, userClaims);

        if (!calimResult.Succeeded)
            return Results.BadRequest(result.Errors.First());

        return Results.Created($"/clients/{newUser.Id}", newUser.Id);
    }
}
