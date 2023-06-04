public class Playstation4Save : AbstractSave
{
	public Playstation4Save()
	{

	}

	public override PlatformData Platform { get; set; } = PlatformData.PlayStation4;

	public FileInfo? Param { get; set; }
	public FileInfo? Icon0 { get; set; }
	public FileInfo? Sdslot { get; set; }
	public FileInfo? Sealedkey { get; set; }
}