using AutoMapper;
using WordsHeavenEndUser.Models;
using WordsHeavenEndUser.Dtos;

namespace WordsHeavenEndUser.Helpers
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            CreateMap<EndUser, EndUserDto>().ReverseMap();
        }


    }
}
