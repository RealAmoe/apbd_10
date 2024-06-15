using apbd_10.Models;

namespace apbd_10.Services;

public interface IUserService
{
    public Task AddUserAsync(LoginDto loginDto);
    public Task<TokenDto> LoginUserAsync(LoginDto loginDto);
    public Task<TokenDto> UpdateTokensAysnc(RefreshTokenDto refreshTokenDto);
}