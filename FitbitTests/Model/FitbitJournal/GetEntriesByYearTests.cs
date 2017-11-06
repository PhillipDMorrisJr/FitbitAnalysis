using System;
using FitbitAnalysis_Phillip_Morris.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FitbitTests.Model.FitbitJournal
{
    /// <summary>
    ///     Tests the GetEntriesByYear method of the FitbitJournal class.
    ///     Tests:
    ///     When no entries are in the journal.
    ///     When none match from multiple in journal.
    ///     1 matches from multiple in journal.
    ///     Multiple match from multiple in jounrnal.
    ///     All entries match.
    ///     Check proper year is found.
    /// </summary>
    [TestClass]
    public class GetEntriesByYearTests
    {
        #region Methods

        [TestMethod]
        public void WhenNoEntriesInJournal()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();

            var resultJournal = testJournal.GetEntriesByYear(2017);

            Assert.AreEqual(0, resultJournal.Count);
        }

        [TestMethod]
        public void WhenNoEntriesMatchFromMultipleInJournal()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();
            var testDate1 = new DateTime(2017, 11, 2);
            var testDate2 = new DateTime(2016, 10, 20);
            var testDate3 = new DateTime(2016, 9, 4);
            var testEntry1 = new FitbitEntry(testDate1, 500, 2.0, 310, 1, 155, new TimeSpan());
            var testEntry2 = new FitbitEntry(testDate2, 2000, 2.0, 310, 1, 155, new TimeSpan());
            var testEntry3 = new FitbitEntry(testDate3, 900, 0.5, 400, 1, 155, new TimeSpan());

            testJournal.AddEntry(testEntry1);
            testJournal.AddEntry(testEntry2);
            testJournal.AddEntry(testEntry3);

            var resultJournal = testJournal.GetEntriesByYear(2015);

            Assert.AreEqual(0, resultJournal.Count);
        }

        [TestMethod]
        public void When1EntryMatchesFromMultipleInJournal()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();
            var testDate1 = new DateTime(2017, 11, 2);
            var testDate2 = new DateTime(2016, 10, 20);
            var testDate3 = new DateTime(2016, 9, 4);
            var testEntry1 = new FitbitEntry(testDate1, 500, 2.0, 310, 1, 155, new TimeSpan());
            var testEntry2 = new FitbitEntry(testDate2, 2000, 2.0, 310, 1, 155, new TimeSpan());
            var testEntry3 = new FitbitEntry(testDate3, 900, 0.5, 400, 1, 155, new TimeSpan());

            testJournal.AddEntry(testEntry1);
            testJournal.AddEntry(testEntry2);
            testJournal.AddEntry(testEntry3);

            var resultJournal = testJournal.GetEntriesByYear(2017);

            Assert.AreEqual(1, resultJournal.Count);
        }

        [TestMethod]
        public void When2EntriesMatchFromMultipleInJournal()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();
            var testDate1 = new DateTime(2017, 11, 2);
            var testDate2 = new DateTime(2016, 10, 20);
            var testDate3 = new DateTime(2016, 9, 4);
            var testEntry1 = new FitbitEntry(testDate1, 500, 2.0, 310, 1, 155, new TimeSpan());
            var testEntry2 = new FitbitEntry(testDate2, 2000, 2.0, 310, 1, 155, new TimeSpan());
            var testEntry3 = new FitbitEntry(testDate3, 900, 0.5, 400, 1, 155, new TimeSpan());

            testJournal.AddEntry(testEntry1);
            testJournal.AddEntry(testEntry2);
            testJournal.AddEntry(testEntry3);

            var resultJournal = testJournal.GetEntriesByYear(2016);

            Assert.AreEqual(2, resultJournal.Count);
        }

        [TestMethod]
        public void WhenAllEntriesMatchFromMultipleInJournal()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();
            var testDate1 = new DateTime(2016, 11, 2);
            var testDate2 = new DateTime(2016, 10, 20);
            var testDate3 = new DateTime(2016, 9, 4);
            var testEntry1 = new FitbitEntry(testDate1, 500, 2.0, 310, 1, 155, new TimeSpan());
            var testEntry2 = new FitbitEntry(testDate2, 2000, 2.0, 310, 1, 155, new TimeSpan());
            var testEntry3 = new FitbitEntry(testDate3, 900, 0.5, 400, 1, 155, new TimeSpan());

            testJournal.AddEntry(testEntry1);
            testJournal.AddEntry(testEntry2);
            testJournal.AddEntry(testEntry3);

            var resultJournal = testJournal.GetEntriesByYear(2016);

            Assert.AreEqual(3, resultJournal.Count);
        }

        [TestMethod]
        public void CheckYearFromEntriesMatchFromMultipleInJournal()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();
            var testDate1 = new DateTime(2016, 11, 2);
            var testDate2 = new DateTime(2016, 10, 20);
            var testDate3 = new DateTime(2016, 9, 4);
            var testEntry1 = new FitbitEntry(testDate1, 500, 2.0, 310, 1, 155, new TimeSpan());
            var testEntry2 = new FitbitEntry(testDate2, 2000, 2.0, 310, 1, 155, new TimeSpan());
            var testEntry3 = new FitbitEntry(testDate3, 900, 0.5, 400, 1, 155, new TimeSpan());

            testJournal.AddEntry(testEntry1);
            testJournal.AddEntry(testEntry2);
            testJournal.AddEntry(testEntry3);

            var resultJournal = testJournal.GetEntriesByYear(2016);

            Assert.AreEqual(2016, resultJournal.Entries[0].Date.Year);
        }

        #endregion
    }
}