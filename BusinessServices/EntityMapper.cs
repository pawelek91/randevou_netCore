using AutoMapper;
using BusinessServices.MessageService;
using RandevouData.Users;
using Microsoft.Extensions.DependencyInjection;
using System;
using RandevouData.Messages;
using RandevouData.Users.Details;
using BusinessServices.UsersService.DetailsDictionary;

namespace BusinessServices
{
    public static class EntityMapper
    {
        public static MapperConfiguration configuration;

        private static IMapper mapper;
        public static IMapper Mapper
        {
            get
            {
                if(mapper == null)
                {
                    Congifure();
                }
                return mapper;
            }
            private set
            {
                mapper = value;
            }
        }
        private static void Congifure()
        {
            if(configuration != null)
                return;
            
            configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>();

                cfg.CreateMap<Message, MessageDto>()
                .ForMember(dto => dto.SenderId, mess => mess.MapFrom(x => x.FromUser.Id))
                .ForMember(dto => dto.SenderName, mess => mess.MapFrom(x => x.FromUser.Name))
                .ForMember(dto => dto.ReceiverId, mess => mess.MapFrom(x => x.ToUser.Id))
                .ForMember(dto => dto.ReceiverName, mess => mess.MapFrom(x => x.ToUser.Name))
                .ForMember(dto => dto.MessageId, mess => mess.MapFrom(x => x.Id))
                .ForMember(dto => dto.Content, mess => mess.MapFrom(x => x.MessageContent));

                cfg.CreateMap<UserDetailsDictionaryItem, DictionaryItemDto>()
                .ForMember(dto => dto.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dto => dto.DisplayName, src => src.MapFrom(x => x.DisplayName))
                .ForMember(dto => dto.ItemType, src => src.MapFrom(x => x.DetailsType))
                .ForMember(dto => dto.Name, src => src.MapFrom(x => x.Name));
            });
            Mapper = configuration.CreateMapper();
        }
    }
}