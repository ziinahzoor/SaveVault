public class MobileSave : AbstractSave
{
	public MobileSave(Game game, User user) : base(game, user)
	{
	}

	public override PlatformData Platform { get; set; } = PlatformData.Mobile;
}