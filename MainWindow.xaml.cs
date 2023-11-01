using System;
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
        private bool stopped;

        private VisualBoard board;

        public MainWindow()
        {
            InitializeComponent();
            stopped = false;
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
            stopped = false;
            StartButton.Content = StopText;
            board = new VisualBoard();

            board.ShowFigure(ref PlayGrid);
            board.ShowNextFigure(ref NextFigureGrid, 1, 1);

        }

        private void StopGame()
        {
            stopped = true;
            StartButton.Content = ContinueText;
        }

        private void ContinueGame()
        {
            stopped = false;
            StartButton.Content = StopText;
        }
    }
}
