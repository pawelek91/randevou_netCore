using BusinessServices.UsersService;
using BusinessServices.MessageService;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using BusinessServices.UsersService.DetailsDictionary;
using BusinessServices.UsersFinderService;

public static class BusinessServicesProvider
{
    private static ServiceCollection _serviceCollection;
    private static  ServiceProvider _serviceProvider;


    private static void Init()
    {
        _serviceCollection= new ServiceCollection();
        var mapper = BusinessServices.EntityMapper.Mapper;
        RegisterServices(mapper);
    }
    public static T GetService<T>()
    {
        if(_serviceCollection == null)
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
        .AddScoped<IMapper>(c=>mapper)
		.BuildServiceProvider();
    }
        

}