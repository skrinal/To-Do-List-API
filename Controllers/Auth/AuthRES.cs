namespace To_Do_List_API.Controllers.Auth;

public class AuthRES
{
    public int Id { get; set; }
    public string UserName { get; set; } = null!;
    public string Token { get; set; } = null!;
    public DateTime RefreshTokenExpiryTime { get; set; }
}