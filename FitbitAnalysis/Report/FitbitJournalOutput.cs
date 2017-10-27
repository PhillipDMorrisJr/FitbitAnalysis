using System;
using System.Collections.Generic;
using System.Globalization;
using FitbitAnalysis_Phillip_Morris.Model;

namespace FitbitAnalysis_Phillip_Morris.Report
{
    /// <summary>
    ///     Manages output.
    /// </summary>
    public class FitbitJournalOutput
    {
        #region Data members

        private readonly FitbitJournal fitbitJournal;
        private List<string> outputStatements;
        private int currentThreshold;
        private int currentAmountOfCategories;
        private int currentBinSize;

        #endregion

        #region Constructors

        public FitbitJournalOutput(FitbitJournal fitbitJournal)
        {
            this.fitbitJournal = fitbitJournal;
        }

        #endregion

        #region Methods
        /// <summary>
        ///     Gets the output.
        /// </summary>
        /// <returns>The output</returns>
        public string GetOutput(int threshold, int amountOfCategories, int binSize)
        {
            if (amountOfCategories > threshold)
            {
                //TODO: Add Popup
                throw new ArgumentException("Threshold must be greater than the amount of categories!");
            }
            if (threshold < 0 || amountOfCategories < 0 || binSize < 0)
            {
                //TODO: Popup
                throw new ArgumentException("Threshold must be positive!");
            }
            this.currentBinSize = binSize;
            this.currentThreshold = threshold;
            this.currentAmountOfCategories = amountOfCategories;
            this.initializeOutputStatments();

            return this.generateOutput();
        }

        /// <summary>
        ///     Initializes the output statments.
        /// </summary>
        private void initializeOutputStatments()
        {
            this.outputStatements = new List<string>();
            this.outputStatements.Add(Environment.NewLine + "Annual Statistics: ");
            this.addStepStatements(this.fitbitJournal);
            this.addBoundaryStatements(this.fitbitJournal);
            this.generateYearlyOutput();
        }

        /// <summary>
        ///     Adds the boundary statements.
        /// </summary>
        private void addBoundaryStatements(FitbitJournal currentFitbitJournal)
        {
            var lowerBound = 1;
            var upperBound = this.currentBinSize;

            for (var index = 0; index < this.currentAmountOfCategories; index++)
            {
                if (index == 0)
                {
                    this.addDaysWithStepBetweenBoundariesStatements(lowerBound-1, upperBound);
                }
                else if (index == this.currentAmountOfCategories - 1)
                {
                    this.addLastBoundaryStatement(lowerBound, currentFitbitJournal);
                }
                else
                {
                    this.addDaysWithStepBetweenBoundariesStatements(lowerBound, upperBound);
                }
                lowerBound += this.currentBinSize;
                upperBound += this.currentBinSize;
            }
        }

        /// <summary>
        ///     Adds the last boundary statement.
        /// </summary>
        /// <param name="lastBoundary">The last boundary.</param>
        /// <param name="currentFitbitJournal"></param>
        private void addLastBoundaryStatement(int lastBoundary, FitbitJournal currentFitbitJournal)
        {
            var boundarySix = "Days with " + lastBoundary.ToString("N0") + " or more: " + currentFitbitJournal.CountDaysWithStepsOver(lastBoundary).ToString("N0");
            this.outputStatements.Add(boundarySix);
        }

        /// <summary>
        ///     Generates the output.
        /// </summary>
        /// <returns></returns>
        private string generateOutput()
        {
            var output = "";
            foreach (var statement in this.outputStatements)
            {
                output += statement + Environment.NewLine ;
            }
            return output;
        }

        /// <summary>
        ///     Adds the step statements.
        /// </summary>
        private void addStepStatements(FitbitJournal currentFitbitJournal)
        {
            var daysWithOverTenThousandSteps = "The number of days with more than " + this.currentThreshold + " steps: " +
                                               this.fitbitJournal.CountDaysWithStepsOver(this.currentThreshold).ToString("N0");
            
            this.addMaxAndMinStepStatements(currentFitbitJournal.MaxSteps, currentFitbitJournal.MinSteps);
            this.averageStepsTakenStatement(currentFitbitJournal);
            this.outputStatements.Add(daysWithOverTenThousandSteps);
        }

        private void averageStepsTakenStatement(FitbitJournal currentCategoryManager)
        {
            var averageStepsTaken = "The average number of steps take all year: " +
                                    Math.Round(currentCategoryManager.AverageSteps, 2).ToString("N");
            this.outputStatements.Add(averageStepsTaken);
        }

        /// <summary>
        ///     Adds the maximum and minimum step statements.
        /// </summary>
        /// <param name="maxSteps">The maximum steps.</param>
        /// <param name="minSteps">The minimum steps.</param>
        private void addMaxAndMinStepStatements(int maxSteps, int minSteps)
        {
            this.addMostStepsStatement(maxSteps);
            this.addLeastStepsStatement(minSteps);
        }

        /// <summary>
        ///     Adds the least steps statement.
        /// </summary>
        /// <param name="minSteps">The minimum steps.</param>
        private void addLeastStepsStatement(int minSteps)
        {
            var fewestStepsTaken = "The fewest steps taken all year: " + minSteps.ToString("N0") + " on " +
                                   this.fitbitJournal.FindDateBasedOnSteps(minSteps).GetDateTimeFormats()[0];
            this.outputStatements.Add(fewestStepsTaken);
        }

        /// <summary>
        ///     Adds the most steps statement.
        /// </summary>
        /// <param name="maxSteps">The maximum steps.</param>
        private void addMostStepsStatement(int maxSteps)
        {
            var mostStepsTaken = "The most steps taken all year: " + maxSteps.ToString("N0") + " on " +
                                 this.fitbitJournal.FindDateBasedOnSteps(maxSteps).GetDateTimeFormats()[0];
            this.outputStatements.Add(mostStepsTaken);
        }

        private void addDaysWithStepBetweenBoundariesStatements(int lowerBoundary, int upperBoundary)
        {
            var daysWithStepsBetween = "Days with Steps between " + lowerBoundary.ToString("N0") + " and " + upperBoundary.ToString("N0") +
                                       " steps: " +
                                       this.fitbitJournal.CountDaysWithStepsBetween(lowerBoundary, upperBoundary).ToString("N0");

            this.outputStatements.Add(daysWithStepsBetween);
        }

        private void generateYearlyOutput()
        {
            var dateTime = new DateTimeFormatInfo();
            var months = dateTime.MonthNames;
            var monthlyCategoryManager = new FitbitJournal();

            for (var yearIndex = this.fitbitJournal.FirstEntryDate.Year;
                yearIndex <= this.fitbitJournal.LastEntryDate.Year;
                yearIndex++)
            {
                this.outputStatements.Add(Environment.NewLine + "Yearly Overview: " + Environment.NewLine);
                this.generateYearlyBreakDown(yearIndex);
                this.outputStatements.Add(Environment.NewLine + "Monthly Breakdown: ");
                this.generateMonthlyBreakDown(yearIndex, months, monthlyCategoryManager);
            }
        }

        private void generateYearlyBreakDown(int year)
        {
            var yearlyJournal = this.fitbitJournal.GetEntriesByYear(year);
            this.addStepStatements(yearlyJournal);
            this.addBoundaryStatements(yearlyJournal);

        }

        private void generateMonthlyBreakDown(int year, string[] months, FitbitJournal monthlyFitbitJournal)
        {
            for (var month = 1; month < months.Length; month++)
            {
                var monthCollection = this.fitbitJournal.GetEntriesOrderedDatesByMonthAndYear(month, year);

                this.addMonthlyEntryData(monthlyFitbitJournal, monthCollection);

                this.outputStatements.Add(Environment.NewLine);
                this.outputStatements.Add(months[month - 1] + " " + year + " (" +
                                          this.fitbitJournal.CountPerMonthAndYear(month, year) + " days of data)" +
                                          ": ");
                this.addMonthlyStatisticsStatements(monthlyFitbitJournal);
            }
        }

        private void addMonthlyStatisticsStatements(FitbitJournal monthlyCategoryManager)
        {
            if (!monthlyCategoryManager.IsEmpty())
            {
                this.addMaxAndMinStepStatements(monthlyCategoryManager.MaxSteps,
                    monthlyCategoryManager.MinSteps);
                this.averageStepsTakenStatement(monthlyCategoryManager);
                monthlyCategoryManager.ClearEntries();
            }
        }

        private void addMonthlyEntryData(FitbitJournal monthlyCategoryManager, List<FitbitEntry> monthCollection)
        {
            foreach (var fitbitEntry in monthCollection)
            {
                var monthlyFitbitEntry = this.fitbitJournal.GetEntryByDate(fitbitEntry.Date);

                monthlyCategoryManager.AddEntry(monthlyFitbitEntry);
            }
        }

        #endregion
    }


}