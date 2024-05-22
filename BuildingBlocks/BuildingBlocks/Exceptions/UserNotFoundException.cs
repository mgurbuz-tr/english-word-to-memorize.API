namespace BuildingBlocks.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(string username, string password) : base($"Username : |{username}| or Password : |{password}| is wrong!")
    {

    }
}
