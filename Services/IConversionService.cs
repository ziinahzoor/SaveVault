using SaveVault.Models;

namespace SaveVault.Services;

public interface IConversionService
{
	UniversalSave Convert(PlatformSave save);
	PlatformSave Convert(UniversalSave save, Platform platform);

	// IEnumerable<U> ConvertAll<T, U>(IEnumerable<T> saves)
	// 	where T : ISave
	// 	where U : ISave;

	T ConvertFromFile<T>(IFormFile file) where T : ISave;
}