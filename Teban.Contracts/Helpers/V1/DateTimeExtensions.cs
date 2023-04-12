namespace Teban.Contracts.Helpers.V1;
public static class DateTimeExtensions
{
    public static int GetAge(this DateTime dateOfBirth)
    {
        var today = DateTime.Today;
        var age = today.Year - dateOfBirth.Year;
        if (dateOfBirth.Date > today.AddYears(-age)) age--;
        return age;
    }

    public static int GetAgeAtDate(this DateTime dateOfBirth, DateTime atDate)
    {
        var age = atDate.Year - dateOfBirth.Year;
        if (dateOfBirth.Date > atDate.AddYears(-age)) age--;
        return age;
    }
}
