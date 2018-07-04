using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.WEB.Models;

namespace GameStore.WEB.Infrastructure
{
    public class AutoMapperConfig
    {
        public static void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<GameDTO, GameModel>()
                .ForMember(dst => dst.Publisher, map => map.MapFrom(src => src.Publisher))
                .ForMember(dst => dst.CreationDate, map => map.MapFrom(src => src.CreationDate.ToShortDateString()));

            cfg.CreateMap<AddGameModel, GameDTO>()
                .ForMember(x => x.Comments, opt => opt.Ignore());

            cfg.CreateMap<EditGameModel, GameDTO>()
                .ForMember(x => x.Comments, opt => opt.Ignore());

            cfg.CreateMap<GenreDTO, GenreModel>()
                .ForMember(dst => dst.Parent, map => map.MapFrom(src => src.Parent.Name));

            cfg.CreateMap<GenreModel, GenreDTO>()
                .ForMember(x => x.Games, opt => opt.Ignore());

            cfg.CreateMap<CommentDTO, CommentModel>()
                .ForMember(dst => dst.Game, map => map.MapFrom(src => src.Game.Name))
                .ForMember(dst => dst.Parent, map => map.MapFrom(src => src.Parent.Name));
            
            cfg.CreateMap<AddCommentModel, CommentDTO>();
            cfg.CreateMap<PublisherModel, PublisherDTO>();
            cfg.CreateMap<PlatformTypeDTO, PlatformModel>();
        }
    }
}