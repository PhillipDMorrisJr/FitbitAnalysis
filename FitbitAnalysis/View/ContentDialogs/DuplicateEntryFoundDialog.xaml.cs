using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;

// This User Control item template was found at: https://stackoverflow.com/questions/37738128/custom-content-dialog-in-uwp-with-3-buttons

namespace FitbitAnalysis_Phillip_Morris.View
{
    /// <summary>
    ///     Creates a custom content dialog
    ///     ****create a merge all button****
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.ContentDialog" />
    public sealed partial class DuplicateEntryFoundDialog
    {
        #region Types and Delegates

        /// <summary>
        ///     Result of button selected
        /// </summary>
        public enum MyResult
        {
            Nothing,
            Skip,
            SkipAll,
            Replace,
            Merge,
            MergeAll
        }

        #endregion

        #region Data members

        /// <summary>
        ///     The application height
        /// </summary>
        public const int ApplicationHeight = 300;

        /// <summary>
        ///     The application width
        /// </summary>
        public const int ApplicationWidth = 300;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the result.
        /// </summary>
        /// <value>
        ///     The result.
        /// </value>
        public MyResult Result { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DuplicateEntryFoundDialog" /> class.
        /// </summary>
        /// <param name="prompt">The prompt.</param>
        public DuplicateEntryFoundDialog(string prompt)
        {
            this.InitializeComponent();
            this.Result = MyResult.Nothing;
            this.prompt.Text = prompt;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Opens the dialog.
        /// </summary>
        public async Task OpenDialog()
        {
            await ShowAsync();
        }

        private void mergeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Result = MyResult.Merge;
            this.dialog.Hide();
        }

        private void skipButton_Click(object sender, RoutedEventArgs e)
        {
            this.Result = MyResult.Skip;
            this.dialog.Hide();
        }

        private void skipAllButton_Click(object sender, RoutedEventArgs e)
        {
            this.Result = MyResult.SkipAll;
            this.dialog.Hide();
        }

        private void replaceButton_Click(object sender, RoutedEventArgs e)
        {
            this.Result = MyResult.Replace;
            this.dialog.Hide();
        }

        private void mergeAllButton_Click(object sender, RoutedEventArgs e)
        {
            this.Result = MyResult.MergeAll;
            this.dialog.Hide();
        }

        #endregion
    }
}