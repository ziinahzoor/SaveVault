public class PCSave : AbstractSave
{
	public PCSave(Game game, User user) : base(game, user)
	{
	}

	public override PlatformData Platform { get; set; } = PlatformData.PC;
}