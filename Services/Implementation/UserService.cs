using SaveVault.Models;
using SaveVault.Repositories;

namespace SaveVault.Services.Implementation;

public class UserService : IUserService
{
	private readonly IUserRepository _userRepository;

	public UserService(IUserRepository userRepository)
	{
		_userRepository = userRepository;
	}

	public async Task<User> GetById(Guid userId)
	{
		return await _userRepository.GetById(userId);
	}
}