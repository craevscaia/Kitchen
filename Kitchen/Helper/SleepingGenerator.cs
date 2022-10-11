namespace Kitchen.Helper;

public class SleepingGenerator
{
    public static Task Delay(int sleep)
    {
        return Task.Delay(TimeSpan.FromSeconds(sleep));
    }
}