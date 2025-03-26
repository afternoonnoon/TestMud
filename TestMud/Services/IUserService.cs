using TestMud.Models;

public interface IUserService
{
    Task<List<User>> GetUsersAsync();
    Task AddUserAsync(User user);
}
