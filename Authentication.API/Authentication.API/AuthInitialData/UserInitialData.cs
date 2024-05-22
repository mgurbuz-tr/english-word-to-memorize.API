using Marten.Schema;

namespace Authentication.API.AuthInitialData;

public class UserInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();
        if (await session.Query<User>().AnyAsync(cancellation))
            return;

        session.Store<User>(GetUserDatas());
        await session.SaveChangesAsync(cancellation);
    }

    private static IEnumerable<User> GetUserDatas() => new List<User>
    {
        new User()
        {
                    Id = new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"),
                    Email = "mgurbuz@email.com",
                    FirstName = "Mustafa",
                    LastName = "Gürbüz",
                    Password = "123456"
        }
    };
}
