using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FitbitAnalysis_Phillip_Morris.Model;
using FitbitAnalysis_Phillip_Morris.View.Report;

namespace FitbitAnalysis_Phillip_Morris.View
{
    /// <summary>
    ///     An main page that will allow the user to load a CSV file of Fitbit data.
    /// </summary>
    public sealed partial class MainPage
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainPage" /> class.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            fitbitJournal = new FitbitJournal();
            ApplicationView.PreferredLaunchViewSize = new Size {Width = ApplicationWidth, Height = ApplicationHeight};
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(ApplicationWidth, ApplicationHeight));
        }

        #endregion

        #region Data members

        /// <summary>
        ///     The application height
        /// </summary>
        public const int ApplicationHeight = 500;

        /// <summary>
        ///     The application width
        /// </summary>
        public const int ApplicationWidth = 800;

        private static List<DateTime> duplicatedDatesNotToAdd;
        private static List<FitbitEntry> entriesToReplace;
        private readonly FitbitJournal fitbitJournal;
        private FitbitJournalOutput fitbitFitbitJournalOutput;
        private bool replaceAllBoxChecked;
        private bool mergeAllBoxChecked;
        private bool skipAll;

        #endregion

        #region Methods

        /// <summary>
        ///     Handles the Click event of the loadButton control.
        ///     Message Dialog retrieved from
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Windows.UI.Xaml.RoutedEventArgs" /> instance containing the event data.</param>
        private async void loadButton_Click(object sender, RoutedEventArgs e)
        {
            skipAll = false;
            entriesToReplace = new List<FitbitEntry>();
            duplicatedDatesNotToAdd = new List<DateTime>();

            var file = await pickFile();
            await parseFile(file);

            replaceEntries();

            await generateHistogram();
        }

        private static async Task handleWhenEitherInputsAreNotParsed(bool thresholdParsed, bool amountOfCategoryParsed)
        {
            if (!thresholdParsed || !amountOfCategoryParsed)
            {
                var invalidInpuDialog = new ContentDialog();
                invalidInpuDialog.Content = "All input must be numbers";
                await invalidInpuDialog.ShowAsync();
            }
        }

        private async Task generateHistogram()
        {
            if (fitbitJournal.IsEmpty())
            {
                outputTextBox.Text = "No Data";
            }
            else
            {
                bool thresholdParsed;
                var thresholdResult = handleWhenThresholdTextParsed(out thresholdParsed);

                bool amountOfCategoriesParsed;
                var amountOfCategoriesResult = handleAmountOfCategoriesParsed(out amountOfCategoriesParsed);

                await handleWhenEitherInputsAreNotParsed(thresholdParsed, amountOfCategoriesParsed);

                fitbitFitbitJournalOutput = new FitbitJournalOutput(fitbitJournal);
                outputTextBox.Text =
                    fitbitFitbitJournalOutput.GetOutput(thresholdResult, amountOfCategoriesResult);
            }
        }

        private int handleAmountOfCategoriesParsed(out bool amountOfCategoriesParsed)
        {
            var amountOfCategoriesText = amountOfCategories.Text;
            int amountOfCategoriesResult;
            amountOfCategoriesParsed = int.TryParse(amountOfCategoriesText, out amountOfCategoriesResult);
            if (amountOfCategoriesParsed)
                amountOfCategories.Text = amountOfCategoriesResult.ToString();
            return amountOfCategoriesResult;
        }

        private int handleWhenThresholdTextParsed(out bool thresholdParsed)
        {
            var thresholdText = threshold.Text;
            int thresholdResult;
            thresholdParsed = int.TryParse(thresholdText, out thresholdResult);
            if (thresholdParsed)
                threshold.Text = thresholdResult.ToString();
            return thresholdResult;
        }

        private void replaceEntries()
        {
            foreach (var fitbitEntry in entriesToReplace)
                fitbitJournal.ReplaceMatchingDateEntries(fitbitEntry);
        }

        private static async Task<StorageFile> pickFile()
        {
            var fileChooser = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };
            fileChooser.FileTypeFilter.Add(".csv");
            fileChooser.FileTypeFilter.Add(".txt");

            var file = await fileChooser.PickSingleFileAsync();
            return file;
        }

        /// <summary>
        ///     Parses the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        private async Task parseFile(StorageFile file)
        {
            if (file == null)
                throw new ArgumentException("File Must not be null.");
            char[] seperator = {','};
            var stream = await file.OpenStreamForReadAsync();

            using (var reader = new StreamReader(stream))
            {
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var input = line.Split(seperator);

                    var date = DateTime.Parse(input[0]);
                    var caloriesBurned = int.Parse(input[1]);
                    var steps = int.Parse(input[2]);
                    var distance = double.Parse(input[3]);
                    var activityCalories = int.Parse(input[5]);
                    var floors = int.Parse(input[4]);
                    var fitbitEntry = new FitbitEntry(date, steps, distance, caloriesBurned, activityCalories,
                        floors);
                    await manageAndAddFitbitEntry(fitbitEntry, date);
                }
            }
        }

        private async Task manageAndAddFitbitEntry(FitbitEntry fitbitEntry, DateTime date)
        {
            if (mergeAllBoxChecked)
            {
                fitbitJournal.AddEntry(fitbitEntry);
            }
            else if (skipAll)
            {
            }
            else
            {
                await handleWhenFitbitContainsDate(date, fitbitEntry);
            }
        }

        private async Task handleWhenFitbitContainsDate(DateTime date, FitbitEntry fitbitEntry)
        {
            if (fitbitJournal.ContainsDate(date))
            {
                await handleWhenThereAreDuplicateDates(fitbitEntry);
                handleWhenReplacing(fitbitEntry);
            }
            else
            {
                fitbitJournal.AddEntry(fitbitEntry);
            }
        }

        private void handleWhenReplacing(FitbitEntry fitbitEntry)
        {
            if (entriesToReplace.Contains(fitbitEntry))
                fitbitJournal.ReplaceMatchingDateEntries(fitbitEntry);
        }

        private void addToFitbitCollection(FitbitEntry fitbitEntry)
        {
            if (!duplicatedDatesNotToAdd.Contains(fitbitEntry.Date))
                fitbitJournal.AddEntry(fitbitEntry);
        }

        private async Task handleWhenThereAreDuplicateDates(FitbitEntry fitbitEntry)
        {
            var duplicateEntryDialog = new CustomContentDialog("There are other entries on " + fitbitEntry.Date);
            await duplicateEntryDialog.OpenDialog();

            switch (duplicateEntryDialog.Result)
            {
                case CustomContentDialog.MyResult.Skip:
                    break;
                case CustomContentDialog.MyResult.Replace:
                    entriesToReplace.Add(fitbitEntry);
                    break;
                case CustomContentDialog.MyResult.SkipAll:
                    skipAll = true;
                    break;
                case CustomContentDialog.MyResult.Merge:
                    addToFitbitCollection(fitbitEntry);
                    break;
            }
        }

        private async void updateButton_OnClickButton_Click(object sender, RoutedEventArgs e)
        {
            await generateHistogram();
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            fitbitJournal.ClearEntries();
        }

        private async void saveButton_Click(object sender, RoutedEventArgs e)
        {
            var fitbitFileOutput = "Date,Calories Burned,Steps,Distance,Floors,Activity Calories" + Environment.NewLine;
            foreach (var entry in fitbitJournal.Entries)
                fitbitFileOutput += entry.Date + "," + entry.CaloriesBurned + "," + entry.Steps + "," + entry.Distance +
                                    "," + entry.Floors + "," + entry.ActivityCalories + Environment.NewLine;

            var storageFolder =
                ApplicationData.Current.LocalFolder;
            var fitbitSaveFile =
                await storageFolder.CreateFileAsync("fitbitSavedData.csv",
                    CreationCollisionOption.ReplaceExisting);

            await FileIO.WriteTextAsync(fitbitSaveFile, fitbitFileOutput);
        }

        private void replaceAllBox_OnChecked(object sender, RoutedEventArgs e)
        {
            var isChecked = replaceAllBox.IsChecked;
            if (isChecked != null)
                replaceAllBoxChecked = isChecked.Value;
            if (replaceAllBoxChecked)
                setEnable(mergeAllBox, false);
        }

        private void mergeAllBox_OnCheckedAllBox_OnChecked(object sender, RoutedEventArgs e)
        {
            var isChecked = mergeAllBox.IsChecked;
            if (isChecked != null)
                mergeAllBoxChecked = isChecked.Value;
            if (mergeAllBoxChecked)
                setEnable(replaceAllBox, false);
        }

        private void replaceAllBox_OnUnchecked(object sender, RoutedEventArgs e)
        {
            setEnable(mergeAllBox, true);
        }

        private void mergeAllBox_OnUnchecked(object sender, RoutedEventArgs e)
        {
            setEnable(replaceAllBox, true);
        }

        private void setEnable(CheckBox checkBox, bool isEnabled)
        {
            checkBox.IsEnabled = isEnabled;
        }

        private async void addEntry_OnClick(object sender, RoutedEventArgs e)
        {
            var entryDialog = new EntryDialog();
            await entryDialog.OpenDialog();
            var entry = entryDialog.FitbitEntry;
            await this.manageAndAddFitbitEntry(entry, entry.Date);
        }

        #endregion
    }
}