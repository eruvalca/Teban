using Teban.Contracts.Frequencies.V1;

namespace Teban.Contracts.Helpers.V1;
public static class ScheduleDateHelper
{
    public static bool IsNextDate(string frequency,
        DateTime startDate,
        DateTime dateToCheck)
    {
        if (dateToCheck >= startDate)
        {
            switch (frequency)
            {
                case Frequency.Daily:
                    return true;
                case Frequency.Weekly:
                    if ((dateToCheck.Date - startDate.Date).Days % 7 == 0)
                    {
                        return true;
                    }
                    return false;
                case Frequency.BiWeekly:
                    if ((dateToCheck.Date - startDate.Date).Days % 14 == 0)
                    {
                        return true;
                    }
                    return false;
                case Frequency.Monthly:
                    if (IsDateLastDayOfMonth(startDate) && IsDateLastDayOfMonth(dateToCheck))
                    {
                        return true;
                    }
                    if (startDate.Day == dateToCheck.Day)
                    {
                        return true;
                    }
                    return false;
                case Frequency.BiMonthly:
                    if (startDate.Month % 2 == dateToCheck.Month % 2)
                    {
                        if (IsDateLastDayOfMonth(startDate) && IsDateLastDayOfMonth(dateToCheck))
                        {
                            return true;
                        }
                        if (startDate.Day == dateToCheck.Day)
                        {
                            return true;
                        }
                    }
                    return false;
                case Frequency.Quarterly:
                    if (startDate.Month == dateToCheck.Month
                        || startDate.Month + 3 == dateToCheck.Month
                        || startDate.Month + 6 == dateToCheck.Month
                        || startDate.Month + 9 == dateToCheck.Month)
                    {
                        if (IsDateLastDayOfMonth(startDate) && IsDateLastDayOfMonth(dateToCheck))
                        {
                            return true;
                        }
                        if (startDate.Day == dateToCheck.Day)
                        {
                            return true;
                        }
                    }
                    return false;
                case Frequency.BiAnnually:
                    if (startDate.Month == dateToCheck.Month
                        || startDate.AddMonths(6).Month == dateToCheck.Month)
                    {
                        if (IsDateLastDayOfMonth(startDate) && IsDateLastDayOfMonth(dateToCheck))
                        {
                            return true;
                        }
                        if (startDate.Day == dateToCheck.Day)
                        {
                            return true;
                        }
                    }
                    return false;
                case Frequency.Annually:
                    if (startDate.Month == dateToCheck.Month
                        && startDate.Day == dateToCheck.Day)
                    {
                        return true;
                    }
                    return false;
                default:
                    return false;
            }
        }
        return false;
    }

    private static bool IsDateLastDayOfMonth(DateTime date)
    {
        return date.AddDays(1).Day == 1;
    }
}
