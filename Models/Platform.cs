namespace SaveVault.Models;

public enum Platform
{
	PC,
	Android,
	iOS
}

public class PlatformData
{
	private PlatformData() { }

	public static PlatformData PC = new()
	{
	};

	public static PlatformData Android = new()
	{
	};

	public static PlatformData iOS = new()
	{
	};

	public static PlatformData GetPlatform(string platform) => platform switch
	{
		"PC" => PC,
		"Android" => Android,
		_ => iOS
	};
}