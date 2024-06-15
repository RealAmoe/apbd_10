using apbd_10.Entities;

namespace apbd_10.Repositories;

public interface IUserRepository
{
    public Task AddUserAsync(User user);
    public Task<User?> GetUserFromTokenAsync(string refreshToken);
    public Task UpdateTokensAsync(User user, string refreshToken, DateTime tokenExpTime);
    public Task<bool> UserExistsAsync(string username);
    public Task<User?> GetUserAsync(string username);
}