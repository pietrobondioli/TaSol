namespace Application.EnvironmentInfos.Queries.GetEnvironmentInfo;

public class QueryRange
{
    public enum Options
    {
        LastHour,
        LastDay,
        LastWeek,
        LastMonth
    }

    public static DateTime GetRelativeDate(Options range, DateTime endDate)
    {
        return range switch
        {
            Options.LastHour => endDate.AddHours(-1),
            Options.LastDay => endDate.AddDays(-1),
            Options.LastWeek => endDate.AddDays(-7),
            Options.LastMonth => endDate.AddMonths(-1),
            _ => endDate
        };
    }
}
