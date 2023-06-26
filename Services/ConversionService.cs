public class ConversionService : IConversionService
{
	public Type GetTargetType(Platform platform) => platform switch
	{
		Platform.PC => typeof(PCSave),
		_ => typeof(MobileSave)
	};

	public U Convert<T, U>(T save)
		where T : ISave
		where U : ISave
	{
		throw new NotImplementedException();
	}

	public IEnumerable<U> ConvertAll<T, U>(IEnumerable<T> saves)
		where T : ISave
		where U : ISave
	{
		throw new NotImplementedException();
	}
}