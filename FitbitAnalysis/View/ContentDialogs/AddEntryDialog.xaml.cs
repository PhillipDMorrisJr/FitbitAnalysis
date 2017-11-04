using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FitbitAnalysis_Phillip_Morris.Model;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace FitbitAnalysis_Phillip_Morris.View
{
    public sealed partial class AddEntryDialog
    {
        #region Properties

        #region Property

        public FitbitEntry FitbitEntry { get; private set; }

        #endregion

        #endregion

        #region Constructors

        public AddEntryDialog()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Methods

        private async void addEntry_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var entryDate = this.date.Date.Date;
                var entrySteps = int.Parse(this.steps.Text);
                var entryDistance = double.Parse(this.distance.Text);
                var entryCaloriesBurned = int.Parse(this.caloriesBurned.Text);
                var entryFloors = int.Parse(this.floors.Text);
                var entryActivityCalories = int.Parse(this.activityCalories.Text);

                this.FitbitEntry = new FitbitEntry(entryDate, entrySteps, entryDistance, entryCaloriesBurned,
                    entryFloors,
                    entryActivityCalories);
                this.entryDialog.Hide();
            }
            catch (Exception)
            {
                var invalidDialog = new ContentDialog {
                    Content = "Invalid input",
                    CloseButtonText = "Ok"
                };
                Hide();
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

        #endregion

        #region Data member

        #endregion
    }
}