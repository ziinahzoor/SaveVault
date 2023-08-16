using SaveVault.Models;

namespace SaveVault.Services;

public interface IUploadService
{
	public void Upload(UniversalSave save);
}