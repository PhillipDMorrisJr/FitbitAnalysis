using System;
using FitbitAnalysis_Phillip_Morris.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FitbitTests.Model.FitbitJournal
{
    /// <summary>
    /// Tests the CountPerMonthAndYearTests method in the FitbitJournal class.
    /// </summary>
    [TestClass]
    public class CountPerMonthAndYearTests
    {
        [TestMethod]
        public void WhenNoEntriesInJournalShouldBe0()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();

            Assert.AreEqual(0, testJournal.CountPerMonthAndYear(11, 2017));
        }

        [TestMethod]
        public void WhenOnlyOneInJournalAndMatches()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();
            var testEntry = new FitbitEntry(DateTime.Today, 500, 2.0, 310, 1, 155);

            testJournal.AddEntry(testEntry);

            Assert.AreEqual(1, testJournal.CountPerMonthAndYear(DateTime.Today.Month, DateTime.Today.Year));
        }

        [TestMethod]
        public void WhenMultipleSameYearAndMonthInJournalAndNoneMatch()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();
            DateTime testDate1 = new DateTime(2017, 11, 02);
            DateTime testDate2 = new DateTime(2017, 11, 05);
            DateTime testDate3 = new DateTime(2017, 11, 20);

            var testEntry = new FitbitEntry(testDate1, 1000, 2.0, 310, 1, 155);
            var testEntry2 = new FitbitEntry(testDate2, 2000, 2.0, 310, 1, 155);
            var testEntry3 = new FitbitEntry(testDate3, 3000, 2.0, 310, 1, 155);

            testJournal.AddEntry(testEntry);
            testJournal.AddEntry(testEntry2);
            testJournal.AddEntry(testEntry3);

            Assert.AreEqual(0, testJournal.CountPerMonthAndYear(10, 2017));
        }

        [TestMethod]
        public void WhenMultipleDifferentYearAndMonthInJournalAndOneMatch()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();
            DateTime testDate1 = new DateTime(2015, 9, 02);
            DateTime testDate2 = new DateTime(2016, 10, 05);
            DateTime testDate3 = new DateTime(2017, 11, 20);

            var testEntry = new FitbitEntry(testDate1, 1000, 2.0, 310, 1, 155);
            var testEntry2 = new FitbitEntry(testDate2, 2000, 2.0, 310, 1, 155);
            var testEntry3 = new FitbitEntry(testDate3, 3000, 2.0, 310, 1, 155);

            testJournal.AddEntry(testEntry);
            testJournal.AddEntry(testEntry2);
            testJournal.AddEntry(testEntry3);

            Assert.AreEqual(1, testJournal.CountPerMonthAndYear(10, 2016));
        }
    }
}
