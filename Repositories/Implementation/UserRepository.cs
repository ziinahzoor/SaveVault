using SaveVault.Models;

namespace SaveVault.Repositories.Implementation;

public class UserRepository : IUserRepository
{
	public User GetById(Guid userId)
	{
		//Remover mock depois
		return new User(userId);
	}
}