using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO.WalkDtos;

namespace NZWalks.API.Profiles
{
    public class WalkProfile : Profile
    {
        public WalkProfile()
        {
            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<Walk, CreateWalkRequest>().ReverseMap();
            CreateMap<Walk, UpdateWalkRequest>().ReverseMap();
        }
    }
}
