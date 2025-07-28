namespace AGSRTestTask.Common.Models.PatientModels;

using Common.Models.NameModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Patient : PatientBase
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public virtual Name Name { get; set; } = default!;
}
