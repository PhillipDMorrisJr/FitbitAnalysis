using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace FitbitAnalysis_Phillip_Morris.Utility.IO
{
    public static class FileSelector
    {
        public static async Task<StorageFile> PickFile()
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
    }
}
