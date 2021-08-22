using AutoMapper;
using FreeGym.API.Dtos;
using FreeGym.Core.Entities;

namespace FreeGym.API.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Muscle, MuscleDto>().ReverseMap();
        }
    }
}
