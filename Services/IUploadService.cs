using SaveVault.Models;

namespace SaveVault.Services;

public interface IUploadService
{
	void Upload(UniversalSave save);
}