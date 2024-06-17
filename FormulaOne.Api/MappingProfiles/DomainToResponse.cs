using AutoMapper;
using FromulaOne.Entities.DbSet;
using FromulaOne.Entities.Dtos.Response;

namespace FormulaOne.Api.MappingProfiles;

public class DomainToResponse : Profile
{
    public DomainToResponse()
    {
        CreateMap<Achievement, DriverAchievementsResponse>()
            .ForMember(
                dest => dest.Wins,
                opt => opt.MapFrom(src => src.RaceWins)
            );

        CreateMap<Driver, GetDriverResponse>()
            .ForMember(
                dest => dest.DriverId,
                opt => opt.MapFrom(src => src.Id)
            )
            .ForMember(
                dest => dest.FullName,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")
            );

    }
}
