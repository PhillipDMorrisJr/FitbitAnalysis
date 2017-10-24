using FitbitAnalysis_Phillip_Morris.Model;

namespace FitbitAnalysis_Phillip_Morris.View.Report
{
    /// <summary>
    ///     Manages output.
    /// </summary>
    public class FitbitJournalOutput
    {
        #region Constructors

        public FitbitJournalOutput(FitbitJournal fitbitJournal)
        {
            this.fitbitJournal = fitbitJournal;
        }

        #endregion

        #region Data members

        private readonly FitbitJournal fitbitJournal;
        private List<string> outputStatements;
        private int currentThreshold;
        private int currentAmountOfCategories;

        #endregion

        #region Methods

        /// <summary>
        ///     Gets the output.
        /// </summary>
        /// <returns>The output</returns>
        public string GetOutput(int threshold, int amountOfCategories)
        {
            if (amountOfCategories > threshold)
                throw new ArgumentException("Threshold must be greater than the amount of categories!");
            if (threshold < 0)
                throw new ArgumentException("Threshold must be positive!");

            if (amountOfCategories < 0)
                throw new ArgumentException("Amount of categories must be positive");

            currentThreshold = threshold;
            currentAmountOfCategories = amountOfCategories;
            initializeOutputStatments();

            return generateOutput();
        }

        /// <summary>
        ///     Initializes the output statments.
        /// </summary>
        private void initializeOutputStatments()
        {
            outputStatements = new List<string>();
            outputStatements.Add(Environment.NewLine + "Annual Statistics: ");
            addStepStatements(fitbitJournal);
            addBoundaryStatements(fitbitJournal);
            generateYearlyOutput();
        }

        /// <summary>
        ///     Adds the boundary statements.
        /// </summary>
        private void addBoundaryStatements(FitbitJournal currentFitbitJournal)
        {
            var lowerBound = 1;
            var upperBound = currentThreshold;

            for (var index = 0; index < currentAmountOfCategories; index++)
            {
                if (index == 0)
                    addDaysWithStepBetweenBoundariesStatements(lowerBound - 1, upperBound);
                else if (index == currentAmountOfCategories - 1)
                    addLastBoundaryStatement(lowerBound, currentFitbitJournal);
                else
                    addDaysWithStepBetweenBoundariesStatements(lowerBound, upperBound);
                lowerBound += currentThreshold;
                upperBound += currentThreshold;
            }
        }

        /// <summary>
        ///     Adds the last boundary statement.
        /// </summary>
        /// <param name="lastBoundary">The last boundary.</param>
        /// <param name="currentCategoryManager"></param>
        private void addLastBoundaryStatement(int lastBoundary, FitbitJournal currentCategoryManager)
        {
            var boundarySix = "Days with " + lastBoundary.ToString("N0") + " or more: " +
                              currentCategoryManager.CountDaysWithStepsOver(lastBoundary).ToString("N0");
            outputStatements.Add(boundarySix);
        }

        /// <summary>
        ///     Generates the output.
        /// </summary>
        /// <returns></returns>
        private string generateOutput()
        {
            var output = "";
            foreach (var statement in outputStatements)
                output += statement + Environment.NewLine;
            return output;
        }

        /// <summary>
        ///     Adds the step statements.
        /// </summary>
        private void addStepStatements(FitbitJournal currentFitbitJournal)
        {
            var daysWithOverTenThousandSteps = "The number of days with more than " + currentThreshold + " steps: " +
                                               fitbitJournal.CountDaysWithStepsOver(currentThreshold).ToString("N0");

            addMaxAndMinStepStatements(currentFitbitJournal.MaxSteps, currentFitbitJournal.MinSteps);
            averageStepsTakenStatement(currentFitbitJournal);
            outputStatements.Add(daysWithOverTenThousandSteps);
        }

        private void averageStepsTakenStatement(FitbitJournal currentCategoryManager)
        {
            var averageStepsTaken = "The average number of steps take all year: " +
                                    Math.Round(currentCategoryManager.AverageSteps, 2).ToString("N");
            outputStatements.Add(averageStepsTaken);
        }

        /// <summary>
        ///     Adds the maximum and minimum step statements.
        /// </summary>
        /// <param name="maxSteps">The maximum steps.</param>
        /// <param name="minSteps">The minimum steps.</param>
        private void addMaxAndMinStepStatements(int maxSteps, int minSteps)
        {
            addMostStepsStatement(maxSteps);
            addLeastStepsStatement(minSteps);
        }

        /// <summary>
        ///     Adds the least steps statement.
        /// </summary>
        /// <param name="minSteps">The minimum steps.</param>
        private void addLeastStepsStatement(int minSteps)
        {
            var fewestStepsTaken = "The fewest steps taken all year: " + minSteps.ToString("N0") + " on " +
                                   fitbitJournal.FindDateBasedOnSteps(minSteps).GetDateTimeFormats()[0];
            outputStatements.Add(fewestStepsTaken);
        }

        /// <summary>
        ///     Adds the most steps statement.
        /// </summary>
        /// <param name="maxSteps">The maximum steps.</param>
        private void addMostStepsStatement(int maxSteps)
        {
            var mostStepsTaken = "The most steps taken all year: " + maxSteps.ToString("N0") + " on " +
                                 fitbitJournal.FindDateBasedOnSteps(maxSteps).GetDateTimeFormats()[0];
            outputStatements.Add(mostStepsTaken);
        }

        private void addDaysWithStepBetweenBoundariesStatements(int lowerBoundary, int upperBoundary)
        {
            var daysWithStepsBetween = "Days with Steps between " + lowerBoundary.ToString("N0") + " and " +
                                       upperBoundary.ToString("N0") +
                                       " steps: " +
                                       fitbitJournal.CountDaysWithStepsBetween(lowerBoundary, upperBoundary)
                                           .ToString("N0");

            outputStatements.Add(daysWithStepsBetween);
        }

        private void generateYearlyOutput()
        {
            var dateTime = new DateTimeFormatInfo();
            var months = dateTime.MonthNames;
            var monthlyCategoryManager = new FitbitJournal();

            for (var yearIndex = fitbitJournal.FirstEntryDate.Year;
                yearIndex <= fitbitJournal.LastEntryDate.Year;
                yearIndex++)
            {
                outputStatements.Add(Environment.NewLine + "Yearly Overview: " + Environment.NewLine);
                generateYearlyBreakDown(yearIndex);
                outputStatements.Add(Environment.NewLine + "Monthly Breakdown: ");
                generateMonthlyBreakDown(yearIndex, months, monthlyCategoryManager);
            }
        }

        private void generateYearlyBreakDown(int year)
        {
            var yearlyJournal = fitbitJournal.GetEntriesByYear(year);
            addStepStatements(yearlyJournal);
            addBoundaryStatements(yearlyJournal);
        }


        private void generateMonthlyBreakDown(int year, string[] months, FitbitJournal monthlyFitbitJournal)
        {
            for (var month = 1; month < months.Length; month++)
            {
                var monthCollection = fitbitJournal.GetEntriesOrderedDatesByMonthAndYear(month, year);

                addMonthlyEntryData(monthlyFitbitJournal, monthCollection);

                outputStatements.Add(Environment.NewLine);
                outputStatements.Add(months[month - 1] + " " + year + " (" +
                                     fitbitJournal.CountPerMonthAndYear(month, year) + " days of data)" +
                                     ": ");
                addMonthlyStatisticsStatements(monthlyFitbitJournal);
            }
        }

        private void addMonthlyStatisticsStatements(FitbitJournal monthlyCategoryManager)
        {
            if (!monthlyCategoryManager.IsEmpty())
            {
                addMaxAndMinStepStatements(monthlyCategoryManager.MaxSteps,
                    monthlyCategoryManager.MinSteps);
                averageStepsTakenStatement(monthlyCategoryManager);
                monthlyCategoryManager.ClearEntries();
            }
        }

        private void addMonthlyEntryData(FitbitJournal monthlyCategoryManager, List<FitbitEntry> monthCollection)
        {
            foreach (var fitbitEntry in monthCollection)
            {
                var monthlyFitbitEntry = fitbitJournal.GetEntryByDate(fitbitEntry.Date);

                monthlyCategoryManager.AddEntry(monthlyFitbitEntry);
            }
        }

        #endregion
    }
}