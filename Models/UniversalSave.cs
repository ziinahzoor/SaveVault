using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SaveVault.Models;

public class UniversalSave : ISave
{
	public UniversalSave() => Id = Guid.NewGuid();

	public UniversalSave(Game game, User user, DateTime timestamp, List<AdditionalContentAccess> accessedAdditionalContent, dynamic data, Guid? id = null)
	{
		Game = game;
		User = user;
		Timestamp = timestamp;
		AccessedAdditionalContent = accessedAdditionalContent;
		Data = data;
		Id = id ?? Guid.NewGuid();
	}
	public Guid Id { get; set; }
	public Game Game { get; set; }
	public User User { get; set; }
	public DateTime Timestamp { get; set; }
	public List<AdditionalContentAccess> AccessedAdditionalContent { get; set; }
	[NotMapped]
	public dynamic Data { get; set; }
	public string SerializedData
	{
		get => JsonSerializer.Serialize(Data);
		set => Data = JsonSerializer.Deserialize<dynamic>(value)!;
	}
}