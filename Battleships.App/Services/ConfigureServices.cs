using Battleships.App.Services.Spawners;

namespace Battleships.App.Services;

public static class ConfigureServices
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IGameService, GameService>();
        services.AddSingleton<IShipSpawner, RandomShipSpawner>();

        return services;
    }
}
