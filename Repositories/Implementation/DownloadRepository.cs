using Google.Cloud.Firestore;
using SaveVault.Models;

namespace SaveVault.Repositories.Implementation;

public class DownloadRepository : IDownloadRepository
{
	private readonly IFirebaseRepository _firebaseRepository;

	public DownloadRepository(IFirebaseRepository firebaseRepository)
	{
		_firebaseRepository = firebaseRepository;
	}

	public async Task<MemoryStream> DownloadLatest(Game game, User user)
	{
		QuerySnapshot saveSnapshot = await _firebaseRepository.GetSaves(user, game)
			.OrderByDescending("timestamp")
			.Limit(1)
			.GetSnapshotAsync();

		DocumentSnapshot save = saveSnapshot[0];

		return await _firebaseRepository.Download(save.Id.ToString());
	}

	public async Task<IEnumerable<ISave>> DownloadAll(Game game, User user)
	{
		throw new NotImplementedException();

	}

	public async Task<ISave> DownloadById(Guid saveId)
	{
		throw new NotImplementedException();
	}
}