using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;

// This User Control item template was found at: https://stackoverflow.com/questions/37738128/custom-content-dialog-in-uwp-with-3-buttons

namespace FitbitAnalysis_Phillip_Morris.View
{
    /// <summary>
    ///     Creates a custom content dialog
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.ContentDialog" />
    public sealed partial class CustomContentDialog
    {
        #region Types and Delegates

        /// <summary>
        ///     Result of button selected
        /// </summary>
        public enum MyResult
        {
            Nothing,
            Yes,
            No
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

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CustomContentDialog" /> class.
        /// </summary>
        /// <param name="prompt">The prompt.</param>
        public CustomContentDialog(string prompt)
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

        /// <summary>
        ///     Handles the Click event of the yesButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void yesButton_Click(object sender, RoutedEventArgs e)
        {
            this.Result = MyResult.Yes;
            this.dialog.Hide();
        }

        /// <summary>
        ///     Handles the Click event of the noButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void noButton_Click(object sender, RoutedEventArgs e)
        {
            this.Result = MyResult.No;
            // Close the dialog
            this.dialog.Hide();
        }

        #endregion
    }
}