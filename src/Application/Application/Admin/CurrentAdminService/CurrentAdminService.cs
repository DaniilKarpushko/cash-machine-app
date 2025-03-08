using Application.Contracts.Admin;
using Application.Models;

namespace Application.Admin.CurrentAdminService;

public class CurrentAdminService : ICurrentAdminService
{
    public AdminState State { get; set; } = AdminState.Disconnected;
}