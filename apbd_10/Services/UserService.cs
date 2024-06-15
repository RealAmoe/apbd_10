using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using apbd_10.Entities;
using apbd_10.Exceptions;
using apbd_10.Helpers;
using apbd_10.Models;
using apbd_10.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace apbd_10.Services;

public class UserService : IUserService
{
    private IUserRepository _userRepository;
    private IConfiguration _configuration;

    public UserService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }
    public async Task AddUserAsync(LoginDto loginDto)
    {
        var hashedPasswordAndSalt = SecurityHelpers.GetHashedPasswordAndSalt(loginDto.Password);
        
        var user = new User()
        {
            Login = loginDto.UserName,
            Password = hashedPasswordAndSalt.Item1,
            Salt = hashedPasswordAndSalt.Item2,
            RefreshToken = SecurityHelpers.GenerateRefreshToken(),
            RefreshTokenExp = DateTime.Now.AddDays(1)
        };
        EnsureRegisterUserExists(await _userRepository.UserExistsAsync(user.Login));
        
        await _userRepository.AddUserAsync(user);
    }

    public async Task<TokenDto> LoginUserAsync(LoginDto loginDto)
    {
        User? user = await _userRepository.GetUserAsync(loginDto.UserName);
        EnsureLoginUserExists(user == null);

        string passwordHashFromDb = user.Password;
        string curHashedPassword = SecurityHelpers.GetHashedPasswordWithSalt(loginDto.Password, user.Salt);

        EnsurePasswordHashMatch(passwordHashFromDb != curHashedPassword);

        Claim[] userclaim = new[]
        {
            new Claim(ClaimTypes.Name, "yes"),
            new Claim(ClaimTypes.Role, "user"),
            new Claim(ClaimTypes.Role, "admin")
        };

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("TokenSettings")["Key"]));

        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _configuration["TokenSettings:Issuer"],
            audience: _configuration["TokenSettings:Audience"],
            claims: userclaim,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds
        );

        await _userRepository.UpdateTokensAsync(user, SecurityHelpers.GenerateRefreshToken(), DateTime.Now.AddDays(1));
        
        return new TokenDto()
        {
            Access = new JwtSecurityTokenHandler().WriteToken(token),
            Register = user.RefreshToken
        };
    }

    public async Task<TokenDto> UpdateTokensAysnc(RefreshTokenDto refreshTokenDto)
    {
        User? user = await _userRepository.GetUserFromTokenAsync(refreshTokenDto.RefreshToken);
        EnsureRefreshTokenUserExists(user == null);
        EnsureRefreshTokenExpired(user.RefreshTokenExp < DateTime.Now);
        
        
        Claim[] userclaim = new[]
        {
            new Claim(ClaimTypes.Name, "yes"),
            new Claim(ClaimTypes.Role, "user"),
            new Claim(ClaimTypes.Role, "admin")
        };

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("TokenSettings")["Key"]));

        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _configuration["TokenSettings:Issuer"],
            audience: _configuration["TokenSettings:Audience"],
            claims: userclaim,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds
        );
        
        await _userRepository.UpdateTokensAsync(user, SecurityHelpers.GenerateRefreshToken(), DateTime.Now.AddDays(1));

        
        return new TokenDto()
        {
            Access = new JwtSecurityTokenHandler().WriteToken(token),
            Register = user.RefreshToken
        };
    }

    private static void EnsureRegisterUserExists(bool userExists)
    {
        if (userExists)
        {
            throw new InvalidRequestException("This user already exists");
        }
    }
    private static void EnsureLoginUserExists(bool userExists)
    {
        if (userExists)
        {
            throw new DoesntExistException("User doesnt exist");
        }
    }

    private static void EnsurePasswordHashMatch(bool match)
    {
        if (match)
        {
            throw new UnauthorisedException();
        }
    }
    private static void EnsureRefreshTokenUserExists(bool refreshTokenExists)
    {
        if (refreshTokenExists)
        {
            throw new SecurityTokenException("Invalid refresh token");
        }
    }

    private static void EnsureRefreshTokenExpired(bool expired)
    {
        if (expired)
        {
            throw new SecurityTokenException("Refresh token expired");
        }
    }
}