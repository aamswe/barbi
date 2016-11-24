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

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Billiards.UI
{
    public sealed partial class ScoreUpdateDialog : ContentDialog
    {

        public string EnteredScore = "";

        public ScoreUpdateDialog()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;

            if (this.score.Text == "0")
                this.score.Text = "";

            switch(clickedButton.Name)
            {
                case "one":
                    this.score.Text += "1";
                    break;
                case "two":
                    this.score.Text += "2";
                    break;
                case "three":
                    this.score.Text += "3";
                    break;
                case "four":
                    this.score.Text += "4";
                    break;
                case "five":
                    this.score.Text += "5";
                    break;
                case "six":
                    this.score.Text += "6";
                    break;
                case "seven":
                    this.score.Text += "7";
                    break;
                case "eight":
                    this.score.Text += "8";
                    break;
                case "nine":
                    this.score.Text += "9";
                    break;
                case "zero":
                    this.score.Text += "0";
                    break;
            }

            this.EnteredScore = this.score.Text;
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            if (this.score.Text.Length >= 0)
            {
                this.score.Text = this.score.Text.Substring(0, this.score.Text.Length - 1);
            }else
            {
                this.score.Text = "0";
            }

            this.EnteredScore = this.score.Text;

        }
    }
}
