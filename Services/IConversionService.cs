using SaveVault.Models;

namespace SaveVault.Services;

public interface IConversionService
{
	U Convert<T, U>(T save, Platform? platform = null)
		where T : ISave
		where U : ISave;

	// IEnumerable<U> ConvertAll<T, U>(IEnumerable<T> saves)
	// 	where T : ISave
	// 	where U : ISave;

	T ConvertFromFile<T>(IFormFile file) where T : ISave;
}