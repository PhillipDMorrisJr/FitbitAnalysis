using System;
using FitbitAnalysis_Phillip_Morris.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FitbitTests.Model.FitbitJournal
{
    /// <summary>
    ///     Tests the CountPerMonthAndYearTests method in the FitbitJournal class.
    ///     Tests:
    ///     No entries in Journal.
    ///     Only 1 entry in Jounal.
    ///     Multiple entries in Journal and none match criteria.
    ///     Multiple entries in Journal and 1 match criteria.
    /// </summary>
    [TestClass]
    public class CountPerMonthAndYearTests
    {
        #region Methods

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
            var testEntry = new FitbitEntry(DateTime.Today, 500, 2.0, 310, 1, 155, new TimeSpan());

            testJournal.AddEntry(testEntry);

            Assert.AreEqual(1, testJournal.CountPerMonthAndYear(DateTime.Today.Month, DateTime.Today.Year));
        }

        [TestMethod]
        public void WhenMultipleSameYearAndMonthInJournalAndNoneMatch()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();
            var testDate1 = new DateTime(2017, 11, 1);
            var testDate2 = new DateTime(2017, 11, 2);
            var testDate3 = new DateTime(2017, 11, 3);

            var testEntry = new FitbitEntry(testDate1, 1000, 2.0, 310, 1, 155, new TimeSpan());
            var testEntry2 = new FitbitEntry(testDate2, 2000, 2.0, 310, 1, 155, new TimeSpan());
            var testEntry3 = new FitbitEntry(testDate3, 3000, 2.0, 310, 1, 155, new TimeSpan());

            testJournal.AddEntry(testEntry);
            testJournal.AddEntry(testEntry2);
            testJournal.AddEntry(testEntry3);

            Assert.AreEqual(0, testJournal.CountPerMonthAndYear(10, 2017));
        }

        [TestMethod]
        public void WhenMultipleDifferentYearAndMonthInJournalAndOneMatch()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();
            var testDate1 = new DateTime(2015, 9, 2);
            var testDate2 = new DateTime(2016, 10, 5);
            var testDate3 = new DateTime(2017, 11, 3);

            var testEntry = new FitbitEntry(testDate1, 1000, 2.0, 310, 1, 155, new TimeSpan());
            var testEntry2 = new FitbitEntry(testDate2, 2000, 2.0, 310, 1, 155, new TimeSpan());
            var testEntry3 = new FitbitEntry(testDate3, 3000, 2.0, 310, 1, 155, new TimeSpan());

            testJournal.AddEntry(testEntry);
            testJournal.AddEntry(testEntry2);
            testJournal.AddEntry(testEntry3);

            Assert.AreEqual(1, testJournal.CountPerMonthAndYear(10, 2016));
        }

        #endregion
    }
}