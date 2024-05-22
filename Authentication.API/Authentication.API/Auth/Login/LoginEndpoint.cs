namespace Authentication.API.Auth.Login;

public record LoginRequest(string Email, string Password);
public record LoginResponse(Guid Id);
public class LoginEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/login", async (LoginRequest request, ISender sender) =>
        {
            var command = request.Adapt<LoginQuery>();
            var result = await sender.Send(command);
            var response = result.Adapt<LoginResponse>(); 
            return Results.Ok(response);
        })
        .WithName("Login")
        .Produces<LoginResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Login User")
        .WithDescription("Login User");
    }
}
