using Microsoft.EntityFrameworkCore;
using SaveVault.Models;

namespace SaveVault.Repositories;

public class DownloadRepository : IDownloadRepository
{
	public ISave DownloadLatest(Game game, User user)
	{
		using var context = new AppDbContext();

		return context.Saves
			.Include(s => s.User)
			.Include(s => s.Game)
			.Include(s => s.AccessedAdditionalContent)
				.ThenInclude(a => a.AdditionalContent)
			.Where(s => s.User.Id == user.Id && s.Game.Id == game.Id)
			.OrderByDescending(s => s.Timestamp)
			.First();
	}
}