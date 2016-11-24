using Billiards.Models;
using Billiards.Shared;
using Billiards.UI;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Billiards
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void manageUsersButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ManageUsers));
        }

        private void SeedButton_Click(object sender, RoutedEventArgs e)
        {
            //Create the tables
            DbContext db = new DbContext();

            var conn = DbContext.DbConnection;
            var c = DbContext.DbConnection.CreateTable<Player>();
            Player tim = new Player() { Id = 1, Name = "Tim" };
            Player andreas = new Player() { Id = 2, Name = "Andreas" };
            Player ian = new Player() { Id = 3, Name = "Ian" };
            Player david = new Player() { Id = 4, Name = "David" };
            Player tom = new Player() { Id = 5, Name = "Tom" };
            Player jess = new Player() { Id = 6, Name = "Jess" };
            Player matt = new Player() { Id = 7, Name = "Matt" };

            conn.InsertOrReplace(tim);
            conn.InsertOrReplace(andreas);
            conn.InsertOrReplace(ian);
            conn.InsertOrReplace(david);
            conn.InsertOrReplace(tom);
            conn.InsertOrReplace(jess);
            conn.InsertOrReplace(matt);

            DbContext.DbConnection.CreateTable<Games>();
            DbContext.DbConnection.CreateTable<GameScore>();

        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GameStart));
        }
    }
}
