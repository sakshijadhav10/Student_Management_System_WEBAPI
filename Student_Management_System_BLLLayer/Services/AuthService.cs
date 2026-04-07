using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Student_Management_System_BLL.DTOS.Auth;
using Student_Management_System_BLL.Interfaces;
using Student_Management_System_DAL.Interfaces;
using Student_Management_System_DAL.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Student_Management_System_BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;

        public AuthService(IAuthRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        public async Task<UserDTO> RegisterAsync(RegisterDTO dto)
        {
            var existing = await _repo.GetByEmailAsync(dto.Email);
            if (existing != null)
                throw new Exception("Email already registered");

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            await _repo.CreateUserAsync(user);

            return new UserDTO
            {
                Name = user.Name,
                Email = user.Email,
                Token = CreateJwtToken(user)
            };
        }

        public async Task<UserDTO> LoginAsync(LoginDTO dto)
        {
            var user = await _repo.GetByEmailAsync(dto.Email);
            if (user == null)
                throw new Exception("Invalid email or password");

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new Exception("Invalid email or password");

            return new UserDTO
            {
                Name = user.Name,
                Email = user.Email,
                Token = CreateJwtToken(user)
            };
        }

        private string CreateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: null,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
