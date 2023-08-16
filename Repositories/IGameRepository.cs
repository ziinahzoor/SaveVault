using SaveVault.Models;

namespace SaveVault.Repositories;

public interface IGameRepository
{
	Game GetById(Guid gameId);
}