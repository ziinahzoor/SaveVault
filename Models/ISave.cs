public interface ISave
{
	Guid Id { get; set; }
	PlatformData Platform { get; set; }
	Game Game { get; set; }
	User User { get; set; }
}