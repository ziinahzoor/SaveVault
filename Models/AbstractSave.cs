public abstract class AbstractSave : ISave
{
	public AbstractSave(Game game, User user)
	{
		Game = game;
		User = user;
		Id = new Guid();
	}
	public Guid Id { get; set; }
	public abstract PlatformData Platform { get; set; }
	public Game Game { get; set; }
	public User User { get; set; }
}