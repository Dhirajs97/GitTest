using WordsHeavenEndUser.Dtos;
using WordsHeavenEndUser.Helpers;
using WordsHeavenEndUser.Interfaces.Repositories;
using WordsHeavenEndUser.Interfaces.Services;
using WordsHeavenEndUser.Models;

namespace WordsHeavenEndUser.Services
{
    public class AuthService : IAuthService
    {

        private readonly IAuthRepository _authRepository;
        private readonly IUserRepository _userRepository;
        private readonly JwtTokenHelper _jwtTokenHelper;

        public AuthService(IAuthRepository authRepository, IUserRepository userRepository, JwtTokenHelper jwtTokenHelper)
        {
            _authRepository = authRepository;
            _userRepository = userRepository;
            _jwtTokenHelper = jwtTokenHelper;
        }

        public async Task<AuthResponseDto> Login(AuthDto authDto)
        {
            var user = await _userRepository.GetUserByEmailAsync(authDto.Email);
            if (user == null)
            {
                return new AuthResponseDto { ErrorMessage = "User not registered." };
            }

            if (!await _authRepository.ValidateUserCredentials(authDto.Email, authDto.Password))
            {
                return new AuthResponseDto { ErrorMessage = "Invalid password." };
            }

            var token = _jwtTokenHelper.GenerateTokenUser(authDto.Email, authDto.Email, user.City);
            return new AuthResponseDto { Token = token };
        }


        public async Task RegisterUser(EndUserDto userDto)
        {
            if (await _authRepository.IsEmailTaken(userDto.Email))
            {
                throw new Exception("Email ID already taken");
            }

            var user = new EndUser
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                Phone = userDto.Phone,
                City = userDto.City,
                CreatedAt = DateTime.Now
            };

            await _authRepository.RegisterUser(user);
        }
    }
}
