using WantApp.API.Domain.Users;

namespace WantApp.API.Endpoints.Client;

public class ClientPost
{
    public static string Template => "/clients";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => ActionAsync;

    [AllowAnonymous]
    public static async Task<IResult> ActionAsync(ClientRequest clientRequest, UserCreator userCreator)
    {
        var userClaims = new List<Claim>
        {
            new Claim("cpf", clientRequest.cpf),
            new Claim("Name", clientRequest.name),
        };

        (IdentityResult identity, string userId) result = await userCreator.Create(clientRequest.email, clientRequest.password, userClaims);

        if (!result.identity.Succeeded)
            return Results.ValidationProblem(result.identity.Errors.ConvertToProblemDetails());

        return Results.Created($"/clients/{result.userId}", result.userId);
    }
}
