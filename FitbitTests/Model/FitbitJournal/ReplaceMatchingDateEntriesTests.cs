using System;
using FitbitAnalysis_Phillip_Morris.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FitbitTests.Model.FitbitJournal
{
    /// <summary>
    /// Tests the ReplaceMatchingDateEntries method in the FitbitJournal class.
    /// 
    /// Tests:
    /// Replacing with different steps.
    /// Replacing with different distance.
    /// Replacing with different calories.
    /// Replacing with different floors.
    /// Replacing with different activity calories.
    /// </summary>
    [TestClass]
    public class ReplaceMatchingDateEntriesTests
    {
        #region Methods

        [TestMethod]
        public void WhenReplaceStepsOfFirst()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();
            var testDate = new DateTime(2017, 11, 2);
            var testEntry1 = new FitbitEntry(testDate, 500, 2.0, 310, 1, 155, new TimeSpan());
            var testEntry2 = new FitbitEntry(testDate, 2000, 2.0, 310, 1, 155, new TimeSpan());

            testJournal.AddEntry(testEntry1);
            testJournal.ReplaceMatchingDateEntries(testEntry2);

            Assert.AreEqual(2000, testJournal.GetEntryByDate(testDate).Steps);
        }

        [TestMethod]
        public void WhenReplaceDistanceOfFirst()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();
            var testDate = new DateTime(2017, 11, 2);
            var testEntry1 = new FitbitEntry(testDate, 500, 2.0, 310, 1, 155, new TimeSpan());
            var testEntry2 = new FitbitEntry(testDate, 500, 75.0, 310, 1, 155, new TimeSpan());

            testJournal.AddEntry(testEntry1);
            testJournal.ReplaceMatchingDateEntries(testEntry2);

            Assert.AreEqual(75.0, testJournal.GetEntryByDate(testDate).Distance);
        }

        [TestMethod]
        public void WhenReplaceCaloriesOfFirst()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();
            var testDate = new DateTime(2017, 11, 2);
            var testEntry1 = new FitbitEntry(testDate, 500, 75.0, 310, 1, 155, new TimeSpan());
            var testEntry2 = new FitbitEntry(testDate, 500, 75.0, 740, 1, 155, new TimeSpan());

            testJournal.AddEntry(testEntry1);
            testJournal.ReplaceMatchingDateEntries(testEntry2);

            Assert.AreEqual(740, testJournal.GetEntryByDate(testDate).CaloriesBurned);
        }

        [TestMethod]
        public void WhenReplaceFloorsOfFirst()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();
            var testDate = new DateTime(2017, 11, 2);
            var testEntry1 = new FitbitEntry(testDate, 500, 75.0, 310, 1, 155, new TimeSpan());
            var testEntry2 = new FitbitEntry(testDate, 500, 75.0, 310, 5, 155, new TimeSpan());

            testJournal.AddEntry(testEntry1);
            testJournal.ReplaceMatchingDateEntries(testEntry2);

            Assert.AreEqual(5, testJournal.GetEntryByDate(testDate).Floors);
        }

        [TestMethod]
        public void WhenReplaceActivityCaloriesOfFirst()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();
            var testDate = new DateTime(2017, 11, 2);
            var testEntry1 = new FitbitEntry(testDate, 500, 2.0, 310, 1, 155, new TimeSpan());
            var testEntry2 = new FitbitEntry(testDate, 500, 75.0, 310, 1, 72, new TimeSpan());

            testJournal.AddEntry(testEntry1);
            testJournal.ReplaceMatchingDateEntries(testEntry2);

            Assert.AreEqual(72, testJournal.GetEntryByDate(testDate).ActivityCalories);
        }

        #endregion
    }
}