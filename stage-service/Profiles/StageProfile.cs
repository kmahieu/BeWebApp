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
    

        }
    }
}