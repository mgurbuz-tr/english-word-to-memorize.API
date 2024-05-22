
namespace Authentication.API.Auth.SignUp;

public record SignUpCommand(string FirstName, string LastName, string Email, string Password) : ICommand<SignUpResult>;
public record SignUpResult(Guid Id);

public class SignUpValidator : AbstractValidator<SignUpCommand>
{
    public SignUpValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email is required");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Email is required");
    }
}
public class SignUpHandler
    (IDocumentSession session) : ICommandHandler<SignUpCommand, SignUpResult>
{
    public async Task<SignUpResult> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Email = request.Email,
            Password = request.Password,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        session.Store<User>(user);
        await session.SaveChangesAsync(cancellationToken);
        return new SignUpResult(user.Id);
    }
}
