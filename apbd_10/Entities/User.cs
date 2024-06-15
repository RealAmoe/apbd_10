namespace apbd_10.Entities;

public class User
{
    public int IdUser { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string? RefreshToken { get; set; }
    public string? Salt { get; set; }
    public DateTime? RefreshTokenExp { get; set; }
}