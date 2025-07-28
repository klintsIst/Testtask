namespace AGSRTestTask.Common.Models.PatientModels;

using Common.Enums;
using System.ComponentModel.DataAnnotations;

public class PatientBase
{
    public Gender Gender { get; set; }

    [Required]
    public DateTime BirthDate { get; set; }

    public bool Active { get; set; }
}
