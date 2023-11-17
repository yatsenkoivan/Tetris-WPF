using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace Tetris_WPF.Code
{
    internal class VisualBoard
    {
        private readonly Brush BorderBrush = Brushes.Black;
        private readonly Thickness BorderThickness = new Thickness(1);
        
        private Board board;
        private readonly Grid MainGrid;
        private readonly Grid NextFigureGrid;
        private readonly TextBlock ScoreValueTextBlock;
        private readonly TextBlock LinesValueTextBlock;
        private List<Label> currentFigure_labels;
        private List<Label> nextFigure_labels;

        private const double InitialDelay = 1000;
        private double delay;

        public bool Stopped { get; set; }
        public double Delay { get => delay; }

        public VisualBoard(Board board, ref Grid MainGrid, ref Grid NextFigureGrid, ref TextBlock ScoreValueTextBlock, ref TextBlock LinesValueTextBlock)
        {
            this.board = board;
            this.MainGrid = MainGrid;
            this.NextFigureGrid = NextFigureGrid;
            this.ScoreValueTextBlock = ScoreValueTextBlock;
            this.LinesValueTextBlock = LinesValueTextBlock;

            currentFigure_labels = new List<Label>();
            nextFigure_labels = new List<Label>();
            Stopped = false;
            delay = InitialDelay;
        }
        public VisualBoard(ref Grid MainGrid, ref Grid NextFigureGrid, ref TextBlock ScoreValueTextBlock, ref TextBlock LinesValueTextBlock)
            : this(new Board(), ref MainGrid, ref NextFigureGrid, ref ScoreValueTextBlock, ref LinesValueTextBlock)
        { }

        public void ShowCurrentFigure()
        {
            currentFigure_labels.Clear();
            Tetromino t = board.CurrentFigure;
            Label l;
            foreach (Coord c in t.Coords)
            {
                l = new Label();
                l.Background = t.Color;
                l.BorderThickness = BorderThickness;
                l.BorderBrush = BorderBrush;
                Grid.SetColumn(l, c.X);
                Grid.SetRow(l, c.Y);
                currentFigure_labels.Add(l);
                MainGrid.Children.Add(l);
            }
        }
        public void HideNextFigure()
        {
            foreach (Label l in nextFigure_labels)
            {
                NextFigureGrid.Children.Remove(l);
            }
        }

        public void ShowNextFigure(int offset_x=1, int offset_y=1)
        {
            HideNextFigure();
            nextFigure_labels.Clear();
            Tetromino t = board.NextFigure;
            Label l;
            foreach (Coord c in t.Coords)
            {
                l = new Label();
                l.Background = t.Color;
                l.BorderThickness = BorderThickness;
                l.BorderBrush = BorderBrush;
                Grid.SetColumn(l, c.X + offset_x - Tetromino.InitialX);
                Grid.SetRow(l, c.Y + offset_y);
                nextFigure_labels.Add(l);
                NextFigureGrid.Children.Add(l);
            }
        }

        public void ShowScore()
        {
            ScoreValueTextBlock.Text = board.Score.ToString();
        }

        public void ShowLines()
        {
            LinesValueTextBlock.Text = board.Lines.ToString();
        }

        private void UpdateCurrentFigureLabels()
        {
            Coord c;
            Label l;
            for (int label_id=0; label_id < currentFigure_labels.Count; label_id++)
            {
                c = board.CurrentFigure.Coords[label_id];
                l = currentFigure_labels[label_id];
                Grid.SetColumn(l, c.X);
                Grid.SetRow(l, c.Y);
            }
        }

        public void NextTick(object sender, EventArgs e)
        {
            if (Stopped == false)
            {
                MoveDown();
            }
        }

        public void MoveLeft()
        {
            if (board.MoveLeft())
            {
                UpdateCurrentFigureLabels();
            }
        }
        public void MoveRight()
        {
            if (board.MoveRight())
            {
                UpdateCurrentFigureLabels();
            }
        }
        public void MoveDown()
        {
            if (board.MoveDown())
            {
                UpdateCurrentFigureLabels();
            }
            else
            {
                board.SaveCurrentFigureCoords();
                ShowNextFigure();
                ShowCurrentFigure();
            }
        }

        public void Rotate()
        {
            if (board.Rotate())
            {
                UpdateCurrentFigureLabels();
            }
        }
    }
}
