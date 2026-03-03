namespace TaskFlow.API.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public Guid TenantId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string Role { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    public string? RefreshToken { get; private set; }
    public DateTime? RefreshTokenExpiresAt { get; private set; }
    public bool UserStatus { get; private set; }

    protected User() { }

    public static User Create(Guid tentantId, string name, string email, string passwordHash, string role = "Member", bool userStatus = true) =>
        new()
        {
            Id = Guid.NewGuid(),
            TenantId = tentantId,
            Name = name,
            Email = email,
            PasswordHash = passwordHash,
            Role = role,
            CreatedAt = DateTime.UtcNow,
            UserStatus = userStatus
        };

    public void SetRefreshToken(string token, DateTime expiresAt)
    {
        RefreshToken = token;
        RefreshTokenExpiresAt = expiresAt;
    }

    public void RevokeRefreshToken()
    { 
        RefreshToken = null;
        RefreshTokenExpiresAt = null;
    }

    public bool IsRefreshTokenValid(string token)
    {
        if (string.IsNullOrEmpty(RefreshToken)) return false;
        if (RefreshToken != token) return false;
        if (RefreshTokenExpiresAt < DateTime.UtcNow) return false;
        
        return true;
    }

    public void UpdatePasswordHash(string newPasswordHash)
    {
        PasswordHash = newPasswordHash;
    }

    public void UpdateName(string name)
    {
        Name = name;
    }

    public void UpdateEmail(string email) 
    {
        Email = email;
    }

    public void PromoteToAdmin() => Role = "Admin";

    public void PromoteToMember() => Role = "Member";

    public void Deactivate() => UserStatus = false;

    public void Activate() => UserStatus = true;
}
