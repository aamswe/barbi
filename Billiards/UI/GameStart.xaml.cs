using Billiards.Models;
using Billiards.Shared;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Controls;

namespace Billiards.UI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GameStart : Page
    {
        ObservableCollection<Player> availablePlayers { get; set; }
        ObservableCollection<Player> selectedPlayers { get; set; }

        public GameStart()
        {
            this.InitializeComponent();
            this.availablePlayers = new ObservableCollection<Player>(new UserManager().PlayerList().AsEnumerable());
            this.selectedPlayers = new ObservableCollection<Player>();
        }


        /// <summary>
        /// Adds a list to the list of selected players and removes it from the available players
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void availablePlayerButtonClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Button castButton = (Button)sender;
            string playerName = castButton.Content.ToString();

            //get the player object
            if (this.availablePlayers.Any(p => p.Name == playerName))
            {
                Player selectedPlayer = this.availablePlayers.FirstOrDefault(p => p.Name == playerName);
                selectedPlayers.Add(selectedPlayer);
                availablePlayers.Remove(selectedPlayer);
            }
        }


        /// <summary>
        /// Removes a player from the list of selected players and adds it to the list of available players
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void removePlayerButtonClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Button castButton = (Button)sender;
            string playerName = castButton.Content.ToString();
            if (this.selectedPlayers.Any(p => p.Name == playerName))
            {
                Player selectedPlayer = this.selectedPlayers.FirstOrDefault(p => p.Name == playerName);
                selectedPlayers.Remove(selectedPlayer);
                availablePlayers.Add(selectedPlayer);
            }
        }


        /// <summary>
        /// Handles the shuffling of the selected playuers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void shuffleSelectedPlayers(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            List<Player> playerList = this.selectedPlayers.ToList<Player>();
            Shuffler.Shuffle(playerList, new System.Random());

            this.selectedPlayers.Clear();
            
            foreach(Player p in playerList)
            {
                this.selectedPlayers.Add(p);
            }
        }


        private void startGameButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //Build the gamestate for passing to the GamePage
            GameState gameState = new GameState();
            gameState.players = new List<PlayerGameContainer>();

            string strGameLength = this.gameLength.Text;

            int iGameLength;
            if (int.TryParse(strGameLength, out iGameLength))
            {
                if (iGameLength > 0)
                {
                    gameState.TimeRemaining = iGameLength * 60;
                }else
                {
                    gameState.TimeRemaining = 20 * 60;
                }
            }

            //add the selected players
            DbContext db = new DbContext();
            foreach (Player player in this.selectedPlayers)
            {
                PlayerGameContainer pgc = new PlayerGameContainer() { Player = player, Fouls = 0, Plays = 0, Score = 0, TotalPointsLost = 0 };
                gameState.players.Add(pgc);
            }

            // we are all setup, load the game in progress screen


            this.Frame.Navigate(typeof(GamePage), gameState);
        }
    }
}
