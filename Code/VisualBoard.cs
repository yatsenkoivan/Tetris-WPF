using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tetris_WPF.Code
{
    internal class VisualBoard
    {
        private Board board;

        private List<Label> labels;

        public VisualBoard(Board board)
        {
            this.board = board;
            labels = new List<Label>();
        }
        public VisualBoard() : this(new Board())
        { }

        public void ShowFigure(ref Grid grid)
        {
            Tetromino t = board.CurrentFigure;
            Label l;
            foreach (Coords c in t.GetCoords)
            {
                l = new Label();
                l.Background = Brushes.LightBlue;
                Grid.SetColumn(l, c.X);
                Grid.SetRow(l, c.Y);
                labels.Add(l);
                grid.Children.Add(l);
            }
        }

        public void ShowNextFigure(ref Grid grid, int offset_x, int offset_y)
        {
            Tetromino t = board.NextFigure;
            Label l;
            foreach (Coords c in t.GetCoords)
            {
                l = new Label();
                l.Background = Brushes.LightBlue;
                Grid.SetColumn(l, c.X + offset_x - Tetromino.InitialX);
                Grid.SetRow(l, c.Y + offset_y);
                labels.Add(l);
                grid.Children.Add(l);
            }
        }

    }
}
