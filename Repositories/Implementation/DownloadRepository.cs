using SaveVault.Models;

namespace SaveVault.Repositories.Implementation;

public class DownloadRepository : IDownloadRepository
{
	private readonly SaveVaultDbContext _dbContext;

	public DownloadRepository(SaveVaultDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<ISave> DownloadLatest(Game game, User user)
	{
		return (await DownloadAll(game, user)).FirstOrDefault();
	}

	public async Task<IEnumerable<ISave>> DownloadAll(Game game, User user)
	{
		return _dbContext.CompleteSaves
			.Where(s => s.User.Id == user.Id && s.Game.Id == game.Id)
			.OrderByDescending(s => s.Timestamp);
	}

	public async Task<ISave> DownloadById(Guid saveId)
	{
		throw new NotImplementedException();
	}
}