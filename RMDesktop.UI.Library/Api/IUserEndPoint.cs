using RMDesktop.UI.Library.Models;
using RMDesktopUI.Library.Api;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RMDesktop.UI.Library.Api
{
    public interface IUserEndPoint
    {
        IAPIHelper _apiHelper { get; }

        Task<List<UserModel>> GetAll();
        Task<Dictionary<string, string>> GetAllRoles();
        Task AddUserToRoles(string userId, string roleName);
        Task RemoveUserFromRole(string userId, string roleName);

    }
}