using System;
using FitbitAnalysis_Phillip_Morris.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FitbitTests.Model.FitbitJournal
{
    /// <summary>
    /// Tests the CountDaysWithLessStepsThan method of the FitbitJournal class.
    /// 
    /// Tests:
    /// No entries in journal.
    /// 1 entry in journal and matches.
    /// Multiple entries in journal and none match criteria.
    /// Multiple entries in journal and 1 matches criteria.
    /// Multiple entries in journal and multiple match criteria.
    /// Multiple entries in journal and all match criteria.
    /// </summary>
    [TestClass]
    public class CountDaysWithStepsLessThanTests
    {
        [TestMethod]
        public void WhenNoEntriesInJournal()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();

            Assert.AreEqual(0, testJournal.CountDaysWithStepsLessThan(1000));
        }

        [TestMethod]
        public void WhenOnlyOneInJournalAndMatches()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();
            var testEntry = new FitbitEntry(DateTime.Today, 500, 2.0, 310, 1, 155, new TimeSpan());

            testJournal.AddEntry(testEntry);

            Assert.AreEqual(1, testJournal.CountDaysWithStepsLessThan(600));
        }

        [TestMethod]
        public void WhenMultiplesInJournalAndNoneMatch()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();
            var testEntry = new FitbitEntry(DateTime.Today, 1000, 2.0, 310, 1, 155, new TimeSpan());
            var testEntry2 = new FitbitEntry(DateTime.Today, 2000, 2.0, 310, 1, 155, new TimeSpan());
            var testEntry3 = new FitbitEntry(DateTime.Today, 3000, 2.0, 310, 1, 155, new TimeSpan());

            testJournal.AddEntry(testEntry);
            testJournal.AddEntry(testEntry2);
            testJournal.AddEntry(testEntry3);

            Assert.AreEqual(0, testJournal.CountDaysWithStepsLessThan(900));
        }

        [TestMethod]
        public void WhenMultipleInJournalAndOneMatches()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();
            var testEntry = new FitbitEntry(DateTime.Today, 500, 2.0, 310, 1, 155, new TimeSpan());
            var testEntry2 = new FitbitEntry(DateTime.Today, 1000, 2.0, 310, 1, 155, new TimeSpan());
            var testEntry3 = new FitbitEntry(DateTime.Today, 1200, 2.0, 310, 1, 155, new TimeSpan());

            testJournal.AddEntry(testEntry);
            testJournal.AddEntry(testEntry2);
            testJournal.AddEntry(testEntry3);

            Assert.AreEqual(1, testJournal.CountDaysWithStepsLessThan(900));
        }

        [TestMethod]
        public void WhenMultipleInJournalAndMultipleMatch()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();
            var testEntry = new FitbitEntry(DateTime.Today, 500, 2.0, 310, 1, 155, new TimeSpan());
            var testEntry2 = new FitbitEntry(DateTime.Today, 1000, 2.0, 310, 1, 155, new TimeSpan());
            var testEntry3 = new FitbitEntry(DateTime.Today, 1200, 2.0, 310, 1, 155, new TimeSpan());
            var testEntry4 = new FitbitEntry(DateTime.Today, 800, 2.0, 310, 1, 155, new TimeSpan());

            testJournal.AddEntry(testEntry);
            testJournal.AddEntry(testEntry2);
            testJournal.AddEntry(testEntry3);
            testJournal.AddEntry(testEntry4);

            Assert.AreEqual(2, testJournal.CountDaysWithStepsLessThan(900));
        }

        [TestMethod]
        public void WhenMultipleInJournalAndAllMatch()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();
            var testEntry = new FitbitEntry(DateTime.Today, 500, 2.0, 310, 1, 155, new TimeSpan());
            var testEntry2 = new FitbitEntry(DateTime.Today, 1000, 2.0, 310, 1, 155, new TimeSpan());
            var testEntry3 = new FitbitEntry(DateTime.Today, 1200, 2.0, 310, 1, 155, new TimeSpan());
            var testEntry4 = new FitbitEntry(DateTime.Today, 800, 2.0, 310, 1, 155, new TimeSpan());

            testJournal.AddEntry(testEntry);
            testJournal.AddEntry(testEntry2);
            testJournal.AddEntry(testEntry3);
            testJournal.AddEntry(testEntry4);

            Assert.AreEqual(4, testJournal.CountDaysWithStepsLessThan(1500));
        }
    }
}
