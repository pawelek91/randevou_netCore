using BusinessServices.UsersService;
using BusinessServices.MessageService;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using BusinessServices.UsersService.DetailsDictionary;
using BusinessServices.UsersFinderService;
using BusinessServices.FriendshipService;
using BusinessServices.AuthenticationService;

public static class BusinessServicesProvider
{
    private static ServiceProvider _serviceProvider = new ServiceCollection().AddScoped<IUsersService, UserService>()
            .AddScoped<IMessagesService, MessagesService>()
            .AddScoped<IUserDetailsDictionaryService, UserDetailsDictionaryService>()
            .AddScoped<IUserFinderService, UserFinderService>()
            .AddScoped<IFriendshipService, FriendshipService>()
            .AddScoped<IAuthenticationService, AuthenticationService>()
            .AddScoped<IMapper>(c => BusinessServices.EntityMapper.Mapper)
            .BuildServiceProvider();

    public static T GetService<T>()
    =>_serviceProvider.GetService<T>();
}