using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.DAL.Entities;
using System.Linq;

namespace GameStore.BLL.Infrastructure
{
    public class AutoMapperConfig
    {
        public static void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Comment, CommentDTO>()
                .ForMember(dst => dst.Publisher, map => map.MapFrom(src => src.Publisher.Name));

            cfg.CreateMap<Game, GameDTO>()
                .ForMember(dst => dst.Publisher, map => map.MapFrom(src => src.Publisher.Name))
                .ForMember(dst => dst.Genres, map => map.MapFrom(src => src.Genres.Select(x => x.Name)))
                .ForMember(dst => dst.PlatformTypes, map => map.MapFrom(src => src.PlatformTypes.Select(x => x.Type)));
            
            cfg.CreateMap<GameDTO, Game>()
                .ForMember(x => x.Publisher, opt => opt.Ignore())
                .ForMember(x => x.Genres, opt => opt.Ignore())
                .ForMember(x => x.PlatformTypes, opt => opt.Ignore());

            cfg.CreateMap<Genre, GenreDTO>();
            cfg.CreateMap<PlatformType, PlatformTypeDTO>();
            cfg.CreateMap<Publisher, PublisherDTO>();
        }
    }
}
