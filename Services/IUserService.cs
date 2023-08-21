using SaveVault.Models;

namespace SaveVault.Services;

public interface IUserService
{
	Task<User> GetById(Guid userId);
}