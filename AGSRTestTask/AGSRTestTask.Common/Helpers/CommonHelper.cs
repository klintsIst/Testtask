namespace AGSRTestTask.Common.Helpers;

public static class CommonHelper
{
    private static Random generator = new ();

    public static DateTime GetRandomDate()
    {
        DateTime start = new (1995, 1, 1);
        int range = (DateTime.Today - start).Days;

        return start.AddDays(generator.Next(range));
    }
}
