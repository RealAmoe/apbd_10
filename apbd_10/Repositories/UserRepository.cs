using apbd_10.Context;
using apbd_10.Entities;
using Microsoft.EntityFrameworkCore;

namespace apbd_10.Repositories;

public class UserRepository : IUserRepository
{
    private MedicalContext _medicalContext;

    public UserRepository(MedicalContext medicalContext)
    {
        _medicalContext = medicalContext;
    }
    
    public async Task AddUserAsync(User user)
    {
        _medicalContext.Users.Add(user);
        await _medicalContext.SaveChangesAsync();
    }

    public async Task<User?> GetUserAsync(string username)
    {
        var user = await _medicalContext.Users.FirstOrDefaultAsync(u => u.Login == username);

        return user;
    }

    public async Task<User?> GetUserFromTokenAsync(string refreshToken)
    {
        var user = await _medicalContext.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

        return user;
    }

    public async Task UpdateTokensAsync(User user, string refreshToken, DateTime tokenExpTime)
    {
        user.RefreshToken = refreshToken;
        user.RefreshTokenExp = tokenExpTime;

        await _medicalContext.SaveChangesAsync();
    }

    public async Task<bool> UserExistsAsync(string username)
    {
        var userExist = await _medicalContext.Users.FirstOrDefaultAsync(u => u.Login == username);

        return userExist != null;
    }
}