using FluentAssertions;
using Teban.Contracts.Frequencies.V1;
using Teban.Contracts.Helpers.V1;

namespace Teban.Contracts.Tests.Unit.Helpers.V1;

public class ContactDateHelperTests
{
    [Theory]
    [InlineData(2022, 1, 1, 2022, 1, 8, true)]
    [InlineData(2022, 1, 1, 2022, 1, 15, true)]
    [InlineData(2022, 1, 1, 2022, 1, 22, true)]
    [InlineData(2022, 1, 1, 2022, 1, 29, true)]
    [InlineData(2022, 1, 1, 2022, 2, 5, true)]
    [InlineData(2022, 1, 1, 2022, 2, 12, true)]
    [InlineData(2022, 1, 1, 2022, 2, 19, true)]
    [InlineData(2022, 1, 1, 2022, 2, 26, true)]
    [InlineData(2022, 1, 1, 2022, 3, 5, true)]
    [InlineData(2022, 1, 1, 2022, 3, 12, true)]
    public void IsNextDate_ReturnsTrue_WhenFrequencyIsWeekly_AndDateIsMultipleOfWeeksAfterStartDate(
        int startYear, int startMonth, int startDay,
        int dateToCheckYear, int dateToCheckMonth, int dateToCheckDay,
        bool expected)
    {
        // Arrange
        var startDate = new DateTime(startYear, startMonth, startDay);
        var dateToCheck = new DateTime(dateToCheckYear, dateToCheckMonth, dateToCheckDay);

        // Act
        var result = ContactDateHelper.IsNextDate(Frequency.Weekly, startDate, dateToCheck);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(2022, 1, 1, 2022, 1, 9, false)]
    [InlineData(2022, 1, 1, 2022, 1, 14, false)]
    [InlineData(2022, 1, 1, 2022, 1, 23, false)]
    [InlineData(2022, 1, 1, 2022, 1, 28, false)]
    [InlineData(2022, 1, 1, 2022, 2, 6, false)]
    [InlineData(2022, 1, 1, 2022, 2, 11, false)]
    [InlineData(2022, 1, 1, 2022, 2, 20, false)]
    [InlineData(2022, 1, 1, 2022, 2, 25, false)]
    [InlineData(2022, 1, 1, 2022, 3, 6, false)]
    [InlineData(2022, 1, 1, 2022, 3, 11, false)]
    public void IsNextDate_ReturnsFalse_WhenFrequencyIsWeekly_AndDateIsNotMultipleOfWeeksAfterStartDate(
        int startYear, int startMonth, int startDay,
        int dateToCheckYear, int dateToCheckMonth, int dateToCheckDay,
        bool expected)
    {
        // Arrange
        var startDate = new DateTime(startYear, startMonth, startDay);
        var dateToCheck = new DateTime(dateToCheckYear, dateToCheckMonth, dateToCheckDay);

        // Act
        var result = ContactDateHelper.IsNextDate(Frequency.Weekly, startDate, dateToCheck);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(2022, 1, 1, 2022, 1, 15, true)]
    [InlineData(2022, 1, 1, 2022, 1, 29, true)]
    [InlineData(2022, 1, 1, 2022, 2, 12, true)]
    [InlineData(2022, 1, 1, 2022, 2, 26, true)]
    [InlineData(2022, 1, 1, 2022, 3, 12, true)]
    [InlineData(2022, 1, 1, 2022, 3, 26, true)]
    [InlineData(2022, 1, 1, 2022, 4, 9, true)]
    [InlineData(2022, 1, 1, 2022, 4, 23, true)]
    [InlineData(2022, 1, 1, 2022, 5, 7, true)]
    [InlineData(2022, 1, 1, 2022, 5, 21, true)]
    public void IsNextDate_ReturnsTrue_WhenFrequencyIsBiWeekly_AndDateIsMultipleOfTwoWeeksAfterStartDate(
        int startYear, int startMonth, int startDay,
        int dateToCheckYear, int dateToCheckMonth, int dateToCheckDay,
        bool expected)
    {
        // Arrange
        var startDate = new DateTime(startYear, startMonth, startDay);
        var dateToCheck = new DateTime(dateToCheckYear, dateToCheckMonth, dateToCheckDay);

        // Act
        var result = ContactDateHelper.IsNextDate(Frequency.BiWeekly, startDate, dateToCheck);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(2023, 4, 3, 2023, 4, 4, false)]
    [InlineData(2023, 4, 3, 2023, 4, 5, false)]
    [InlineData(2023, 4, 3, 2023, 4, 6, false)]
    [InlineData(2023, 4, 3, 2023, 4, 7, false)]
    [InlineData(2023, 4, 3, 2023, 4, 8, false)]
    [InlineData(2023, 4, 3, 2023, 4, 9, false)]
    [InlineData(2023, 4, 3, 2023, 4, 10, false)]
    [InlineData(2023, 4, 3, 2023, 4, 11, false)]
    [InlineData(2023, 4, 3, 2023, 4, 12, false)]
    [InlineData(2023, 4, 3, 2023, 4, 13, false)]
    public void IsNextDate_ReturnsFalse_WhenFrequencyIsBiWeekly_AndDateIsNotMultipleOfTwoWeeksAfterStartDate(
        int startYear, int startMonth, int startDay,
        int dateToCheckYear, int dateToCheckMonth, int dateToCheckDay,
        bool expected)
    {
        // Arrange
        var startDate = new DateTime(startYear, startMonth, startDay);
        var dateToCheck = new DateTime(dateToCheckYear, dateToCheckMonth, dateToCheckDay);

        // Act
        var result = ContactDateHelper.IsNextDate(Frequency.BiWeekly, startDate, dateToCheck);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(2023, 4, 30, 2023, 5, 31, true)]
    [InlineData(2023, 2, 28, 2023, 3, 31, true)]
    [InlineData(2023, 2, 28, 2024, 2, 29, true)]
    [InlineData(2023, 2, 28, 2024, 2, 28, true)]
    [InlineData(2023, 12, 31, 2024, 1, 31, true)]
    [InlineData(2023, 12, 31, 2024, 2, 29, true)]
    [InlineData(2023, 12, 31, 2024, 12, 31, true)]
    [InlineData(2022, 12, 31, 2023, 1, 31, true)]
    [InlineData(2023, 10, 31, 2025, 3, 31, true)]
    [InlineData(2023, 9, 30, 2029, 1, 31, true)]
    public void IsNextDate_ReturnsTrue_WhenFrequencyIsMonthly_AndDateIsLastDayOfMonth(
        int startYear, int startMonth, int startDay,
        int dateToCheckYear, int dateToCheckMonth, int dateToCheckDay,
        bool expected)
    {
        // Arrange
        var startDate = new DateTime(startYear, startMonth, startDay);
        var dateToCheck = new DateTime(dateToCheckYear, dateToCheckMonth, dateToCheckDay);

        // Act
        var result = ContactDateHelper.IsNextDate(Frequency.Monthly, startDate, dateToCheck);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(2022, 1, 31, 2022, 2, 28, true)]
    [InlineData(2022, 1, 31, 2022, 3, 31, true)]
    [InlineData(2022, 1, 31, 2022, 4, 30, true)]
    [InlineData(2022, 1, 31, 2022, 5, 31, true)]
    [InlineData(2022, 1, 31, 2022, 6, 30, true)]
    [InlineData(2022, 1, 31, 2022, 7, 31, true)]
    [InlineData(2022, 1, 31, 2022, 8, 31, true)]
    [InlineData(2022, 1, 31, 2022, 9, 30, true)]
    [InlineData(2022, 1, 31, 2022, 10, 31, true)]
    [InlineData(2022, 1, 31, 2023, 1, 31, true)]
    public void IsNextDate_ReturnsTrue_WhenFrequencyIsMonthly_AndDatesHaveSameDayOfMonth(
        int startYear, int startMonth, int startDay,
        int dateToCheckYear, int dateToCheckMonth, int dateToCheckDay,
        bool expected)
    {
        // Arrange
        var startDate = new DateTime(startYear, startMonth, startDay);
        var dateToCheck = new DateTime(dateToCheckYear, dateToCheckMonth, dateToCheckDay);

        // Act
        var result = ContactDateHelper.IsNextDate(Frequency.Monthly, startDate, dateToCheck);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(2022, 1, 1, 2022, 2, 2, false)]
    [InlineData(2022, 1, 1, 2022, 3, 3, false)]
    [InlineData(2022, 1, 1, 2022, 4, 4, false)]
    [InlineData(2022, 1, 1, 2022, 5, 5, false)]
    [InlineData(2022, 1, 1, 2022, 6, 6, false)]
    [InlineData(2022, 1, 1, 2022, 7, 7, false)]
    [InlineData(2022, 1, 1, 2022, 8, 8, false)]
    [InlineData(2022, 1, 1, 2022, 9, 3, false)]
    [InlineData(2022, 12, 31, 2023, 12, 30, false)]
    [InlineData(2022, 1, 1, 2024, 1, 23, false)]
    public void IsNextDate_ReturnsFalse_WhenFrequencyIsMonthly_AndDatesDoNotMeetCriteria(
        int startYear, int startMonth, int startDay,
        int dateToCheckYear, int dateToCheckMonth, int dateToCheckDay,
        bool expected)
    {
        // Arrange
        var startDate = new DateTime(startYear, startMonth, startDay);
        var dateToCheck = new DateTime(dateToCheckYear, dateToCheckMonth, dateToCheckDay);

        // Act
        var result = ContactDateHelper.IsNextDate(Frequency.Monthly, startDate, dateToCheck);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(2022, 1, 1, 2022, 3, 1, true)]
    [InlineData(2022, 1, 1, 2022, 4, 1, false)]
    [InlineData(2022, 1, 1, 2022, 5, 1, true)]
    [InlineData(2022, 1, 1, 2022, 7, 1, true)]
    [InlineData(2022, 1, 1, 2022, 8, 1, false)]
    [InlineData(2022, 1, 1, 2022, 9, 1, true)]
    [InlineData(2022, 1, 1, 2023, 3, 1, true)]
    [InlineData(2022, 1, 1, 2023, 4, 1, false)]
    [InlineData(2022, 1, 1, 2023, 5, 1, true)]
    [InlineData(2022, 12, 31, 2023, 2, 28, true)]
    public void IsNextDate_ReturnsTrue_WhenFrequencyIsBiMonthly_AndDateIsMultipleOfTwoMonthsAfterStartDate(
        int startYear, int startMonth, int startDay,
        int dateToCheckYear, int dateToCheckMonth, int dateToCheckDay,
        bool expected)
    {
        // Arrange
        var startDate = new DateTime(startYear, startMonth, startDay);
        var dateToCheck = new DateTime(dateToCheckYear, dateToCheckMonth, dateToCheckDay);

        // Act
        var result = ContactDateHelper.IsNextDate(Frequency.BiMonthly, startDate, dateToCheck);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(2022, 1, 1, 2022, 2, 1, false)]
    [InlineData(2022, 1, 1, 2022, 4, 1, false)]
    [InlineData(2022, 1, 1, 2022, 6, 1, false)]
    [InlineData(2022, 1, 1, 2022, 8, 1, false)]
    [InlineData(2022, 1, 1, 2022, 10, 1, false)]
    [InlineData(2022, 1, 1, 2022, 12, 1, false)]
    [InlineData(2022, 1, 1, 2023, 2, 1, false)]
    [InlineData(2022, 1, 1, 2023, 4, 1, false)]
    [InlineData(2022, 1, 1, 2023, 6, 1, false)]
    [InlineData(2022, 1, 1, 2023, 8, 1, false)]
    public void IsNextDate_ReturnsFalse_WhenFrequencyIsBiMonthly_AndDateIsNotMultipleOfTwoMonthsAfterStartDate(
        int startYear, int startMonth, int startDay,
        int dateToCheckYear, int dateToCheckMonth, int dateToCheckDay,
        bool expected)
    {
        // Arrange
        var startDate = new DateTime(startYear, startMonth, startDay);
        var dateToCheck = new DateTime(dateToCheckYear, dateToCheckMonth, dateToCheckDay);

        // Act
        var result = ContactDateHelper.IsNextDate(Frequency.BiMonthly, startDate, dateToCheck);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(2022, 1, 1, 2022, 4, 1, true)]
    [InlineData(2022, 1, 1, 2022, 7, 1, true)]
    [InlineData(2022, 1, 1, 2022, 10, 1, true)]
    [InlineData(2022, 1, 1, 2023, 1, 1, true)]
    [InlineData(2022, 1, 1, 2023, 4, 1, true)]
    [InlineData(2022, 1, 1, 2023, 7, 1, true)]
    [InlineData(2022, 1, 1, 2023, 10, 1, true)]
    [InlineData(2022, 1, 1, 2024, 1, 1, true)]
    [InlineData(2022, 1, 1, 2024, 4, 1, true)]
    [InlineData(2022, 1, 1, 2024, 7, 1, true)]
    public void IsNextDate_ReturnsTrue_WhenFrequencyIsQuarterly_AndDateIsMultipleOfThreeMonthsAfterStartDate(
        int startYear, int startMonth, int startDay,
        int dateToCheckYear, int dateToCheckMonth, int dateToCheckDay,
        bool expected)
    {
        // Arrange
        var startDate = new DateTime(startYear, startMonth, startDay);
        var dateToCheck = new DateTime(dateToCheckYear, dateToCheckMonth, dateToCheckDay);

        // Act
        var result = ContactDateHelper.IsNextDate(Frequency.Quarterly, startDate, dateToCheck);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(2022, 1, 1, 2022, 3, 1, false)]
    [InlineData(2022, 1, 1, 2022, 5, 1, false)]
    [InlineData(2022, 1, 1, 2022, 6, 1, false)]
    [InlineData(2022, 1, 1, 2022, 8, 1, false)]
    [InlineData(2022, 1, 1, 2022, 9, 1, false)]
    [InlineData(2022, 1, 1, 2022, 11, 1, false)]
    [InlineData(2022, 1, 1, 2022, 12, 1, false)]
    [InlineData(2022, 1, 1, 2023, 3, 1, false)]
    [InlineData(2022, 1, 1, 2023, 5, 1, false)]
    [InlineData(2022, 1, 1, 2023, 6, 1, false)]
    public void IsNextDate_ReturnsFalse_WhenFrequencyIsQuarterly_AndDateIsNotMultipleOfThreeMonthsAfterStartDate(
        int startYear, int startMonth, int startDay,
        int dateToCheckYear, int dateToCheckMonth, int dateToCheckDay,
        bool expected)
    {
        // Arrange
        var startDate = new DateTime(startYear, startMonth, startDay);
        var dateToCheck = new DateTime(dateToCheckYear, dateToCheckMonth, dateToCheckDay);

        // Act
        var result = ContactDateHelper.IsNextDate(Frequency.Quarterly, startDate, dateToCheck);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(2022, 1, 1, 2022, 7, 1, true)]
    [InlineData(2022, 1, 1, 2023, 1, 1, true)]
    [InlineData(2022, 1, 1, 2023, 7, 1, true)]
    [InlineData(2022, 1, 1, 2024, 1, 1, true)]
    [InlineData(2022, 1, 1, 2024, 7, 1, true)]
    [InlineData(2022, 1, 1, 2025, 1, 1, true)]
    [InlineData(2022, 1, 1, 2025, 7, 1, true)]
    [InlineData(2022, 1, 1, 2026, 1, 1, true)]
    [InlineData(2022, 1, 1, 2026, 7, 1, true)]
    [InlineData(2022, 1, 1, 2027, 1, 1, true)]
    public void IsNextDate_ReturnsTrue_WhenFrequencyIsBiAnnually_AndDateIsMultipleOfSixMonthsAfterStartDate(
        int startYear, int startMonth, int startDay,
        int dateToCheckYear, int dateToCheckMonth, int dateToCheckDay,
        bool expected)
    {
        // Arrange
        var startDate = new DateTime(startYear, startMonth, startDay);
        var dateToCheck = new DateTime(dateToCheckYear, dateToCheckMonth, dateToCheckDay);

        // Act
        var result = ContactDateHelper.IsNextDate(Frequency.BiAnnually, startDate, dateToCheck);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(2022, 1, 1, 2022, 6, 1, false)]
    [InlineData(2022, 1, 1, 2022, 8, 1, false)]
    [InlineData(2022, 1, 1, 2022, 9, 1, false)]
    [InlineData(2022, 1, 1, 2022, 10, 1, false)]
    [InlineData(2022, 1, 1, 2022, 11, 1, false)]
    [InlineData(2022, 1, 1, 2022, 12, 1, false)]
    [InlineData(2022, 1, 1, 2023, 6, 1, false)]
    [InlineData(2022, 1, 1, 2023, 8, 1, false)]
    [InlineData(2022, 1, 1, 2023, 9, 1, false)]
    [InlineData(2022, 1, 1, 2023, 10, 1, false)]
    public void IsNextDate_ReturnsFalse_WhenFrequencyIsBiAnnually_AndDateIsNotMultipleOfSixMonthsAfterStartDate(
        int startYear, int startMonth, int startDay,
        int dateToCheckYear, int dateToCheckMonth, int dateToCheckDay,
        bool expected)
    {
        // Arrange
        var startDate = new DateTime(startYear, startMonth, startDay);
        var dateToCheck = new DateTime(dateToCheckYear, dateToCheckMonth, dateToCheckDay);

        // Act
        var result = ContactDateHelper.IsNextDate(Frequency.BiAnnually, startDate, dateToCheck);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(2022, 1, 1, 2023, 1, 1, true)]
    [InlineData(2022, 1, 1, 2024, 1, 1, true)]
    [InlineData(2022, 1, 1, 2025, 1, 1, true)]
    [InlineData(2022, 1, 1, 2026, 1, 1, true)]
    [InlineData(2022, 1, 1, 2027, 1, 1, true)]
    [InlineData(2022, 1, 1, 2028, 1, 1, true)]
    [InlineData(2022, 1, 1, 2029, 1, 1, true)]
    [InlineData(2022, 1, 1, 2030, 1, 1, true)]
    [InlineData(2022, 1, 1, 2031, 1, 1, true)]
    [InlineData(2022, 1, 1, 2032, 1, 1, true)]
    public void IsNextDate_ReturnsTrue_WhenFrequencyIsAnnual_AndDateIsMultipleOfOneYearAfterStartDate(
        int startYear, int startMonth, int startDay,
        int dateToCheckYear, int dateToCheckMonth, int dateToCheckDay,
        bool expected)
    {
        // Arrange
        var startDate = new DateTime(startYear, startMonth, startDay);
        var dateToCheck = new DateTime(dateToCheckYear, dateToCheckMonth, dateToCheckDay);

        // Act
        var result = ContactDateHelper.IsNextDate(Frequency.Annually, startDate, dateToCheck);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(2022, 1, 1, 2023, 1, 2, false)]
    [InlineData(2022, 1, 1, 2022, 1, 2, false)]
    [InlineData(2022, 1, 1, 2024, 1, 2, false)]
    [InlineData(2022, 1, 1, 2022, 1, 3, false)]
    [InlineData(2022, 1, 1, 2025, 1, 2, false)]
    [InlineData(2022, 1, 1, 2022, 1, 4, false)]
    [InlineData(2022, 1, 1, 2022, 1, 22, false)]
    [InlineData(2022, 1, 1, 2026, 1, 2, false)]
    [InlineData(2022, 1, 1, 2075, 1, 13, false)]
    [InlineData(2022, 1, 1, 2028, 1, 31, false)]
    public void IsNextDate_ReturnsFalse_WhenFrequencyIsAnnual_AndDateIsNotMultipleOfOneYearAfterStartDate(
        int startYear, int startMonth, int startDay,
        int dateToCheckYear, int dateToCheckMonth, int dateToCheckDay,
        bool expected)
    {
        // Arrange
        var startDate = new DateTime(startYear, startMonth, startDay);
        var dateToCheck = new DateTime(dateToCheckYear, dateToCheckMonth, dateToCheckDay);

        // Act
        var result = ContactDateHelper.IsNextDate(Frequency.Annually, startDate, dateToCheck);

        // Assert
        result.Should().Be(expected);
    }

    // write unit tests for ContactDateHelper.IsDateLastDayOfMonth method
    [Theory]
    [InlineData(2022, 1, 31, true)]
    [InlineData(2022, 2, 28, true)]
    [InlineData(2022, 3, 31, true)]
    [InlineData(2022, 4, 30, true)]
    [InlineData(2022, 5, 31, true)]
    [InlineData(2022, 6, 30, true)]
    [InlineData(2022, 7, 31, true)]
    [InlineData(2022, 8, 31, true)]
    [InlineData(2022, 9, 30, true)]
    [InlineData(2022, 10, 31, true)]
    public void IsDateLastDayOfMonth_ReturnsTrue_WhenDateIsLastDayOfMonth(
        int year, int month, int day, bool expected)
    {
        // Arrange
        var date = new DateTime(year, month, day);

        // Act
        var result = ContactDateHelper.IsDateLastDayOfMonth(date);

        // Assert
        result.Should().Be(expected);
    }

    // write unit tests for ContactDateHelper.IsDateLastDayOfMonth method that will return false when the date is not the last day of the month
    [Theory]
    [InlineData(2022, 1, 1, false)]
    [InlineData(2022, 2, 1, false)]
    [InlineData(2022, 3, 1, false)]
    [InlineData(2022, 4, 1, false)]
    [InlineData(2022, 5, 1, false)]
    [InlineData(2022, 6, 1, false)]
    [InlineData(2022, 7, 1, false)]
    [InlineData(2022, 8, 1, false)]
    [InlineData(2022, 9, 1, false)]
    [InlineData(2022, 10, 1, false)]
    public void IsDateLastDayOfMonth_ReturnsFalse_WhenDateIsNotLastDayOfMonth(
        int year, int month, int day, bool expected)
    {
        // Arrange
        var date = new DateTime(year, month, day);

        // Act
        var result = ContactDateHelper.IsDateLastDayOfMonth(date);

        // Assert
        result.Should().Be(expected);
    }
}