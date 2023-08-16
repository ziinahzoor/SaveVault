using SaveVault.Models;

namespace SaveVault.Repositories.Implementation;

public class UploadRepository : IUploadRepository
{
	public void Upload(UniversalSave save)
	{
		using SaveVaultDbContext context = new();
		if (!context.Saves.Select(x => x.Id).Contains(save.Id))
		{
			if (!context.Games.Select(x => x.Id).Contains(save.Game.Id))
			{
				context.Games.Add(save.Game);
			}

			if (!context.Users.Select(x => x.Id).Contains(save.User.Id))
			{
				context.Users.Add(save.User);
			}

			IEnumerable<AdditionalContent> additionalContents = save.AccessedAdditionalContent.Select(a => a.AdditionalContent);

			foreach (AdditionalContent content in additionalContents)
			{
				if (!context.AdditionalContents.Select(x => x.Id).Contains(content.Id))
				{
					context.AdditionalContents.Add(content);
				}
			}

			context.Saves.Add(save);
			context.SaveChanges();
		}
	}
}