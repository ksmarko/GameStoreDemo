using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.DAL.Entities;
using GameStore.WEB.Models;

namespace GameStore.WEB.Infrastructure
{
    public class AutoMapperConfig
    {
        public static void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<GameDTO, GameModel>()
                .ForMember(dst => dst.Publisher, map => map.MapFrom(src => src.Publisher));

            cfg.CreateMap<GameModel, GameDTO>()
                .ForMember(x => x.Comments, opt => opt.Ignore());
                //.ForMember(x => x.Genres, opt => opt.Ignore())
                //.ForMember(x => x.PlatformTypes, opt => opt.Ignore());

            cfg.CreateMap<GenreDTO, GenreModel>()
                .ForMember(dst => dst.Parent, map => map.MapFrom(src => src.Parent.Name));

            cfg.CreateMap<GenreModel, GenreDTO>()
                .ForMember(x => x.Games, opt => opt.Ignore());

            cfg.CreateMap<CommentDTO, CommentModel>()
                .ForMember(dst => dst.Game, map => map.MapFrom(src => src.Game.Name))
                .ForMember(dst => dst.Parent, map => map.MapFrom(src => src.Parent.Name));
            
            cfg.CreateMap<AddCommentModel, CommentDTO>();
            cfg.CreateMap<PublisherModel, PublisherDTO>();
        }
    }
}