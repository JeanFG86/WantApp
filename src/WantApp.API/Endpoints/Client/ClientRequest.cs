namespace WantApp.API.Endpoints.Client;

public record ClientRequest(string email, string password, string name, string cpf);
