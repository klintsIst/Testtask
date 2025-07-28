namespace AGSRTestTask.WebAPI;

using Core.Implementations;
using Core.Interfaces;
using Infrastructure;

public static class ServiceConfigs
{
    public static IServiceCollection AddServiceConfigs(this IServiceCollection services)
    {
        services.AddInfrastructureServices();

        services.AddScoped<IPatientService, PatientService>();

        return services;
    }
}
