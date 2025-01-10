using WordsHeavenEndUser.Dtos;
using WordsHeavenEndUser.Models;



namespace WordsHeavenEndUser.Interfaces.Services
{
    public interface IAuthService
    {

        Task<AuthResponseDto> Login(AuthDto authDto);

        Task RegisterUser(EndUserDto userDto);

    }
}
