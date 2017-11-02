using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitbitAnalysis_Phillip_Morris.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FitbitTests.Model.FitbitJournal
{
    /// <summary>
    /// Tests the CountDaysWithStepsOver method in the FitbitJournal class.
    /// </summary>
    [TestClass]
    public class CountDaysWithStepsOverTests
    {
        [TestMethod]
        public void WhenNoEntriesInJournal()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();

            Assert.AreEqual(0, testJournal.CountDaysWithStepsOver(1000));
        }

        [TestMethod]
        public void WhenOnlyOneInJournalAndMatches()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();
            var testEntry = new FitbitEntry(DateTime.Today, 500, 2.0, 310, 1, 155);

            testJournal.AddEntry(testEntry);

            Assert.AreEqual(1, testJournal.CountDaysWithStepsOver(200));
        }

        [TestMethod]
        public void WhenMultiplesInJournalAndNoneMatch()
        {
            var testJournal = new FitbitAnalysis_Phillip_Morris.Model.FitbitJournal();
            var testEntry = new FitbitEntry(DateTime.Today, 1000, 2.0, 310, 1, 155);
            var testEntry2 = new FitbitEntry(DateTime.Today, 2000, 2.0, 310, 1, 155);
            var testEntry3 = new FitbitEntry(DateTime.Today, 3000, 2.0, 310, 1, 155);

            testJournal.AddEntry(testEntry);
            testJournal.AddEntry(testEntry2);
            testJournal.AddEntry(testEntry3);

            Assert.AreEqual(0, testJournal.CountDaysWithStepsOver(5000));
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

            Assert.AreEqual(1, testJournal.CountDaysWithStepsOver(1100));
        }

        [TestMethod]
        public void WhenMultipleInJournalAndMultipleMatch()
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

            Assert.AreEqual(2, testJournal.CountDaysWithStepsOver(900));
        }
    }
}
