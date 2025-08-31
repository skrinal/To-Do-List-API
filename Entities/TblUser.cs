using System.ComponentModel.DataAnnotations;

namespace To_Do_List_API.Entities;

public class TblUser
{
    public int Id { get; set; }
    [MaxLength(50)]
    public string Username { get; set; } = string.Empty;
    [MaxLength(100)]
    public string Password { get; set; } = string.Empty;
    [MaxLength(250)]
    public string Email { get; set; } = string.Empty;
    public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
    public DateTime Created { get; set; }
}