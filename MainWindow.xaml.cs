using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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

        private VisualBoard board;
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
            board = new VisualBoard(ref PlayGrid, ref NextFigureGrid);
            StartButton.Content = StopText;

            board.ShowCurrentFigure();
            board.ShowNextFigure();

            dt = new DispatcherTimer(DispatcherPriority.Send);
            dt.Tick += new EventHandler(board.NextTick);
            dt.Interval = TimeSpan.FromMilliseconds(board.Delay);
            dt.Start();
        }

        private void StopGame()
        {
            dt.Stop();
            board.Stopped = true;
            StartButton.Content = ContinueText;
        }

        private void ContinueGame()
        {
            board.Stopped = false;
            StartButton.Content = StopText;

            dt.Start();
        }
    }
}
