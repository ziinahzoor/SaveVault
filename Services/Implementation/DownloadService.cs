using SaveVault.Models;
using SaveVault.Repositories;

namespace SaveVault.Services.Implementation;

public class DownloadService : IDownloadService
{
	private readonly IDownloadRepository _downloadRepository;
	private readonly IConversionService _conversionService;

	public DownloadService(IDownloadRepository downloadRepository, IConversionService conversionService)
	{
		_downloadRepository = downloadRepository;
		_conversionService = conversionService;
	}

	public async Task<IEnumerable<ISave>> DownloadAllSaves(Game game, User user) => await _downloadRepository.DownloadAll(game, user);

	public async Task<ISave> DownloadById(Guid saveId) => await _downloadRepository.DownloadById(saveId);

	public async Task<ISave> DownloadLatest(Game game, User user)
	{
		MemoryStream file = await _downloadRepository.DownloadLatest(game, user);
		UniversalSave save = await _conversionService.ConvertFromFile<UniversalSave>(file);
		return save;
	}

	public SVFile CreatePlatformFile(string fileName, string content) => SVFile.CreatePlatformSaveFile(fileName, content);
}