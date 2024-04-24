using BookAPI.Repositories.Implementations;
using BookAPI.Repositories.Interfaces;

namespace BookAPI;

public static class ServiceRegistration
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBookRepository, BookRepository>();

    }
}
