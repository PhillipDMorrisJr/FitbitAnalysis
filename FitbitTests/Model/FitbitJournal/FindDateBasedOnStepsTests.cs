using System;
using FitbitAnalysis_Phillip_Morris.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FitbitTests.Model.FitbitJournal
{
    /// <summary>
    /// 1 entry in journal matches.
    /// 1 entry from multiple in journal matches.
    /// </summary>
    [TestClass]
    public class FindDateBasedOnStepsTests
    {
        [TestMethod]
        public void WhenEntryInJournalMatches()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();
            var testDate = new DateTime(2017, 11, 2);
            var testEntry1 = new FitbitEntry(testDate, 1234, 2.0, 310, 1, 155, new TimeSpan());

            testJournal.AddEntry(testEntry1);

            Assert.AreEqual(testDate, testJournal.FindDateBasedOnSteps(1234));
        }

        [TestMethod]
        public void When1EntryInMultipleMatches()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();
            var testDate1 = new DateTime(2017, 11, 2);
            var testDate2 = new DateTime(2016, 10, 20);
            var testDate3 = new DateTime(2014, 6, 14);
            var testEntry1 = new FitbitEntry(testDate1, 500, 2.0, 310, 1, 155, new TimeSpan());
            var testEntry2 = new FitbitEntry(testDate2, 2000, 2.0, 310, 1, 155, new TimeSpan());
            var testEntry3 = new FitbitEntry(testDate3, 7300, 2.0, 310, 1, 155, new TimeSpan());

            testJournal.AddEntry(testEntry1);
            testJournal.AddEntry(testEntry2);
            testJournal.AddEntry(testEntry3);

            Assert.AreEqual(testDate2, testJournal.FindDateBasedOnSteps(2000));
        }
    }
}
