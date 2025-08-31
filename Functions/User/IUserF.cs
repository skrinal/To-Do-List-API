using To_Do_List_API.Controllers.Auth;

namespace To_Do_List_API.Functions.User;

public interface IUserF
{
    Task<AuthRES?> Authenticate(string email, string enteredPassword);
}