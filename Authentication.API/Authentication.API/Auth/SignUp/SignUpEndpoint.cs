namespace Authentication.API.Auth.SignUp;

public record SignUpRequest(string Email, string Password);
public record SignUpResponse(Guid Id);
public class SignUpEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/signup", async (SignUpRequest request, ISender sender) =>
        {
            var command = request.Adapt<SignUpCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<SignUpResult>();
            return Results.Ok(response);
        })
        .WithName("SignUp")
        .Produces<SignUpResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("SignUp User")
        .WithDescription("SignUp User");
    }
}
