using AutoMapper;
using FromulaOne.Entities.DbSet;
using FromulaOne.Entities.Dtos.Requests;
using Microsoft.AspNetCore.SignalR;

namespace FormulaOne.Api.MappingProfiles;

public class RequestToDomain : Profile
{
    public RequestToDomain()
    {
        CreateMap<CreateDriverAchievementRequest, Achievement>()
            // 如果变量名字不一致，指定一个
            .ForMember(
                dest => dest.RaceWins,
                opt => opt.MapFrom(src => src.Wins)
            )
            // 为变量添加默认值
            .ForMember(
                dest => dest.Status,
                opt => opt.MapFrom(src => 1)
            )
            .ForMember(
                dest => dest.AddedDate,
                opt => opt.MapFrom(src => DateTime.UtcNow)
            )
            .ForMember(
                dest => dest.UpdatedDate,
                opt => opt.MapFrom(src => DateTime.UtcNow)
            );

        CreateMap<UpdateDriverAchievementRequest, Achievement>()
            .ForMember(
                dest => dest.RaceWins,
                opt => opt.MapFrom(src => src.Wins)
            )
            .ForMember(
                dest => dest.UpdatedDate,
                opt => opt.MapFrom(src => DateTime.UtcNow)
            );

        CreateMap<CreateDriverRequest, Driver>()
            .ForMember(
                dest => dest.Status,
                opt => opt.MapFrom(sec => 1)
            )
            .ForMember(
                dest => dest.AddedDate,
                opt => opt.MapFrom(src => DateTime.UtcNow)
            )
            .ForMember(
                dest => dest.UpdatedDate,
                opt => opt.MapFrom(src => DateTime.UtcNow)
            );

        CreateMap<UpdateDriverRequest, Driver>()
            .ForMember(
                dest => dest.UpdatedDate,
                opt => opt.MapFrom(src => DateTime.UtcNow)
            );
    }
}
