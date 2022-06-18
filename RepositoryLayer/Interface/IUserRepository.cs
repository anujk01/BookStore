using ModelLayer;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IUserRepository
    {
        Task<RegisterModel> Register(RegisterModel register);
        Task<RegisterModel> Login(LoginModel login);
        Task<bool> ForgotPassword(string emailID);
        Task<RegisterModel> ResetPassword(ResetModel resetPassword);
    }
}
