using Teban.Contracts.Frequencies.V1;
using Teban.Contracts.Responses.V1.Contacts;

namespace Teban.Contracts.Helpers.V1;
public static class ContactDateHelper
{
    public static bool IsNextDate(string? frequency,
        DateTime? startDate,
        DateTime dateToCheck)
    {
        if (frequency is null || startDate is null)
        {
            return false;
        }

        var startDateVal = (DateTime)startDate;

        if (dateToCheck >= startDateVal)
        {
            switch (frequency)
            {
                case Frequency.Weekly:
                    if ((dateToCheck.Date - startDateVal.Date).Days % 7 == 0)
                    {
                        return true;
                    }
                    return false;
                case Frequency.BiWeekly:
                    if ((dateToCheck.Date - startDateVal.Date).Days % 14 == 0)
                    {
                        return true;
                    }
                    return false;
                case Frequency.Monthly:
                    if (IsDateLastDayOfMonth(startDateVal) && IsDateLastDayOfMonth(dateToCheck))
                    {
                        return true;
                    }
                    if (startDateVal.Day == dateToCheck.Day)
                    {
                        return true;
                    }
                    return false;
                case Frequency.BiMonthly:
                    if (startDateVal.Month % 2 == dateToCheck.Month % 2)
                    {
                        if (IsDateLastDayOfMonth(startDateVal) && IsDateLastDayOfMonth(dateToCheck))
                        {
                            return true;
                        }
                        if (startDateVal.Day == dateToCheck.Day)
                        {
                            return true;
                        }
                    }
                    return false;
                case Frequency.Quarterly:
                    if (startDateVal.Month == dateToCheck.Month
                        || startDateVal.Month + 3 == dateToCheck.Month
                        || startDateVal.Month + 6 == dateToCheck.Month
                        || startDateVal.Month + 9 == dateToCheck.Month)
                    {
                        if (IsDateLastDayOfMonth(startDateVal) && IsDateLastDayOfMonth(dateToCheck))
                        {
                            return true;
                        }
                        if (startDateVal.Day == dateToCheck.Day)
                        {
                            return true;
                        }
                    }
                    return false;
                case Frequency.BiAnnually:
                    if (startDateVal.Month == dateToCheck.Month
                        || startDateVal.AddMonths(6).Month == dateToCheck.Month)
                    {
                        if (IsDateLastDayOfMonth(startDateVal) && IsDateLastDayOfMonth(dateToCheck))
                        {
                            return true;
                        }
                        if (startDateVal.Day == dateToCheck.Day)
                        {
                            return true;
                        }
                    }
                    return false;
                case Frequency.Annually:
                    if (startDateVal.Month == dateToCheck.Month
                        && startDateVal.Day == dateToCheck.Day)
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

    public static bool IsDateLastDayOfMonth(DateTime date)
    {
        return date.AddDays(1).Day == 1;
    }

    public static bool IsBirthDate(DateTime? birthDate, DateTime dateToCheck)
    {
        if (birthDate is null)
        {
            return false;
        }

        if (birthDate.Value.Month == 2 && birthDate.Value.Day == 29
            && dateToCheck.Month == 2 && dateToCheck.Day == 28
            && !DateTime.IsLeapYear(dateToCheck.Year))
        {
            return true;
        }

        return birthDate.Value.Month == dateToCheck.Month && birthDate.Value.Day == dateToCheck.Day;
    }

    public static bool IsBirthDateWithin7Days(DateTime? birthDate, DateTime dateToCheck)
    {
        if (birthDate is null)
        {
            return false;
        }

        var birthDayDate = new DateTime(dateToCheck.Year, birthDate.Value.Month, birthDate.Value.Day);

        return birthDayDate > dateToCheck && birthDayDate <= dateToCheck.AddDays(7);
    }

    public static bool IsScheduledWithin7Days(string? frequency, DateTime? startDate, DateTime dateToCheck)
    {
        if (frequency is null || startDate is null)
        {
            return false;
        }

        for (var nextDay = dateToCheck.AddDays(1); nextDay <= dateToCheck.AddDays(7); nextDay = nextDay.AddDays(1))
        {
            if (IsNextDate(frequency, startDate, nextDay))
            {
                return true;
            }
        }

        return false;
    }

    public static IEnumerable<(ContactResponse, DateTime)> GetContactsScheduledWithin7Days(IEnumerable<ContactResponse> contacts,
        DateTime dateToCheck)
    {
        foreach (var contact in contacts)
        {
            if (contact.Frequency is null || contact.StartDate is null)
            {
                continue;
            }

            for (var nextDay = dateToCheck.AddDays(1); nextDay <= dateToCheck.AddDays(7); nextDay = nextDay.AddDays(1))
            {
                if (IsNextDate(contact.Frequency, contact.StartDate, nextDay))
                {
                    yield return (contact, nextDay);
                }
            }
        }
    }

    public static DateTime GetNextScheduledDate(string frequency, DateTime startDate, DateTime dateToCheck)
    {
        while (!IsNextDate(frequency, startDate, dateToCheck))
        {
            dateToCheck = dateToCheck.AddDays(1);
        }

        return dateToCheck;
    }
}
