using Billiards.Models;
using Billiards.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Billiards.UI
{
    public sealed partial class GamePage : Page
    {
        GameState _gameState { get; set; }
        ObservableCollection<PlayerGameContainer> players { get; set; }
        ObservableCollection<string> rankings { get; set; }
        bool gameStarted = false;
        DispatcherTimer timer;

        public GamePage()
        {
            this.InitializeComponent();
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this._gameState = (GameState)e.Parameter;

            players = new ObservableCollection<PlayerGameContainer>(_gameState.players.AsEnumerable());
            rankings = new ObservableCollection<string>();
            CalculateRankings();

            //setup the timer
            timer = new DispatcherTimer();
            timer.Tick += timer_tick;
            timer.Interval = new TimeSpan(0, 0, 1);
        }


        void timer_tick(object sender, object e)
        {
            if (this._gameState.TimeRemaining > 0)
            {
                this._gameState.TimeRemaining--;
            }

            TimeSpan span = TimeSpan.FromSeconds(this._gameState.TimeRemaining);
            this.countdownText.Text = string.Format("{0}:{1:00}", (int)span.TotalMinutes, span.Seconds);


            if (this._gameState.TimeRemaining <= 0)
            {
                this.abandonGameButton.Visibility = Visibility.Collapsed;
                this.storeGameButton.Visibility = Visibility.Visible;
                this.dontStoreGameButton.Visibility = Visibility.Visible;
            }
        }


        private async void Player_Click(object sender, RoutedEventArgs e)
        {
            ScoreUpdateDialog dialog = new ScoreUpdateDialog();
            await dialog.ShowAsync();

            if (dialog.EnteredScore != "" && dialog.EnteredScore != "0")
            {
                Button clickedButton = ((Button)sender);

                //find the user which has been clicked on
                if (this.players.Any(p => p.Player.Name == clickedButton.Content.ToString()))
                {
                    PlayerGameContainer pgc = this.players.FirstOrDefault(p => p.Player.Name == clickedButton.Content.ToString());

                    int parsedEnteredNumber = 0;

                    try
                    {
                        int.TryParse(dialog.EnteredScore, out parsedEnteredNumber);
                    }
                    catch (Exception) { }


                    if (parsedEnteredNumber != 0)
                    {
                        pgc.Score += parsedEnteredNumber;
                    }

                    CalculateRankings();
                }
            }
        }


        private void CalculateRankings()
        {
            List<PlayerGameContainer> pRanking = this.players.OrderBy(p => p.Score).Reverse().ToList();
 
            this.rankings.Clear();
            foreach(PlayerGameContainer pgc in pRanking)
            {
                this.rankings.Add(pgc.Player.Name + " - " + pgc.Score);
            }
        }


        private void beginGameButton_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan span = TimeSpan.FromSeconds(this._gameState.TimeRemaining);
            this.countdownText.Text = string.Format("{0}:{1:00}", (int)span.TotalMinutes, span.Seconds);


            this.beginGameButton.Visibility = Visibility.Collapsed;
            this.countdownText.Visibility = Visibility.Visible;
            this.timer.Start();
        }




        private void abandonGameButton_Click(object sender, RoutedEventArgs e)
        {

        }


        private void storeGameButton_Click(object sender, RoutedEventArgs e)
        {
            var conn = DbContext.DbConnection;

            // make an entry for this game
            Games game = new Games();

            conn.Insert(game);

            foreach (PlayerGameContainer player in this.players)
            {
                GameScore score = new GameScore();
                score.GameId = game.Id;
                score.Player = player.Player.Id;
                score.Score = player.Score;
                conn.Insert(score);
            }

            conn.Commit();

            this.Frame.Navigate(typeof(MainPage));

        }
    }
}
