namespace AGSRTestTask.Infrastructure.Contexts;

using Common.Models;
using Common.Models.PatientModels;
using Microsoft.EntityFrameworkCore;

public class MaternityHospitalContext : DbContext
{
    public DbSet<Patient> Patients { get; set; }

    public DbSet<GivenName> GivenNames { get; set; }

    public MaternityHospitalContext() : base(GetOptions(ConnectionString))
    {
    }

    private const string ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=MaternityHospital4;";

    private static DbContextOptions GetOptions(string connectionString)
    {
        return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
