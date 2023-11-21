namespace Application.EnvironmentInfos.Constants;

public class QueryInterval
{
    public enum Options
    {
        Every5Minutes,
        Every10Minutes,
        Every1Hour,
        Every4Hours,
        Every12Hours,
        EveryDay,
        EveryWeek
    }

    public static int GetIntervalDuration(Options interval)
    {
        return interval switch
        {
            Options.Every5Minutes => 5,
            Options.Every10Minutes => 10,
            Options.Every1Hour => 60,
            Options.Every4Hours => 240,
            Options.Every12Hours => 720,
            Options.EveryDay => 1440,
            Options.EveryWeek => 10080,
            _ => 5
        };
    }
}
