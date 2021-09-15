using Sahara.Common;
using Sahra.DataLayer.Models.Identity;
using System.Threading.Tasks;

namespace Sahra.Services.IService
{
    public interface ILoginService
    {
        Task<Result<LoginResponse>> Login(LoginRequest model);
    }
}
