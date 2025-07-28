namespace AGSRTestTask.Core.Interfaces;

using Common.Models.PatientModels;

public interface IPatientService
{
    public Task CreateNewPatientAsync(PatientCreate patient);

    public Task CreateNewRangePatientsAsync(IEnumerable<PatientCreate> patients);

    public Task CreateRandomPatientsAsync(int n);

    public Task<Patient> GetPatientAsync(int patientId);

    public Task<IEnumerable<Patient>> GetAllPatientsAsync();

    public Task UpdatePatientAsync(PatientCreate patientCreate, int patientId);

    public Task<IEnumerable<Patient>> SearchByBirthDateAsync(string query);

    public Task DeletePatient(int patientId);
}
