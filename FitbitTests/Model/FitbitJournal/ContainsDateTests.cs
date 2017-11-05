using System;
using FitbitAnalysis_Phillip_Morris.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FitbitTests.Model.FitbitJournal
{
    /// <summary>
    /// Tests the ContainsDateTests method of the FitbitJournal class.
    /// 
    /// Tests:
    /// Does not contain date from single entry.
    /// Does contain date from single entry.
    /// Does contain date from multiple entries.
    /// Does not contain date from multiple entries.
    /// </summary>
    [TestClass]
    public class ContainsDateTests
    {
        [TestMethod]
        public void WhenDoesNotContainDate()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();
            var testDate = new DateTime(2017, 11, 2);
            var otherDate = new DateTime(2016, 3, 18);
            var testEntry1 = new FitbitEntry(testDate, 500, 2.0, 310, 1, 155, new TimeSpan());

            testJournal.AddEntry(testEntry1);

            Assert.AreEqual(false, testJournal.ContainsDate(otherDate));
        }

        [TestMethod]
        public void WhenContainsDate()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();
            var testDate = new DateTime(2017, 11, 2);
            var testEntry1 = new FitbitEntry(testDate, 500, 2.0, 310, 1, 155, new TimeSpan());

            testJournal.AddEntry(testEntry1);

            Assert.AreEqual(true, testJournal.ContainsDate(testDate));
        }

        [TestMethod]
        public void WhenContainsDateFromMultipleInJounral()
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

            Assert.AreEqual(true, testJournal.ContainsDate(testDate2));
        }

        [TestMethod]
        public void WhenDoesNotContainDateFromMultipleInJounral()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();
            var testDate1 = new DateTime(2017, 11, 2);
            var testDate2 = new DateTime(2016, 10, 20);
            var testDate3 = new DateTime(2016, 9, 4);
            var testEntry1 = new FitbitEntry(testDate1, 500, 2.0, 310, 1, 155, new TimeSpan());
            var testEntry2 = new FitbitEntry(testDate2, 2000, 2.0, 310, 1, 155, new TimeSpan());
            var testEntry3 = new FitbitEntry(testDate3, 900, 0.5, 400, 1, 155, new TimeSpan());
            var otherDate = new DateTime(2015, 6, 12);

            testJournal.AddEntry(testEntry1);
            testJournal.AddEntry(testEntry2);
            testJournal.AddEntry(testEntry3);

            Assert.AreEqual(false, testJournal.ContainsDate(otherDate));
        }
    }
}
