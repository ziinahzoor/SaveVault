using SaveVault.Models;

namespace SaveVault.Repositories;

public interface IUserRepository
{
	Task<User> GetById(Guid userId);
}