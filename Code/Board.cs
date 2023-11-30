using System;
using System.Linq;

namespace Tetris_WPF.Code
{
    internal class Board
    {
        public const int SIZE_X = 10;
        public const int SIZE_Y = 20;

        private readonly Tetromino.Types?[][] board;
        private Tetromino currentFigure;
        private Tetromino nextFigure;
        private readonly Random generator;

        public Tetromino.Types? this[int row, int col]
        {
            get => board[row][col];
        }
        public int Columns { get => SIZE_X; }
        public int Rows { get => SIZE_Y; }
        public int Score { get; private set; }
        public int Level { get; private set; }
        public int Lines { get; private set; }

        public Tetromino CurrentFigure { get => currentFigure; private set => currentFigure = value; }
        public Tetromino NextFigure { get => nextFigure; private set => nextFigure = value; }
        public Board(int UniqueSeed=-1)
        {
            Score = 0;
            Level = 1;
            Lines = 0;

            board = new Tetromino.Types?[SIZE_Y][];
            for (int row = 0; row < SIZE_Y; row++) board[row] = new Tetromino.Types?[SIZE_X];

            if (UniqueSeed == -1) generator = new Random();
            else generator = new Random(UniqueSeed);

            CurrentFigure = GenerateNewTetromino();
            NextFigure = GenerateNewTetromino();
        }

        public Board() : this((int)DateTimeOffset.UtcNow.ToUnixTimeSeconds())
        { }

        public bool GameEndCheck()
        {
            foreach (Coord c in CurrentFigure.Coords)
            {
                if (board[c.Y][c.X] != null) return true;
            }
            return false;
        }
        private Tetromino GenerateNewTetromino()
        {
            Array values = Enum.GetValues(typeof(Tetromino.Types));
            Tetromino.Types type = (Tetromino.Types)values.GetValue(generator.Next(values.Length));

            Tetromino t = new Tetromino(type);
            return t;
        }
        public void SaveCurrentFigureCoords()
        {
            foreach (Coord c in CurrentFigure.Coords)
            {
                board[c.Y][c.X] = CurrentFigure.Type;
            }
            ShiftTetrominoes();
            NextFigure = GenerateNewTetromino();
        }
        private void ShiftTetrominoes()
        {
            CurrentFigure = NextFigure;
        }

        public bool CanMoveDown()
        {
            foreach (Coord c in CurrentFigure.Coords)
            {
                if (c.Y + 1 >= SIZE_Y || board[c.Y + 1][c.X] != null) return false;
            }
            return true;
        }

        public bool CanMoveLeft()
        {
            foreach (Coord c in CurrentFigure.Coords)
            {
                if (c.X - 1 < 0 || board[c.Y][c.X - 1] != null) return false;
            }
            return true;
        }

        public bool CanMoveRight()
        {
            foreach (Coord c in CurrentFigure.Coords)
            {
                if (c.X + 1 >= SIZE_X || board[c.Y][c.X + 1] != null) return false;
            }
            return true;
        }

        public bool MoveDown()
        {
            if (CanMoveDown() == false) return false;
            foreach (Coord c in CurrentFigure.Coords)
            {
                c.Y++;
            }
            return true;
        }

        public bool MoveLeft()
        {
            if (CanMoveLeft() == false) return false;
            foreach (Coord c in CurrentFigure.Coords)
            {
                c.X--;
            }
            return true;
        }

        public bool MoveRight()
        {
            if (CanMoveRight() == false) return false;
            foreach (Coord c in CurrentFigure.Coords)
            {
                c.X++;
            }
            return true;
        }

        //true - next level
        public bool BurnLines()
        {
            for (int row=Rows-1; row>=0; row--)
            {
                if (board[row].All(x => x != null))
                {
                    board[row] = new Tetromino.Types?[Columns];
                    Lines++;
                }
            }

            int current_row = Rows-1;
            for (int row=Rows-1; row>=0; row--)
            {
                if (board[row].Any(x => x != null))
                {
                    (board[current_row], board[row]) = (board[row], board[current_row]);
                    current_row--;
                }
            }

            if (Lines > Level*10)
            {
                Level++;
                return true;
            }
            return false;
        }
        public bool Rotate()
        {
            Coord mid = CurrentFigure.Coords[Tetromino.SIZE/2];

            Coord[] coords = CurrentFigure.Coords;
            Coord[] newcoords = new Coord[coords.Length];

            Coord c;

            for (int coord_idx = 0; coord_idx < newcoords.Length; coord_idx++)
            {
                c = new Coord(coords[coord_idx].X, coords[coord_idx].Y);
                newcoords[coord_idx] = c;

                if (c.X == mid.X && c.Y == mid.Y) continue;

                if (c.X != mid.X && c.Y == mid.Y)
                {
                    c.Y = mid.Y - (mid.X - c.X);
                    c.X = mid.X;
                }
                else if (c.X == mid.X && c.Y != mid.Y)
                {
                    c.X = mid.X + (mid.Y - c.Y);
                    c.Y = mid.Y;
                }

                else if (c.X < mid.X)
                {
                    if (c.Y < mid.Y)
                    {
                        c.X = mid.X + (mid.X - c.X);
                    }
                    else
                    {
                        c.Y = mid.Y + (mid.Y - c.Y);
                    }
                }
                else
                {
                    if (c.Y < mid.Y)
                    {
                        c.Y = mid.Y - (c.Y - mid.Y);
                    }
                    else
                    {
                        c.X = mid.X - (c.X - mid.X);
                    }
                }

                if (c.X < 0 || c.X >= SIZE_X || c.Y < 0 || c.Y >= SIZE_Y || board[c.Y][c.X] != null) return false;

            }

            for (int coord_idx = 0; coord_idx < newcoords.Length; coord_idx++)
            {
                CurrentFigure.Coords[coord_idx].X = newcoords[coord_idx].X;
                CurrentFigure.Coords[coord_idx].Y = newcoords[coord_idx].Y;
            }
            return true;
        }
    }
}
