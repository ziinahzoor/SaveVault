using SaveVault.Models;

namespace SaveVault.Services;

public interface IDownloadService
{
	IEnumerable<ISave> GetAllSaves(Game game, User user);
	ISave GetById(Guid SaveId, User user);
	ISave GetLatest(Game game, User user);
	SVFile CreatePlatformFile(string fileName, string saveString);
}