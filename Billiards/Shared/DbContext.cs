using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Billiards.Shared
{
    class DbContext
    {
        public static SQLiteConnection DbConnection
        {
            get
            {
                return new SQLiteConnection(
                     new SQLitePlatformWinRT(),
                     Path.Combine(ApplicationData.Current.LocalFolder.Path, "billiards.sqlite"));
            }

        }
    }
}
