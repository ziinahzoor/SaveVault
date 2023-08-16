using SaveVault.Models;

namespace SaveVault.Repositories;

public class UserRepository : IUserRepository
{
	public User GetById(Guid userId)
	{
		//Remover mock depois
		return new User(userId);
	}
}