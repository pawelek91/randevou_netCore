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

    private static void Init()
    {
        _serviceCollection= new ServiceCollection();
        var mapper = BusinessServices.EntityMapper.Mapper;
        RegisterServices(mapper);
        _init = true;
    }
    public static T GetService<T>()
    {
        if(_serviceCollection == null && !_init)
        {
            Init();
        }

        var service = BusinessServicesProvider._serviceProvider.GetService<T>();
        return service;
    }

    private static void RegisterServices(IMapper mapper)
    {
		_serviceProvider = _serviceCollection
		.AddSingleton<IUsersService, UserService>()
		.AddSingleton<IMessagesService, MessagesService>()
        .AddSingleton<IUserDetailsDictionaryService, UserDetailsDictionaryService>()
        .AddSingleton<IUserFinderService, UserFinderService>()
        .AddSingleton<IFriendshipService, FriendshipService>()
        .AddSingleton<IAuthenticationService, AuthenticationService>()
        .AddScoped<IMapper>(c=>mapper)
		.BuildServiceProvider();
    }
        

}