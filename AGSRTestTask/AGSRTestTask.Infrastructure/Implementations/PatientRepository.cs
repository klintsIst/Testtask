namespace AGSRTestTask.Infrastructure.Implementations;

using AGSRTestTask.Common.Models.NameModels;
using Common.Models.PatientModels;
using Core.Interfaces;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;

public class PatientRepository : IPatientRepository, IDisposable
{
    private MaternityHospitalContext context;

    public PatientRepository(MaternityHospitalContext context)
    {
        this.context = context;
    }

    public async Task AddAsync(Patient patient)
    {
        await context.AddAsync(patient);
        await SaveAsync();
    }

    public async Task AddRangeAsync(IEnumerable<Patient> patients)
    {
        await context.AddRangeAsync(patients);
        await SaveAsync();
    }

    public async Task<Patient?> GetByIdAsync(int id)
    {
        return await context.Patients
            .Include(p => p.Name)
            .ThenInclude(n => n.GivenNames)
            .FirstOrDefaultAsync(p => p.Id.Equals(id));
    }

    public async Task<IEnumerable<Patient>> GetAllAsync()
    {
        return await context.Patients
            .Include(p => p.Name)
            .ThenInclude(n => n.GivenNames)
            .ToListAsync();
    }

    public async Task UpdateAsync(Patient newPatient, int id)
    {
        Patient patient = await context.Patients
            .Include(p => p.Name)
            .ThenInclude(n => n.GivenNames)
            .FirstAsync(p => p.Id.Equals(id)) ?? throw new Exception($"Patient with id: {id} was not found.");

        // Update Patient.
        newPatient.Id = id;
        context.Entry(patient).CurrentValues.SetValues(newPatient);

        // Update Name.
        newPatient.Name.Id = patient.Name.Id;
        newPatient.Name.PatientId = id;
        context.Entry(patient.Name).CurrentValues.SetValues(newPatient.Name);

        // Update Given names.
        await UpdateGivenItemsAsync(patient, newPatient);

        await SaveAsync();
    }

    public async Task<IEnumerable<Patient>> FindByPredicateAsync(Expression<Func<Patient, bool>> predicate)
    {
        return await context.Patients.Where(predicate)
            .Include(p => p.Name)
            .ThenInclude(n => n.GivenNames)
            .ToListAsync();
    }

    public async Task DeleteByIdAsync(int id)
    {
        Patient? patient = await GetByIdAsync(id);

        if (patient is null)
        {
            throw new Exception($"Patient with id: {id} was not found.");
        }

        context.Remove(patient!);
        await SaveAsync();
    }

    public async Task SaveAsync()
    {
        await context.SaveChangesAsync();
    }

    // TODO: Move into separate service and inject it.
    private async Task UpdateGivenItemsAsync(Patient patient, Patient newPatient)
    {
        foreach (var givenName in newPatient.Name.GivenNames)
        {
            givenName.NameId = patient.Name.Id;
        }
        if (patient.Name.GivenNames.Count > 0)
        {
            context.GivenNames.RemoveRange(patient.Name.GivenNames);
        }
        await context.GivenNames.AddRangeAsync(newPatient.Name.GivenNames);
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
