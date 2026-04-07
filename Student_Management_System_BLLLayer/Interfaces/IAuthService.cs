using Student_Management_System_BLL.DTOS.Auth;

namespace Student_Management_System_BLL.Interfaces
{
    public interface IAuthService
    {
        Task<UserDTO> RegisterAsync(RegisterDTO dto);
        Task<UserDTO> LoginAsync(LoginDTO dto);
    }
}
