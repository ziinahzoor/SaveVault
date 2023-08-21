using SaveVault.Models;
using SaveVault.Repositories;

namespace SaveVault.Services.Implementation;

public class DownloadService : IDownloadService
{
	private readonly IDownloadRepository _downloadRepository;

	public DownloadService(IDownloadRepository downloadRepository)
	{
		_downloadRepository = downloadRepository;
	}

	public async Task<IEnumerable<ISave>> DownloadAllSaves(Game game, User user) => await _downloadRepository.DownloadAll(game, user);

	public async Task<ISave> DownloadById(Guid saveId) => await _downloadRepository.DownloadById(saveId);

	public async Task<ISave> DownloadLatest(Game game, User user) => await _downloadRepository.DownloadLatest(game, user);

	public SVFile CreatePlatformFile(string fileName, string content) => SVFile.CreatePlatformSaveFile(fileName, content);
}