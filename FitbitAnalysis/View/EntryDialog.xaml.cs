using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FitbitAnalysis_Phillip_Morris.Model;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace FitbitAnalysis_Phillip_Morris.View
{
    public sealed partial class EntryDialog : ContentDialog
    {
        #region Property
        public FitbitEntry FitbitEntry => this.fitbitEntry;
        #endregion
        #region Data member
        private FitbitEntry fitbitEntry;
        #endregion
        public EntryDialog()
        {
            this.InitializeComponent();
        }

        private async void addEntry_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
            var entryDate = this.date.Date.Date;
            var entrySteps = Int32.Parse(this.steps.Text);
            var entryDistance =  Double.Parse(this.distance.Text);
            var entryCaloriesBurned = Int32.Parse(this.caloriesBurned.Text);
            var entryFloors = Int32.Parse(this.floors.Text);
            var entryActivityCalories = Int32.Parse(this.activityCalories.Text);

             this.fitbitEntry = new FitbitEntry(entryDate, entrySteps, entryDistance, entryCaloriesBurned, entryFloors, entryActivityCalories);
            this.entryDialog.Hide();
            }
            catch (Exception exception)
            {
                ContentDialog invalidDialog = new ContentDialog();
                invalidDialog.Content = "Invalid input";
                invalidDialog.CloseButtonText = "Ok";
                this.Hide();
                await invalidDialog.ShowAsync();
                await this.OpenDialog();
            }
            

        }

        /// <summary>
        ///     Opens the dialog.
        /// </summary>
        public async Task OpenDialog()
        {
            await ShowAsync();
        }
    }
}
