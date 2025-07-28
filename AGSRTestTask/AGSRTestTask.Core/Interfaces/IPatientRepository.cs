namespace AGSRTestTask.Core.Interfaces;

using Common.Models.PatientModels;
using System.Linq.Expressions;

public interface IPatientRepository
{
    public Task AddAsync(Patient patient);

    public Task AddRangeAsync(IEnumerable<Patient> patients);

    public Task<Patient?> GetByIdAsync(int id);

    public Task<IEnumerable<Patient>> GetAllAsync();

    public Task UpdateAsync(Patient newValues, int id);

    public Task<IEnumerable<Patient>> FindByPredicateAsync(Expression<Func<Patient, bool>> predicate);

    public Task DeleteByIdAsync(int id);

    public Task SaveAsync();
}
