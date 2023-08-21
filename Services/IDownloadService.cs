using SaveVault.Models;

namespace SaveVault.Services;

public interface IDownloadService
{
	IEnumerable<ISave> DownloadAllSaves(Game game, User user);
	ISave DownloadById(Guid saveId);
	ISave DownloadLatest(Game game, User user);
	SVFile CreatePlatformFile(string fileName, string saveString);
}