namespace AGSRTestTask.Core.Implementations;

using Common.Extensions;
using Common.Helpers;
using Common.Models.NameModels;
using Common.Models.PatientModels;
using Interfaces;
using System.Collections.Generic;
using System.Linq.Expressions;

public class PatientService : IPatientService
{
    private readonly IPatientRepository patientRepository;

    public PatientService(IPatientRepository patientRepository)
    {
        this.patientRepository = patientRepository;
    }

    public async Task CreateNewPatientAsync(PatientCreate patientCreate)
    {
        await patientRepository.AddAsync(patientCreate.ToPatient());
    }

    public async Task CreateNewRangePatientsAsync(IEnumerable<PatientCreate> patientsCreate)
    {
        IEnumerable<Patient> patients = patientsCreate.Select(p =>
        {
            var patient = p.ToPatient();
            patient.Name.Id = Guid.NewGuid();
            return patient;
        });

        await patientRepository.AddRangeAsync(patients);
    }

    public async Task CreateRandomPatientsAsync(int n)
    {
        List<PatientCreate> patientsToCreate = [];
        for (int i = 0; i < n; i++)
        {
            patientsToCreate.Add(GenerateRandomPatient(i.ToString()));
        }

        await CreateNewRangePatientsAsync(patientsToCreate);
    }

    public async Task<Patient> GetPatientAsync(int patientId)
    {
        return await patientRepository.GetByIdAsync(patientId);
    }

    public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
    {
        return await patientRepository.GetAllAsync();
    }

    public async Task UpdatePatientAsync(PatientCreate patientCreate, int patientId)
    {
        await patientRepository.UpdateAsync(patientCreate.ToPatient(), patientId);
    }

    public async Task<IEnumerable<Patient>> SearchByBirthDateAsync(string query)
    {
        Expression<Func<Patient, bool>> expression = FHIRQueryParser.Parse(query);

        return await patientRepository.FindByPredicateAsync(expression);
    }

    public async Task DeletePatient(int patientId)
    {
        await patientRepository.DeleteByIdAsync(patientId);
    }

    private static PatientCreate GenerateRandomPatient(string? randomIdentifier = null)
    {
        var patientCreate = new PatientCreate()
        {
            Name = new NameCreate()
            {
                Use = $"TestUse{randomIdentifier ?? string.Empty}",
                Family = $"TestFamily{randomIdentifier ?? string.Empty}",
                GivenNames = [
                    $"TestFirstGivenName{randomIdentifier ?? string.Empty}",
                    $"TestSecondGivenName{randomIdentifier ?? string.Empty}"
                ]
            },
            BirthDate = CommonHelper.GetRandomDate(),
            Gender = Common.Enums.Gender.Male, // TODO: Add rondom too.
            Active = true, // TODO: Add rondom too.
        };

        return patientCreate;
    }
}
