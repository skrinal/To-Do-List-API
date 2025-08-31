using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using To_Do_List_API.Controllers.Auth;
using To_Do_List_API.Entities;

namespace To_Do_List_API.Functions.User;

public class UserF(ChatAppContext context)
{
    private readonly ChatAppContext _context = context;

    public async Task<AuthRES?> Authenticate(string email, string enteredPassword)
    {
        try
        {
            var entity = await _context.TblUsers
                .FirstOrDefaultAsync(x => x.Email == email);
            
            if (entity == null) return null;
            
            var isPasswordMatched = VerifyPassword(enteredPassword, entity.PasswordSalt, entity.Password);
            if (!isPasswordMatched) return null;
            
            return new AuthRES
            {
                Id = entity.Id,
                UserName = entity.Username,
                Token = null,
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    private static bool VerifyPassword(string enteredPassword, byte[] storedSalt, string storedPassword)
    {
        const int iterationCount = 10000; 
        const int keySize = 256 / 8;
            
        var encryptedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: enteredPassword,
            salt: storedSalt,
            prf: KeyDerivationPrf.HMACSHA1, //HMACSHA1 / HMACSHA256
            iterationCount: iterationCount,
            numBytesRequested: keySize));
            
        return encryptedPassword.Equals(storedPassword);
    }
}