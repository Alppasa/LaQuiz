using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace LaQuiz.UWP
{
    public static class CopyDatabase
    {
        public static async void CopyDatabaseAsync()
        {
            var dbFile = await
           ApplicationData.Current.LocalFolder.TryGetItemAsync("german.db") as StorageFile;
            if (null == dbFile)
            {
                // first time ... copy the .db file from assets to local folder
                var localFolder = ApplicationData.Current.LocalFolder;
                var originalDbFileUri = new Uri("ms-appx:///Assets/german.db");
                var originalDbFile = await
               StorageFile.GetFileFromApplicationUriAsync(originalDbFileUri);
                if (null != originalDbFile)
                {
                    dbFile = await originalDbFile.CopyAsync(localFolder, "german.db",
                   NameCollisionOption.ReplaceExisting);
                }
            }
        }
    }
}
