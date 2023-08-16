using SaveVault.Models;
using SaveVault.Repositories;

namespace SaveVault.Services;

public class DownloadService : IDownloadService
{
	private readonly IDownloadRepository _downloadRepository;

	public DownloadService(IDownloadRepository downloadRepository)
	{
		_downloadRepository = downloadRepository;
	}


	public IEnumerable<ISave> GetAllSaves(Game game, User user)
	{
		throw new NotImplementedException();
	}

	public ISave GetById(Guid SaveId, User user)
	{
		throw new NotImplementedException();
	}

	public ISave GetLatest(Game game, User user)
	{
		return _downloadRepository.DownloadLatest(game, user);
	}
}