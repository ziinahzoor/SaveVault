namespace SaveVault.Services;

public interface IUserService
{
	User GetById(Guid userId);
}