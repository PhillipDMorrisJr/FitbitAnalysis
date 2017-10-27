using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// This User Control item template was found at: https://stackoverflow.com/questions/37738128/custom-content-dialog-in-uwp-with-3-buttons

namespace FitbitAnalysis_Phillip_Morris.View
{
    /// <summary>
    ///     Creates a custom content dialog
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.ContentDialog" />
    public sealed partial class CustomContentDialog : ContentDialog
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
            Merge
        }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CustomContentDialog" /> class.
        /// </summary>
        /// <param name="prompt">The prompt.</param>
        public CustomContentDialog(string prompt)
        {
            InitializeComponent();
            Result = MyResult.Nothing;
            this.prompt.Text = prompt;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the result.
        /// </summary>
        /// <value>
        ///     The result.
        /// </value>
        public MyResult Result { get; set; }

        /// <summary>
        ///     The application height
        /// </summary>
        public const int ApplicationHeight = 300;

        /// <summary>
        ///     The application width
        /// </summary>
        public const int ApplicationWidth = 300;

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
            Result = MyResult.Merge;
            dialog.Hide();
        }

        private void skipButton_Click(object sender, RoutedEventArgs e)
        {
            Result = MyResult.Skip;
            dialog.Hide();
        }

        private void skipAllButton_Click(object sender, RoutedEventArgs e)
        {
            Result = MyResult.SkipAll;
            dialog.Hide();
        }

        private void replaceButton_Click(object sender, RoutedEventArgs e)
        {
            Result = MyResult.Replace;
            dialog.Hide();
        }

        #endregion
    }
}