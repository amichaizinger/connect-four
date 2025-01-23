using System;
using System.Data.Common;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using FourInARow.functions;
using static FourInARow.functions.GameLogic;
using static FourInARow.GameBoardBack;

namespace FourInARow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public GameBoardBack _gameBoard { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();
            _gameBoard = new GameBoardBack();
            UpdateStatus();
        }

        private void InitializeBoard()
        {
            // Filling the board with empty images

            for (int i = 0; i < 42; i++)  // 6 rows * 7 columns = 42 spaces
            {
                Border cellBorder = new Border(); 
                cellBorder.BorderBrush = Brushes.Black;
                cellBorder.BorderThickness = new Thickness(1);  
                cellBorder.Margin = new Thickness(1);  // Margin to make sure borders don't overlap

                Image empty = new Image
                {
                    Width = 60,
                    Height = 60,
                    Margin = new Thickness(5),
                    Stretch = Stretch.Uniform
                };
                // Add the image inside the border
                cellBorder.Child = empty;
                GameBoard.Children.Add(cellBorder);
            }
        }

        public void SetImage(int row, int column)
        {

            Image discImage = new Image();  
            BitmapImage discSource; 

            if (_gameBoard.CurrentPlayer == GameBoardBack.Player.Blue)
            {
                discSource = new BitmapImage(new Uri("C:\\Users\\sharo\\source\\repos\\FourInARow\\FourInARow\\Repository\\blue player.png"));
            }

            else
            {
                discSource = new BitmapImage(new Uri("C:\\Users\\sharo\\source\\repos\\FourInARow\\FourInARow\\Repository\\red player.png"));
            }
            discImage.Source = discSource;

            discImage.Width = 55; 
            discImage.Height = 55;
            discImage.Stretch = Stretch.Uniform;
            discImage.Margin = new Thickness(1); 

            Border cellBorder = new Border();
            if (WayWon == Won.noWin)
            {
                cellBorder.BorderBrush = Brushes.Black;
                cellBorder.BorderThickness = new Thickness(1);  
                cellBorder.Margin = new Thickness(1); 
            }
            else
            {
                cellBorder.BorderBrush = (_gameBoard.CurrentPlayer == Player.Red) ? Brushes.Red : Brushes.Blue;
                cellBorder.BorderThickness = new Thickness(3); 
                cellBorder.Margin = new Thickness(3); 
            }
            // Add the image inside the border
            cellBorder.Child = discImage;

            // Calculate the position in which we should put the disc
            int index = (row * GameBoard.Columns) + column;
            GameBoard.Children.RemoveAt(index);
            GameBoard.Children.Insert(index, cellBorder);
        }

        #region Buttons

        private void Button_Click(object sender, RoutedEventArgs e) => HandleButtonClick(0);
        private void Button_Click_1(object sender, RoutedEventArgs e) => HandleButtonClick(1);
        private void Button_Click_2(object sender, RoutedEventArgs e) => HandleButtonClick(2);
        private void Button_Click_3(object sender, RoutedEventArgs e) => HandleButtonClick(3);
        private void Button_Click_4(object sender, RoutedEventArgs e) => HandleButtonClick(4);
        private void Button_Click_5(object sender, RoutedEventArgs e) => HandleButtonClick(5);
        private void Button_Click_6(object sender, RoutedEventArgs e) => HandleButtonClick(6);

        private void HandleButtonClick(int column)
        {
            if (WayWon == Won.noWin) //checking that no one won
            {
                int discRow, discCol;
                if (!_gameBoard.DropDisc(column, out discRow, out discCol)) //no place in the column
                {
                    if (!_gameBoard.AllColumnsFull(_gameBoard.FullColumn))
                    {
                        StatusText.Text = $"This column is full, {_gameBoard.CurrentPlayer} try again";
                    }
                    else
                    {
                        UpdateStatus();
                    }
                }
                
                else // the column isnt full, put disc in the right place and check for a win
                {
                    SetImage(discRow, discCol);

                    int winningRow, winningCol;
                    if (GameLogic.CheckWin(_gameBoard, _gameBoard.CurrentPlayer, out winningRow, out winningCol))
                    {
                        if (GameLogic.WayWon == Won.horizontal)
                        {
                            SetImage(winningRow, winningCol);
                            SetImage(winningRow, winningCol + 1);
                            SetImage(winningRow, winningCol + 2);
                            SetImage(winningRow, winningCol + 3);
                            UpdateStatus();
                            ShowFireworks();
                            rematch.Visibility = Visibility.Visible;
                        }
                        if (WayWon == Won.vertical)
                        {
                            SetImage(winningRow, winningCol);
                            SetImage(winningRow + 1, winningCol);
                            SetImage(winningRow + 2, winningCol);
                            SetImage(winningRow + 3, winningCol);
                            UpdateStatus();
                            ShowFireworks();
                            rematch.Visibility = Visibility.Visible;
                        }
                        if (WayWon == Won.diagonalDownRight)
                        {
                            SetImage(winningRow, winningCol);
                            SetImage(winningRow + 1, winningCol + 1);
                            SetImage(winningRow + 2, winningCol + 2);
                            SetImage(winningRow + 3, winningCol + 3);
                            ShowFireworks();
                            UpdateStatus();
                            rematch.Visibility = Visibility.Visible;
                        }
                        if (WayWon == Won.diagonalUpRight)
                        {
                            SetImage(winningRow, winningCol);
                            SetImage(winningRow - 1, winningCol + 1);
                            SetImage(winningRow - 2, winningCol + 2);
                            SetImage(winningRow - 3, winningCol + 3);
                            UpdateStatus();
                            ShowFireworks();
                            rematch.Visibility = Visibility.Visible;
                        }
                        if (WayWon == Won.noWin)
                        {
                            StatusText.Foreground = (_gameBoard.CurrentPlayer == Player.Red) ? Brushes.Red : Brushes.Blue;
                            StatusText.Text = $"Current Player: {_gameBoard.CurrentPlayer}";
                        }
                    }
                    else // no one won yet
                    {
                      _gameBoard.SwitchPlayer();                        
                    }
                    UpdateStatus();
                }
            }
        }
        
        

         #endregion

        #region WonVideo
        private void ShowFireworks()
        {
            FireworksPopup.IsOpen = true;  // Show the Popup
            FireworksMedia.Visibility = Visibility.Visible; // Make the MediaElement visible
            FireworksMedia.Play();         // Play the fireworks video
        }

        private void ClosePopupButton_Click(object sender, RoutedEventArgs e)
        {
            FireworksPopup.IsOpen = false; // Close the Popup
            FireworksMedia.Stop();         // Stop the video
        }

        private void FireworksMedia_MediaEnded(object sender, RoutedEventArgs e)
        {
            FireworksPopup.IsOpen = false; // Close the Popup after video ends
            FireworksMedia.Stop();         // Stop the video
        }
        #endregion
        public void New_Game(object sender, RoutedEventArgs e)
        {

            GameBoard.Children.Clear();
            InitializeBoard();
            _gameBoard.CurrentPlayer = Player.Blue;
            rematch.Visibility = Visibility.Collapsed;
            _gameBoard.FullColumn = new bool[7];

            for (int i = 0; i < _gameBoard.Board.GetLength(0); i++) 
            {
                for (int j = 0; j < _gameBoard.Board.GetLength(1); j++)
                {
                    _gameBoard.Board[i, j] = Player.None;
                }
            }
            WayWon = Won.noWin;
            UpdateStatus();
           
        }
        public void UpdateStatus()
        {
            if (_gameBoard.AllColumnsFull(_gameBoard.FullColumn)) // all columns are full
            {
                StatusText.Foreground = Brushes.Purple;
                StatusText.Text = "it is a tie!";
                GameLogic.WayWon = Won.noWin;
                rematch.Visibility = Visibility.Visible;
            }
            else if (!(WayWon == Won.noWin))
            {
                StatusText.Text = $"{_gameBoard.CurrentPlayer} wins!";
            }
            else 
            {
                StatusText.Foreground = (_gameBoard.CurrentPlayer == Player.Red) ? Brushes.Red : Brushes.Blue;
                StatusText.Text = $"Current Player: {_gameBoard.CurrentPlayer}";
            }


        }
    } 
}