namespace Battleships.App.Persistance;

public static class ConfigureServices
{
    public static IServiceCollection AddPersistance(this IServiceCollection services)
    {
        services.AddTransient<IGameRepository, GameRepository>();

        return services;
    }
}
