namespace AGSRTestTask.Common.Extensions;

using Models;
using Models.NameModels;
using Models.PatientModels;

public static class CommonExtensions
{
    public static Patient ToPatient(this PatientCreate patientCreate)
    {
        Name name = new()
        {
            Use = patientCreate.Name.Use,
            Family = patientCreate.Name.Family,
            GivenNames = [.. patientCreate.Name.GivenNames.Select(n => new GivenName() { Value = n })],
        };

        return new Patient
        {
            Name = name,
            Gender = patientCreate.Gender,
            BirthDate = patientCreate.BirthDate,
            Active = patientCreate.Active,
        };
    }
}
