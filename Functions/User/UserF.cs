using To_Do_List_API.Entities;

namespace To_Do_List_API.Functions.User;

public class UserF(ChatAppContext context)
{
    private readonly ChatAppContext _context = context;

    public async Task Authenticate()
    {
        try
        {
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}