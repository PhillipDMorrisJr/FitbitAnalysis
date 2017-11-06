using System;
using FitbitAnalysis_Phillip_Morris.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FitbitTests.Model.FitbitJournal
{
    /// <summary>
    ///     Tests the GetEntryByDate metho of the FitbitJournal class.
    ///     Tests:
    ///     1 entry in journal matches.
    ///     1 entry from multiple in journal matches.
    ///     Date matches properly.
    /// </summary>
    [TestClass]
    public class GetEntryByDateTests
    {
        #region Methods

        [TestMethod]
        public void WhenEntryInJournalMatches()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();
            var testDate = new DateTime(2017, 11, 2);
            var testEntry1 = new FitbitEntry(testDate, 500, 2.0, 310, 1, 155, new TimeSpan());

            testJournal.AddEntry(testEntry1);

            Assert.AreEqual(500, testJournal.GetEntryByDate(testDate).Steps);
        }

        [TestMethod]
        public void When1EntryInMultipleMatches()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();
            var testDate1 = new DateTime(2017, 11, 2);
            var testDate2 = new DateTime(2016, 10, 20);
            var testEntry1 = new FitbitEntry(testDate1, 500, 2.0, 310, 1, 155, new TimeSpan());
            var testEntry2 = new FitbitEntry(testDate2, 2000, 2.0, 310, 1, 155, new TimeSpan());

            testJournal.AddEntry(testEntry1);
            testJournal.AddEntry(testEntry2);

            Assert.AreEqual(2000, testJournal.GetEntryByDate(testDate2).Steps);
        }

        [TestMethod]
        public void When1EntryInMultipleMatchesHasProperDate()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();
            var testDate1 = new DateTime(2017, 11, 2);
            var testDate2 = new DateTime(2016, 10, 20);
            var testEntry1 = new FitbitEntry(testDate1, 500, 2.0, 310, 1, 155, new TimeSpan());
            var testEntry2 = new FitbitEntry(testDate2, 2000, 2.0, 310, 1, 155, new TimeSpan());

            testJournal.AddEntry(testEntry1);
            testJournal.AddEntry(testEntry2);

            Assert.AreEqual(testDate2, testJournal.GetEntryByDate(testDate2).Date);
        }

        #endregion
    }
}