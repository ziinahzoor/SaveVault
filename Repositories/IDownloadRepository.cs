using SaveVault.Models;

namespace SaveVault.Repositories;

public interface IDownloadRepository
{
	ISave DownloadLatest(Game game, User user);
}