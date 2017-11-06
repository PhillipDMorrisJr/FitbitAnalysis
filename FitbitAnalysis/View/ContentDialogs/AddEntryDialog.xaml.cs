using System;
using Windows.UI.Xaml;
using FitbitAnalysis_Phillip_Morris.Model;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace FitbitAnalysis_Phillip_Morris.View.ContentDialogs
{
    /// <summary>
    /// Creates entry when correct formatted values are passed in
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.ContentDialog" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class AddEntryDialog
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AddEntryDialog"/> class.
        /// </summary>
        public AddEntryDialog()
        {
            this.InitializeComponent();
            this.IsGoodFormat = true;
        }

        #endregion

        #region Methods

        private void addEntry_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var entryDate = this.date.Date.Date;
                var entrySteps = int.Parse(this.steps.Text);
                var entryDistance = double.Parse(this.distance.Text);
                var entryCaloriesBurned = int.Parse(this.caloriesBurned.Text);
                var entryFloors = int.Parse(this.floors.Text);
                var entryActivityCalories = int.Parse(this.activityCalories.Text);
                var entryActiveMinutes = MinuteConverter.ConvertMinutesToTimeSpan(int.Parse(this.activeMinutes.Text));

                var fitbitEntry = new FitbitEntry(entryDate, entrySteps, entryDistance, entryCaloriesBurned,
                    entryFloors, entryActivityCalories, entryActiveMinutes);
                this.FitbitEntry = fitbitEntry;
                this.doneAddingEntry_OnClick(sender, e);
            }

            catch (FormatException)
            {
                this.handleWhenNoEntryCreated(sender, e);
            }
            catch (ArgumentException)
            {
                this.handleWhenNoEntryCreated(sender, e);
            }
        }

        private void handleWhenNoEntryCreated(object sender, RoutedEventArgs e)
        {
            this.IsGoodFormat = false;
            this.doneAddingEntry_OnClick(sender, e);
        }

        private void doneAddingEntry_OnClick(object sender, RoutedEventArgs e)
        {
            this.entryDialog.Hide();
        }

        #endregion

        #region Property

        /// <summary>
        /// Gets the fitbit entry.
        /// </summary>
        /// <value>
        /// The fitbit entry.
        /// </value>
        public FitbitEntry FitbitEntry { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this instance is good format.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is good format; otherwise, <c>false</c>.
        /// </value>
        public bool IsGoodFormat { get; private set; }

        #endregion

        #region Data member

        #endregion
    }
}