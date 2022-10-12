using AutoMapper;
using StageService.Dtos;
using StageService.Models;

namespace StageService.Profiles
{
    public class StageProfile : Profile
    {
        public StageProfile()
        {
            CreateMap<Stage, StageCreateDto>();
            CreateMap<StageCreateDto, Stage>();

            CreateMap<StageCreateDto, StageUpdatedDto>();
            CreateMap<Stage, StageUpdatedDto>();
            CreateMap<StageUpdatedDto, Stage>();

            CreateMap<Stage, StageReadDto>();

        }
    }
}