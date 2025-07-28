namespace AGSRTestTask.Common.Models.PatientModels;

using Common.Models.NameModels;

public class PatientCreate : PatientBase
{
    public NameCreate Name { get; set; } = default!;
}
