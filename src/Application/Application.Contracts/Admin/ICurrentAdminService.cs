using Application.Models;

namespace Application.Contracts.Admin;

public interface ICurrentAdminService
{
    AdminState State { get; set; }
}