using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FitbitAnalysis_Phillip_Morris.Annotations;
using FitbitAnalysis_Phillip_Morris.Model;

namespace FitbitAnalysis_Phillip_Morris.ViewModel
{
    /// <summary>
    ///     Updates values changed between the view and model.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class FitbitJournalViewModel : INotifyPropertyChanged
    {
        #region Data members

        private FitbitJournal fitbitJournal;

        private ObservableCollection<FitbitEntry> entries;

        private FitbitEntry selectedEntry;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the entries.
        /// </summary>
        /// <value>
        ///     The entries.
        /// </value>
        public ObservableCollection<FitbitEntry> Entries
        {
            get => this.entries;
            set
            {
                this.entries = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the selected entry.
        /// </summary>
        /// <value>
        ///     The selected entry.
        /// </value>
        public FitbitEntry SelectedEntry
        {
            get => this.selectedEntry;
            set
            {
                this.selectedEntry = value;
                this.OnPropertyChanged();
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="FitbitJournalViewModel" /> class.
        /// </summary>
        public FitbitJournalViewModel()
        {
            this.fitbitJournal = new FitbitJournal();
        }

        #endregion

        #region Methods

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}