using SaveVault.Models;

namespace SaveVault.Repositories;

public interface IDownloadRepository
{
	ISave DownloadLatest(Game game, User user);
	IEnumerable<ISave> DownloadAll(Game game, User user);
	ISave DownloadById(Guid saveId);
}