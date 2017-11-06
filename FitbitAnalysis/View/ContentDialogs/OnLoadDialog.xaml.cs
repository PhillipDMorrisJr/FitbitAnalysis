using Windows.UI.Xaml;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FitbitAnalysis_Phillip_Morris.View
{
    public sealed partial class OnLoadDialog
    {
        #region Properties

        public bool Merge { get; private set; }
        public bool MergeAll { get; private set; }
        public bool Replace { get; private set; }
        public bool Cancel { get; private set; }

        #endregion

        #region Constructors

        public OnLoadDialog()
        {
            this.InitializeComponent();
            this.Merge = false;
            this.Replace = false;
            this.Cancel = false;
        }

        #endregion

        #region Methods

        private void mergeButton_OnClick(object sender, RoutedEventArgs args)
        {
            this.Merge = true;
            this.loadDialog.Hide();
        }

        private void mergeAllButton_OnClick(object sender, RoutedEventArgs args)
        {
            this.MergeAll = true;
            this.loadDialog.Hide();
        }

        private void replaceButton_OnClick(object sender, RoutedEventArgs args)
        {
            this.Replace = true;
            this.loadDialog.Hide();
        }

        private void cancelButton_OnClick(object sender, RoutedEventArgs args)
        {
            this.Cancel = true;
            this.loadDialog.Hide();
        }

        #endregion
    }
}