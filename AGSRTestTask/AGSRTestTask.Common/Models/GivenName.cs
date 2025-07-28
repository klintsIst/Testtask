namespace AGSRTestTask.Common.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("GivenNames")]
public class GivenName
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Value { get; set; } = default!;

    public Guid NameId { get; set; }
}
