using SaveVault.Models;

namespace SaveVault.Repositories.Implementation;

public class GameRepository : IGameRepository
{
	public async Task<Game> GetById(Guid gameId)
	{
		//Remover mock depois
		return new Game(gameId)
		{
			AdditionalContents = new List<AdditionalContent>()
			{
				new AdditionalContent(new Guid("23815979-e9c5-45df-9aa3-d8acd219996b")),
				new AdditionalContent(new Guid("d1d5de9a-72b7-4ff8-9cf0-dbc743f39f33")),
			}
		};
	}
}
