namespace AGSRTestTask.Common.Models.NameModels;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Names")]
public class Name : NameBase
{
    [Key]
    public Guid Id { get; set; }

    public int PatientId { get; set; }

    public ICollection<GivenName> GivenNames { get; set; } = [];
}
