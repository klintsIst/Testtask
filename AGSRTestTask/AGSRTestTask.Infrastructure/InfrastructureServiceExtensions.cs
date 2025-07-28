namespace AGSRTestTask.Infrastructure;

using AGSRTestTask.Core.Interfaces;
using AGSRTestTask.Infrastructure.Implementations;
using Contexts;
using Microsoft.Extensions.DependencyInjection;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddDbContext<MaternityHospitalContext>();

        services.AddScoped<IPatientRepository, PatientRepository>();

        return services;
    }
}
