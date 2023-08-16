using SaveVault.Models;

namespace SaveVault.Services;

public interface IGameService
{
	Game GetById(Guid gameId);
}