using System.Text.Json;
using SaveVault.Helpers;
using SaveVault.Models;

namespace SaveVault.Services;

public class ConversionService : IConversionService
{
	public readonly IUserService _userService;
	public readonly IGameService _gameService;

	public ConversionService(IUserService userService, IGameService gameService)
	{
		_userService = userService;
		_gameService = gameService;
	}

	public U Convert<T, U>(T save, Platform? platform = null)
		where T : ISave
		where U : ISave
	{
		if (typeof(T) == typeof(PlatformSave))
		{
			var platformSave = (PlatformSave)System.Convert.ChangeType(save, typeof(PlatformSave))!;
			var additionalContent = platformSave.Game.AdditionalContents.Select(a => new AdditionalContentAccess(a, platformSave.AccessedContent.Contains(a.Id)));
			var universalSave = new UniversalSave(platformSave.Game, platformSave.User, platformSave.Timestamp, additionalContent.ToList(), platformSave.Data);

			return (U)System.Convert.ChangeType(universalSave, typeof(U));
		}
		else
		{
			var universalSave = (UniversalSave)System.Convert.ChangeType(save, typeof(UniversalSave))!;
			var additionalContent = universalSave.AccessedAdditionalContent
				.Where(a => a.WasAccessed)
				.Select(a => a.AdditionalContent.Id);

			var platformSave = new PlatformSave(universalSave.Game, universalSave.User, PlatformData.GetPlatform(platform.ToString()!), universalSave.Timestamp, additionalContent.ToList(), universalSave.Data, universalSave.Id);
			return (U)System.Convert.ChangeType(platformSave, typeof(U));

		}
	}

	// public IEnumerable<U> ConvertAll<T, U>(IEnumerable<T> saves)
	// 	where T : ISave
	// 	where U : ISave
	// {
	// 	throw new NotImplementedException();
	// }

	public T ConvertFromFile<T>(IFormFile file) where T : ISave
	{
		var saveString = SaveIOHelper.ReadUniversalFile(file);
		var jsonString = SaveIOHelper.Serialize(saveString);

		var jsonObject = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(jsonString)!;

		ISave save;

		Guid? saveId = null;
		if (jsonObject.TryGetValue("id", out dynamic? id))
		{
			saveId = id.GetGuid();
		}
		var userId = jsonObject["user-id"].GetGuid();
		var gameId = jsonObject["game-id"].GetGuid();
		var user = _userService.GetById(userId);
		var game = _gameService.GetById(gameId);
		var time = jsonObject["timestamp"].GetDateTime();
		List<Guid> accessedContent = new();
		foreach (var content in jsonObject["accessed-content"].EnumerateArray())
		{
			accessedContent.Add(content.GetGuid());
		}
		var data = jsonObject["data"];

		if (typeof(T) == typeof(PlatformSave))
		{
			var platform = PlatformData.GetPlatform(jsonObject["platform"].ToString());

			save = new PlatformSave(game, user, platform, time, accessedContent, data, saveId);
		}
		else
		{
			throw new NotImplementedException();
			// save = new UniversalSave(new Game(), new User());
		}

		return (T)System.Convert.ChangeType(save, typeof(T));
	}
}