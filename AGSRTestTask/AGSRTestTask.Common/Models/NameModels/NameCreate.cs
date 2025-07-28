namespace AGSRTestTask.Common.Models.NameModels;

public class NameCreate : NameBase
{
    public IEnumerable<string> GivenNames { get; set; } = [];
}
