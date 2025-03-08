using System.Diagnostics;
using Application.Abstractions.Configs;
using Application.Contracts.Admin;
using Application.Contracts.Records;
using Application.Models;
using Lab5.Application.Contracts.Admin;

namespace Application.Admin;

public class AdminService : IAdminService
{
    private readonly IAdminPasswordRepository _passwordRepository;
    private readonly ICurrentAdminService _currentAdminService;

    public AdminService(ICurrentAdminService currentAdminService, IAdminPasswordRepository provider)
    {
        _currentAdminService = currentAdminService;
        _passwordRepository = provider;
    }

    public LoginResult Login(string password)
    {
        if (_passwordRepository.Password != password)
            Process.GetCurrentProcess().Kill();

        _currentAdminService.State = AdminState.Connected;

        return new LoginResult.Success();
    }

    public void Logout()
    {
        _currentAdminService.State = AdminState.Disconnected;
    }
}