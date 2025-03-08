using Application.Contracts.Records;

namespace Lab5.Application.Contracts.Admin;

public interface IAdminService
{
    LoginResult Login(string password);
    void Logout();
}