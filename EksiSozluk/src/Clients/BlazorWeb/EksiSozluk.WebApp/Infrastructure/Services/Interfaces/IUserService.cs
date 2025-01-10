using EksiSozluk.Common.ViewModels.Queries;

namespace EksiSozluk.WebApp.Infrastructure.Services;

public interface IUserService
{
    Task<UserDetailViewModel> GetUserDetail(Guid? id);
    Task<UserDetailViewModel> GetUserDetail(string userName);
    Task<bool> UpdateUser(UserDetailViewModel user);
    Task<bool> ChangeUserPassword(string oldPassword, string newPassword);
}