namespace AGSRTestTask.Common.Models.NameModels;

using System.ComponentModel.DataAnnotations;

public class NameBase
{
    public string Use { get; set; } = null!;

    [Required]
    public string Family { get; set; } = null!;
}
