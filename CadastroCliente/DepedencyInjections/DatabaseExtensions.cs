using CadastroClienteServices.Service;
using CadastroClienteRepository.Interface;
using CadastroClienteRepository.Repository;
using CadastroClienteRepository.UnitOfWork;

namespace CadastroCliente.DepedencyInjections;
public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabaseExtensions(this IServiceCollection services)
    {
        services.AddScoped<ICadastroServices, CadastroServices>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddTransient<ICadastroRepository, CadastroRepository>();

        return services;
    }
}
