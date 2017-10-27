using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FitbitAnalysis_Phillip_Morris.Model;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace FitbitAnalysis_Phillip_Morris.View
{
    public sealed partial class EntryDialog : ContentDialog
    {
        #region Data member

        #endregion

        public EntryDialog()
        {
            InitializeComponent();
        }

        #region Property

        public FitbitEntry FitbitEntry { get; private set; }

        #endregion

        private async void addEntry_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var entryDate = date.Date.Date;
                var entrySteps = Int32.Parse(steps.Text);
                var entryDistance = Double.Parse(distance.Text);
                var entryCaloriesBurned = Int32.Parse(caloriesBurned.Text);
                var entryFloors = Int32.Parse(floors.Text);
                var entryActivityCalories = Int32.Parse(activityCalories.Text);

                FitbitEntry = new FitbitEntry(entryDate, entrySteps, entryDistance, entryCaloriesBurned, entryFloors,
                    entryActivityCalories);
                entryDialog.Hide();
            }
            catch (Exception exception)
            {
                var invalidDialog = new ContentDialog();
                invalidDialog.Content = "Invalid input";
                invalidDialog.CloseButtonText = "Ok";
                Hide();
                await invalidDialog.ShowAsync();
                await OpenDialog();
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