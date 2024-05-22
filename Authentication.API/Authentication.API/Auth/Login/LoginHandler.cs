namespace Authentication.API.Auth.Login;

public record LoginQuery(string Email, string Password) : IQuery<LoginResult>;
public record LoginResult(Guid Id);

public class LoginQueryHandler(IDocumentSession session) : IQueryHandler<LoginQuery, LoginResult>
{
    public async Task<LoginResult> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        var user = await session.Query<User>()
            .Where(user => user.Email == query.Email
                        && user.Password == query.Password)
            .FirstOrDefaultAsync(cancellationToken);

        if (user == null)
        {
            throw new UserNotFoundException(query.Email,query.Password);
        }

        return new LoginResult(user.Id);
    }
}

