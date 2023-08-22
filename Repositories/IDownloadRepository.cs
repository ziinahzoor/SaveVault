using SaveVault.Models;

namespace SaveVault.Repositories;

public interface IDownloadRepository
{
	Task<MemoryStream> DownloadLatest(Game game, User user);
	Task<IEnumerable<ISave>> DownloadAll(Game game, User user);
	Task<ISave> DownloadById(Guid saveId);
}