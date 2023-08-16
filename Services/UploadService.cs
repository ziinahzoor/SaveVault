using SaveVault.Models;
using SaveVault.Repositories;

namespace SaveVault.Services;

public class UploadService : IUploadService
{
	private readonly IUploadRepository _uploadRepository;

	public UploadService(IUploadRepository uploadRepository)
	{
		_uploadRepository = uploadRepository;
	}

	public void Upload(UniversalSave save)
	{
		_uploadRepository.Upload(save);
	}
}
