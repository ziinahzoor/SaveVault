using SaveVault.Models;

namespace SaveVault.Repositories;

public interface IUploadRepository
{
	void Upload(UniversalSave save);
}