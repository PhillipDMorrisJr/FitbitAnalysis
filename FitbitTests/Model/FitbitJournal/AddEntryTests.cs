using System;
using FitbitAnalysis_Phillip_Morris.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FitbitTests.Model.FitbitJournal
{
    /// <summary>
    ///     Tests the AddEntry method of the FitbitJournal class.
    ///     Tests:
    ///     No entries added to journal.
    ///     1 entry in journal added.
    ///     Multiple entries added to journal.
    ///     Adding null entry throws NullArgumentException.
    /// </summary>
    [TestClass]
    public class AddEntryTests
    {
        #region Methods

        [TestMethod]
        public void WhenAddNoneCountShouldBe0()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();

            Assert.AreEqual(0, testJournal.Count);
        }

        [TestMethod]
        public void WhenAdd1CountShouldBe1()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();
            var testEntry = new FitbitEntry(DateTime.Today, 500, 2.0, 310, 1, 155, new TimeSpan());

            testJournal.AddEntry(testEntry);

            Assert.AreEqual(1, testJournal.Count);
        }

        [TestMethod]
        public void WhenAdd4CountIs4()
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

            Assert.AreEqual(4, testJournal.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WhenAddingNullToJournal()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();

            testJournal.AddEntry(null);
        }

        #endregion
    }
}