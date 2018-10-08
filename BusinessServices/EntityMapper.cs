using AutoMapper;
using BusinessServices.MessageService;
using RandevouData.Users;
using Microsoft.Extensions.DependencyInjection;
using System;
using RandevouData.Messages;

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
                cfg.CreateMap<Message,MessageDto>();
            });
            Mapper = configuration.CreateMapper();
        }
    }
}