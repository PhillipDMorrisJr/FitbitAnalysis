using System;
using System.Collections.Generic;
using System.Linq;

namespace FitbitAnalysis_Phillip_Morris.Model
{
    /// <summary>
    ///     Manages the category by arranging various collections objects.
    /// </summary>
    public class FitbitJournal
    {
        #region Properties

        /// <summary>
        ///     Gets the minimum steps.
        /// </summary>
        /// <value>
        ///     The minimum steps.
        /// </value>
        public int MinSteps => this.Entries.Min(entry => entry.Steps);

        /// <summary>
        ///     Gets the maximum steps.
        /// </summary>
        /// <value>
        ///     The maximum steps.
        /// </value>
        public int MaxSteps => this.Entries.Max(entry => entry.Steps);

        /// <summary>
        ///     Gets the entries.
        /// </summary>
        /// <value>
        ///     The entries.
        /// </value>
        public List<FitbitEntry> Entries { get; }

        /// <summary>
        ///     Gets the average steps.
        /// </summary>
        /// <value>
        ///     The average steps.
        /// </value>
        public double AverageSteps => this.Entries.Average(entry => entry.Steps);

        /// <summary>
        ///     Gets the first entry date.
        /// </summary>
        /// <value>
        ///     The first entry date.
        /// </value>
        public DateTime FirstEntryDate => this.getAllEntriesOrderedByDate()[0].Date;

        /// <summary>
        ///     Gets the last entry date.
        /// </summary>
        /// <value>
        ///     The last entry date.
        /// </value>
        public DateTime LastEntryDate => this.getAllEntriesOrderedByDate()[this.Entries.Count - 1].Date;

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count => this.Entries.Count;
        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="FitbitJournal" /> class.
        /// </summary>
        public FitbitJournal()
        {
            this.Entries = new List<FitbitEntry>();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Gets the entries by year.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns>
        ///     a Journal containing the yearly entries for the year in the year parameter
        /// </returns>
        public FitbitJournal GetEntriesByYear(int year)
        {
            var entries = this.Entries.FindAll(entry => entry.Date.Year == year);
            var yearlyJournal = new FitbitJournal();

            foreach (var entry in entries)
            {
                yearlyJournal.AddEntry(entry);
            }
            return yearlyJournal;
        }

        /// <summary>
        ///     Adds the entry.
        /// </summary>
        /// <param name="fitbitEntry"> The entry to be added to fitbit collection</param>
        public void AddEntry(FitbitEntry fitbitEntry)
        {
            this.Entries.Add(fitbitEntry ?? throw new ArgumentException("Fitbit must not be null"));
        }

        /// <summary>
        ///     Finds the date based on stepTracker.
        /// </summary>
        /// <param name="steps">The step count.</param>
        /// <returns></returns>
        public DateTime FindDateBasedOnSteps(int steps)
        {
            var currentDate = this.Entries.Find(fitbitEntry => fitbitEntry.Steps == steps).Date;
            return currentDate;
        }

        /// <summary>
        ///     Counts the days with stepTracker between.
        /// </summary>
        /// <param name="lowerBound">The lower bound.</param>
        /// <param name="upperBound">The upper bound.</param>
        /// <returns></returns>
        public int CountDaysWithStepsBetween(int lowerBound, int upperBound)
        {
            int count;
            count = this.Entries.Count(
                fitbitEntry => fitbitEntry.Steps >= lowerBound && fitbitEntry.Steps <= upperBound);
            return count;
        }

        /// <summary>
        ///     Counts the days with stepTracker over.
        /// </summary>
        /// <param name="bound">The bound.</param>
        /// <returns></returns>
        public int CountDaysWithStepsOver(int bound)
        {
            var count = this.Entries.Count(fitbitEntry => fitbitEntry.Steps >= bound);
            return count;
        }

        /// <summary>
        ///     Counts the days with stepTracker less than.
        /// </summary>
        /// <param name="bound">The bound.</param>
        /// <returns></returns>
        public int CountDaysWithStepsLessThan(int bound)
        {
            var count = this.Entries.Count(fitbitEntry => fitbitEntry.Steps <= bound);
            return count;
        }

        /// <summary>
        ///     Gets the ordered dateTracker by month.
        /// </summary>
        /// <param name="month">The month.</param>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<FitbitEntry> GetEntriesOrderedDatesByMonthAndYear(int month, int year)
        {
            var copyFitbitEntries = this.Entries;

            var fitbitEntriesInYear = copyFitbitEntries.FindAll(fitbitEntry => fitbitEntry.Date.Year == year);
            var fitbitEntriesInMonth = fitbitEntriesInYear.FindAll(fitbitEntry => fitbitEntry.Date.Month == month);
            var fitbitEntriesorderedByDates = fitbitEntriesInMonth.OrderBy(fitbitEntry => fitbitEntry.Date);

            return fitbitEntriesorderedByDates.ToList();
        }

        /// <summary>
        ///     Clears all trackers for the instance of this class.
        /// </summary>
        public void ClearEntries()
        {
            this.Entries.Clear();
        }

        /// <summary>
        ///     Gets the entries associated with the date.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public FitbitEntry GetEntryByDate(DateTime date)
        {
            return this.Entries.Find(fitbitEntry => fitbitEntry.Date.Equals(date));
        }

        /// <summary>
        ///     Checks if this instance is empty.
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return this.Entries.Count == 0;
        }

        /// <summary>
        ///     Determines whether the specified date contains date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>
        ///     <c>true</c> if the specified date contains date; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsDate(DateTime date)
        {
            return this.Entries.Exists(fitbitEntry => fitbitEntry.Date.Equals(date));
        }

        /// <summary>
        ///     Gets all entries ordered by date.
        /// </summary>
        /// <returns></returns>
        private List<FitbitEntry> getAllEntriesOrderedByDate()
        {
            var fitbitEntriesOrderedByDates = this.Entries.OrderBy(fitbitEntry => fitbitEntry.Date.Year)
                                                  .ThenBy(fitbitEntry => fitbitEntry.Date.Month);

            return fitbitEntriesOrderedByDates.ToList();
        }

        /// <summary>
        ///     Replaces the matching date entries.
        /// </summary>
        /// <param name="fitbitEntry">The fitbit entry.</param>
        public void ReplaceMatchingDateEntries(FitbitEntry fitbitEntry)
        {
            this.Entries.RemoveAll(entry => entry.Date.Equals(fitbitEntry.Date));
            this.AddEntry(fitbitEntry);
        }

        /// <summary>
        ///     Counts the per month and year.
        /// </summary>
        /// <param name="month">The month.</param>
        /// <param name="year">The year.</param>
        /// <returns></returns>
        public int CountPerMonthAndYear(int month, int year)
        {
            var entries = this.Entries.FindAll(entry => entry.Date.Month == month && entry.Date.Year == year);
            return entries.Count;
        }

        #endregion
    }
}