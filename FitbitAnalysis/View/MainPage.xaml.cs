using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FitbitAnalysis_Phillip_Morris.Model;
using FitbitAnalysis_Phillip_Morris.Report;
using FitbitAnalysis_Phillip_Morris.Utility.IO;
using FitbitAnalysis_Phillip_Morris.View.ContentDialogs;

namespace FitbitAnalysis_Phillip_Morris.View
{
    /// <summary>
    ///     An main page that will allow the user to load a CSV file of Fitbit data.
    /// </summary>
    public sealed partial class MainPage
    {
        #region Data members

        /// <summary>
        ///     The application height
        /// </summary>
        public const int ApplicationHeight = 1000;

        /// <summary>
        ///     The application width
        /// </summary>
        public const int ApplicationWidth = 1200;

        private List<DateTime> duplicatedDatesNotToAdd;
        private List<FitbitEntry> entriesToReplace;
        private FitbitJournal fitbitJournal;
        private FitbitJournalOutput fitbitFitbitJournalOutput;
        private bool replaceAll;
        private bool mergeAll;
        private bool skipAll;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainPage" /> class.
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();
            this.fitbitJournal = new FitbitJournal();
            ApplicationView.PreferredLaunchViewSize = new Size {Width = ApplicationWidth, Height = ApplicationHeight};
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(ApplicationWidth, ApplicationHeight));
        }

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
            this.skipAll = false;
            this.entriesToReplace = new List<FitbitEntry>();
            this.duplicatedDatesNotToAdd = new List<DateTime>();

            var file = await pickFile();
            if (!this.fitbitJournal.IsEmpty())
            {
                var loadDialog = new OnLoadDialog();
                await loadDialog.ShowAsync();

                this.mergeAll = loadDialog.MergeAll;
                this.replaceAll = loadDialog.Replace;
                if (loadDialog.Cancel)
                {
                    return;
                }
                if (this.replaceAll)
                {
                    this.clearButton_Click(sender, e);
                }

            }
            await this.parseFile(file);

            this.replaceEntries();

            await this.generateHistogram();
        }

        private static async Task handleWhenEitherInputsAreNotParsed(bool thresholdParsed, bool amountOfCategoryParsed,
            bool binSizeParsed)
        {
            if (!thresholdParsed || !amountOfCategoryParsed || !binSizeParsed)
            {
                var invalidInpuDialog = new ContentDialog {
                    Content = "All input must be numbers"
                };
                await invalidInpuDialog.ShowAsync();
            }
        }

        private async Task generateHistogram()
        {
            if (this.fitbitJournal.IsEmpty())
            {
                this.outputTextBox.Text = "No Data";
            }
            else
            {
                bool thresholdParsed;
                bool binSizeParsed;
                bool amountOfCategoriesParsed;

                var thresholdResult = this.parseTextBoxTextToInteger(out thresholdParsed, this.threshold);
                var amountOfCategoriesResult =
                    this.parseTextBoxTextToInteger(out amountOfCategoriesParsed, this.amountOfCategories);
                var binSizeResult = this.parseTextBoxTextToInteger(out binSizeParsed, this.binSize);

                await handleWhenEitherInputsAreNotParsed(thresholdParsed, amountOfCategoriesParsed, binSizeParsed);

                this.fitbitFitbitJournalOutput = new FitbitJournalOutput(this.fitbitJournal);
                this.outputTextBox.Text =
                    this.fitbitFitbitJournalOutput.GetOutput(thresholdResult, amountOfCategoriesResult, binSizeResult);
            }
        }

        private int parseTextBoxTextToInteger(out bool isParsed, TextBox textBox)
        {
            var text = textBox.Text;
            int result;
            isParsed = int.TryParse(text, out result);
            if (isParsed)
            {
                textBox.Text = Convert.ToString(result);
            }
            return result;
        }

        private void replaceEntries()
        {
            foreach (var fitbitEntry in this.entriesToReplace)
            {
                this.fitbitJournal.ReplaceMatchingDateEntries(fitbitEntry);
            }
        }

        private static async Task<StorageFile> pickFile()
        {
            var fileChooser = new FileOpenPicker {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };
            fileChooser.FileTypeFilter.Add(".csv");
            fileChooser.FileTypeFilter.Add(".txt");
            fileChooser.FileTypeFilter.Add(".xml");

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
            {
                return;
            }
            char[] seperator = {','};
            var stream = await file.OpenStreamForReadAsync();
            switch (file.FileType)
            {
                case ".csv":
                    try
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            while (!reader.EndOfStream)
                            {
                                var line = reader.ReadLine();
                                var input = line.Split(seperator);
                                var fitbitEntry = Parser.ParseCsv(input);

                                await this.manageAndAddFitbitEntry(fitbitEntry, fitbitEntry.Date);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        await informTheUserOfIssue();
                    }
                    break;
                case ".txt":
                    try
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            while (!reader.EndOfStream)
                            {
                                var line = reader.ReadLine();
                                var input = line.Split(seperator);
                                var fitbitEntry = Parser.ParseCsv(input);

                                await this.manageAndAddFitbitEntry(fitbitEntry, fitbitEntry.Date);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        await informTheUserOfIssue();
                    }
                    break;
                case ".xml":
                    try
                    {
                        var serializer = new XmlSerializer(typeof(FitbitJournal));

                        using (var xmlStream = new StreamReader(stream))
                        {
                            var journal = serializer.Deserialize(xmlStream);

                            this.fitbitJournal = journal as FitbitJournal;
                        }
                    }
                    catch (Exception)
                    {
                        await informTheUserOfIssue();
                    }
                    break;
            }
        }

        private static async Task informTheUserOfIssue()
        {
            var tellAboutTroubles = new ContentDialog {
                Content = "Stumbled a bit while reading the file, but we're going to keep trying!",
                CloseButtonText = "Great"
            };
            await tellAboutTroubles.ShowAsync();
        }

        private async Task manageAndAddFitbitEntry(FitbitEntry fitbitEntry, DateTime date)
        {
            if (this.mergeAll)
            {
                this.fitbitJournal.AddEntry(fitbitEntry);
            }
            else if (!this.skipAll)
            {
                await this.handleWhenFitbitContainsDate(date, fitbitEntry);
            }
        }

        private async Task handleWhenFitbitContainsDate(DateTime date, FitbitEntry fitbitEntry)
        {
            if (this.fitbitJournal.ContainsDate(date))
            {
                await this.handleWhenThereAreDuplicateDates(fitbitEntry);
                this.handleWhenReplacing(fitbitEntry);
            }
            else
            {
                this.fitbitJournal.AddEntry(fitbitEntry);
            }
        }

        private void handleWhenReplacing(FitbitEntry fitbitEntry)
        {
            if (this.entriesToReplace.Contains(fitbitEntry))
            {
                this.fitbitJournal.ReplaceMatchingDateEntries(fitbitEntry);
            }
        }

        private void addToFitbitCollection(FitbitEntry fitbitEntry)
        {
            if (!this.duplicatedDatesNotToAdd.Contains(fitbitEntry.Date))
            {
                this.fitbitJournal.AddEntry(fitbitEntry);
            }
        }

        private async Task handleWhenThereAreDuplicateDates(FitbitEntry fitbitEntry)
        {
            if (!this.mergeAll)
            {
                var duplicateEntryDialog =
                    new DuplicateEntryFoundDialog("There are other entries on " + fitbitEntry.Date);
                await duplicateEntryDialog.OpenDialog();

                switch (duplicateEntryDialog.Result)
                {
                    case DuplicateEntryFoundDialog.MyResult.Skip:
                        break;
                    case DuplicateEntryFoundDialog.MyResult.Replace:
                        this.entriesToReplace.Add(fitbitEntry);
                        break;
                    case DuplicateEntryFoundDialog.MyResult.SkipAll:
                        this.skipAll = true;
                        break;
                    case DuplicateEntryFoundDialog.MyResult.Merge:
                        this.addToFitbitCollection(fitbitEntry);
                        break;
                    case DuplicateEntryFoundDialog.MyResult.MergeAll:
                        this.addToFitbitCollection(fitbitEntry);
                        this.mergeAll = true;
                        break;
                }
            }
        }

        private async void updateButton_OnClickButton_Click(object sender, RoutedEventArgs e)
        {
            await this.generateHistogram();
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            this.fitbitJournal.ClearEntries();
            this.updateButton_OnClickButton_Click(sender, e);
        }

        private async void saveButton_Click(object sender, RoutedEventArgs e)
        {
            var picker = new FileSavePicker();
            picker.FileTypeChoices.Add("file style", new[] {".csv", ".xml"});
            picker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            picker.SuggestedFileName = "fitbitData";

            var fitbitSaveFile = await picker.PickSaveFileAsync();
            switch (fitbitSaveFile.FileType)
            {
                case ".csv":
                    var fitbitFileOutput = this.getCsvFitbitFileOutput();
                    await FileIO.WriteTextAsync(fitbitSaveFile, fitbitFileOutput);
                    break;
                case ".xml":
                    await this.serializeFile(fitbitSaveFile);
                    break;
            }
        }

        private async Task serializeFile(StorageFile fitbitSaveFile)
        {
            var serializer = new XmlSerializer(typeof(FitbitJournal));

            var stream = await fitbitSaveFile.OpenStreamForWriteAsync();

            using (stream)
            {
                serializer.Serialize(stream, this.fitbitJournal);
                stream.Position = 0;
            }
        }

        private string getCsvFitbitFileOutput()
        {
            var fitbitFileOutput = "Date,Calories Burned,Steps,Distance,Floors,Activity Calories,Active Minutes" +
                                   Environment.NewLine;
            foreach (var entry in this.fitbitJournal.Entries)
            {
                fitbitFileOutput = getFitbitEntryOutput(fitbitFileOutput, entry);
            }
            return fitbitFileOutput;
        }

        private static string getFitbitEntryOutput(string fitbitFileOutput, FitbitEntry entry)
        {
            var currentDate = entry.Date.ToString("d", CultureInfo.CurrentCulture);
            fitbitFileOutput += currentDate + ",";
            fitbitFileOutput += entry.CaloriesBurned + ",";
            fitbitFileOutput += entry.Steps + ",";
            fitbitFileOutput += entry.Distance + ",";
            fitbitFileOutput += entry.Floors + ",";
            fitbitFileOutput += entry.ActiveMinutes + ",";
            fitbitFileOutput += entry.ActivityCalories + Environment.NewLine;
            return fitbitFileOutput;
        }

        private async void addEntry_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var entryDialog = new AddEntryDialog();
                await entryDialog.ShowAsync();
                var entry = entryDialog.FitbitEntry;
                if (entry == null)
                {
                    if (!entryDialog.IsGoodFormat)
                    {
                        await informEntryIsBadFormat();
                    }
                    else
                    {
                        await informEntryNotAdded();
                    }
                    return;
                }
                await this.manageAndAddFitbitEntry(entry, entry.Date);
                this.updateButton_OnClickButton_Click(sender, e);
            }
            catch (FormatException)
            {
            }
        }

        private static async Task informEntryNotAdded()
        {
            var entryNotAddeddialog = new ContentDialog {
                Content = "Entry not added",
                CloseButtonText = "Okay"
            };
            await entryNotAddeddialog.ShowAsync();
        }

        private static async Task informEntryIsBadFormat()
        {
            var formatDialog = new ContentDialog {
                Content =
                    "The distance must be positive and the steps, calories, active minutes and floors must be positive integers. The entry's date can not come before the current date.",
                CloseButtonText = "Okay"
            };
            await formatDialog.ShowAsync();
        }

        #endregion
    }
}