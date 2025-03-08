using Application.Abstractions.Configs;

namespace Infrastructure.DataAccess.Configs;

public class AdminPasswordRepository : IAdminPasswordRepository
{
    private readonly string _filePath;

    public AdminPasswordRepository(string filePath)
    {
        _filePath = filePath;
        Password = string.Empty;
        InitializePassword();
    }

    public string Password { get; private set; }

    private void InitializePassword()
    {
        using var reader = new StreamReader(_filePath);
        string? password = reader.ReadLine();

        Password = !string.IsNullOrEmpty(password) ? password : "0000";
    }
}