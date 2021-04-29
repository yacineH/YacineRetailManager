using System.Collections.Generic;
using System.Threading.Tasks;
using TRMDesktopUILibrary.Model;

namespace TRMDesktopUILibrary.API
{
    public interface IUserEndPoint
    {
        Task<List<UserModel>> GetAll();
    }
}