using System;
using FitbitAnalysis_Phillip_Morris.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FitbitTests.Model.FitbitJournal
{
    /// <summary>
    ///     Tests the CountDaysWithStepsBetweenTests method in the FitbitJournal class.
    /// </summary>
    [TestClass]
    public class CountDaysWithStepsBetweenTests
    {
        #region Methods

        [TestMethod]
        public void WhenNoEntriesInJournal()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();
            var testEntry = new FitbitEntry(DateTime.Today, 500, 2.0, 310, 1, 155);

            Assert.AreEqual(0, testJournal.CountDaysWithStepsBetween(0, 100));
        }

        [TestMethod]
        public void WhenOnlyOneInJournalAndMatches()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();
            var testEntry = new FitbitEntry(DateTime.Today, 500, 2.0, 310, 1, 155);

            testJournal.AddEntry(testEntry);

            Assert.AreEqual(1, testJournal.CountDaysWithStepsBetween(0, 600));
        }

        [TestMethod]
        public void WhenMultiplesInJournalAndNoneMatch()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();
            var testEntry = new FitbitEntry(DateTime.Today, 1000, 2.0, 310, 1, 155);
            var testEntry2 = new FitbitEntry(DateTime.Today, 5000, 2.0, 310, 1, 155);
            var testEntry3 = new FitbitEntry(DateTime.Today, 1000, 2.0, 310, 1, 155);

            testJournal.AddEntry(testEntry);
            testJournal.AddEntry(testEntry2);
            testJournal.AddEntry(testEntry3);

            Assert.AreEqual(0, testJournal.CountDaysWithStepsBetween(0, 600));
        }

        [TestMethod]
        public void WhenMultipleInJournalAndOneMatches()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();
            var testEntry = new FitbitEntry(DateTime.Today, 500, 2.0, 310, 1, 155);
            var testEntry2 = new FitbitEntry(DateTime.Today, 1000, 2.0, 310, 1, 155);
            var testEntry3 = new FitbitEntry(DateTime.Today, 1200, 2.0, 310, 1, 155);

            testJournal.AddEntry(testEntry);
            testJournal.AddEntry(testEntry2);
            testJournal.AddEntry(testEntry3);

            Assert.AreEqual(1, testJournal.CountDaysWithStepsBetween(0, 600));
        }

        [TestMethod]
        public void WhenMultipleInJournalAndMultipleMatche()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();
            var testEntry = new FitbitEntry(DateTime.Today, 500, 2.0, 310, 1, 155);
            var testEntry2 = new FitbitEntry(DateTime.Today, 1000, 2.0, 310, 1, 155);
            var testEntry3 = new FitbitEntry(DateTime.Today, 1200, 2.0, 310, 1, 155);
            var testEntry4 = new FitbitEntry(DateTime.Today, 800, 2.0, 310, 1, 155);

            testJournal.AddEntry(testEntry);
            testJournal.AddEntry(testEntry2);
            testJournal.AddEntry(testEntry3);
            testJournal.AddEntry(testEntry4);

            Assert.AreEqual(2, testJournal.CountDaysWithStepsBetween(2, 900));
        }

        #endregion
    }
}