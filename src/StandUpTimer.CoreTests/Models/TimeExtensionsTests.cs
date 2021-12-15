using Microsoft.VisualStudio.TestTools.UnitTesting;
using StandUpTimer.Core.Models;

namespace StandUpTimer.CoreTests.Models;

[TestClass()]
public class TimeExtensionsTests
{
    [TestMethod()]
    [DataRow(Day.Saturday, Day.Saturday | Day.Sunday, Day.Sunday)]
    [DataRow(Day.Sunday, Day.Saturday | Day.Sunday, Day.Saturday)]
    [DataRow(Day.Friday, Day.Saturday | Day.Sunday, Day.Saturday)]
    [DataRow(Day.Sunday, Day.Saturday | Day.Sunday, Day.Saturday)]
    [DataRow(Day.Sunday, Day.Monday | Day.Friday, Day.Monday)]
    public void GetNextDayTest(Day currentDay, Day settingsDays, Day expected)
    {
        var day = TimeExtensions.GetNextDay(settingsDays, currentDay);

        Assert.AreEqual(expected, day);
    }
}