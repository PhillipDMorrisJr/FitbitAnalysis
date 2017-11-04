using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FitbitAnalysis_Phillip_Morris.Model;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace FitbitAnalysis_Phillip_Morris.View
{
    public sealed partial class AddEntryDialog : ContentDialog
    {
        #region Constructors

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

                var fitbitEntry = new FitbitEntry(entryDate, entrySteps, entryDistance, entryCaloriesBurned,
                    entryFloors,
                    entryActivityCalories, new TimeSpan());
                this.FitbitEntry = fitbitEntry;
                this.doneAddingEntry_OnClick(sender, e);
            }
            catch (FormatException)
            {
                this.IsGoodFormat = false;
                this.doneAddingEntry_OnClick(sender, e);
            }
            catch (ArgumentException)
            {
                this.IsGoodFormat = false;
                this.doneAddingEntry_OnClick(sender, e);
            }
        }

        private void doneAddingEntry_OnClick(object sender, RoutedEventArgs e)
        {
            this.entryDialog.Hide();
        }

        #endregion

        #region Property

        public FitbitEntry FitbitEntry { get; private set; }
        public bool IsGoodFormat { get; private set; }

        #endregion

        #region Data member

        #endregion
    }
}