using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FitbitAnalysis_Phillip_Morris.View
{
    public sealed partial class OnLoadDialog : ContentDialog
    {
        public bool Merge { get; private set; }
        public bool Replace { get; private set; }
        public bool Cancel { get; private set; }

        public OnLoadDialog()
        {
            this.InitializeComponent();
            this.Merge = false;
            this.Replace = false;
            this.Cancel = false;
        }

        private void mergeButton_OnClick(object sender, RoutedEventArgs args)
        {
            this.Merge = true;
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

    }
}
