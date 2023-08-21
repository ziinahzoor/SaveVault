using Microsoft.EntityFrameworkCore;
using SaveVault.Models;

namespace SaveVault.Repositories.Implementation;

public class UploadRepository : IUploadRepository
{
	private readonly SaveVaultDbContext _dbContext;

	public UploadRepository(SaveVaultDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task Upload(UniversalSave save)
	{
		if (!_dbContext.CompleteSaves.Select(x => x.Id).Contains(save.Id))
		{
			if (_dbContext.Games.Select(x => x.Id).Contains(save.Game.Id))
			{
				_dbContext.Entry(save.Game).State = EntityState.Detached;
			}

			if (_dbContext.Users.Select(x => x.Id).Contains(save.User.Id))
			{
				_dbContext.Entry(save.User).State = EntityState.Detached;
			}

			foreach (AdditionalContentAccess content in save.AccessedAdditionalContent)
			{
				if (_dbContext.AdditionalContents.Select(x => x.Id).Contains(content.AdditionalContent.Id))
				{
					_dbContext.Entry(content.AdditionalContent).State = EntityState.Detached;
				}
			}

			await _dbContext.Saves.AddAsync(save);
			await _dbContext.SaveChangesAsync();
		}
	}
}