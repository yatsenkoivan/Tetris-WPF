using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Tetris_WPF.Code;
using System.Windows.Threading;

namespace Tetris_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string StartText = "Start";
        private const string StopText = "Stop";
        private const string ContinueText = "Continue";

        private VisualBoard visualBoard;
        DispatcherTimer dt;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Start_ButtonClicked(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;

            if (b.Content.ToString() == StartText)
            {
                StartGame();
            }
            else if (b.Content.ToString() == StopText)
            {
                StopGame();
            }
            else if (b.Content.ToString() == ContinueText)
            {
                ContinueGame();
            }
        }

        private void StartGame()
        {
            ScoreTextBlock.Visibility = Visibility.Visible;
            ScoreValueTextBlock.Visibility = Visibility.Visible;
            LinesTextBlock.Visibility = Visibility.Visible;
            LinesValueTextBlock.Visibility = Visibility.Visible;

            visualBoard = new VisualBoard(ref PlayGrid, ref NextFigureGrid, ref ScoreValueTextBlock, ref LinesValueTextBlock);
            StartButton.Content = StopText;

            visualBoard.ShowCurrentFigure();
            visualBoard.ShowNextFigure();
            visualBoard.ShowScore();
            visualBoard.ShowLines();

            dt = new DispatcherTimer(DispatcherPriority.Send);
            dt.Tick += new EventHandler(visualBoard.NextTick);
            dt.Interval = TimeSpan.FromMilliseconds(visualBoard.Delay);
            dt.Start();
        }

        private void StopGame()
        {
            dt.Stop();
            visualBoard.Stopped = true;
            StartButton.Content = ContinueText;
        }

        private void ContinueGame()
        {
            visualBoard.Stopped = false;
            StartButton.Content = StopText;

            dt.Start();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (visualBoard == null || visualBoard.Stopped == true) return;
            if (e.Key == Key.A || e.Key == Key.Left)
            {
                visualBoard.MoveLeft();
            }
            if (e.Key == Key.D || e.Key == Key.Right)
            {
                visualBoard.MoveRight();
            }
            if (e.Key == Key.S || e.Key == Key.Down)
            {
                visualBoard.MoveDown();
            }
            if (e.Key == Key.R || e.Key == Key.Up)
            {
                visualBoard.Rotate();
            }
        }
    }
}
