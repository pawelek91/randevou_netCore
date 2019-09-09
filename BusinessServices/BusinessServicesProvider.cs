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
    private static ServiceCollection _serviceCollection;
    private static  ServiceProvider _serviceProvider;
    private static bool _init = false;


    public static T GetService<T>()
    {
        if(_serviceCollection == null || !_init)
        {
            RegisterServices();
        }

        var service = BusinessServicesProvider._serviceProvider.GetService<T>();
        return service;
    }

    private static void RegisterServices()
    {
        _init = true;

        _serviceCollection = new ServiceCollection();
        var mapper = BusinessServices.EntityMapper.Mapper;

        _serviceProvider = _serviceCollection
		.AddScoped<IUsersService, UserService>()
		.AddScoped<IMessagesService, MessagesService>()
        .AddScoped<IUserDetailsDictionaryService, UserDetailsDictionaryService>()
        .AddScoped<IUserFinderService, UserFinderService>()
        .AddScoped<IFriendshipService, FriendshipService>()
        .AddScoped<IAuthenticationService, AuthenticationService>()
        .AddScoped<IMapper>(c=>mapper)
		.BuildServiceProvider();
    }
        

}