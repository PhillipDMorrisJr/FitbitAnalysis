using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FitbitAnalysis_Phillip_Morris.Model;

namespace FitbitTests
{
    [TestClass]
    public class WhenTestingAddFitbitEntryToJournal
    {
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void WhenAddingNullToJournal()
        { 
            FitbitJournal fitbitJournal = new FitbitJournal();
            fitbitJournal.AddEntry(null);
        }



    }
}
