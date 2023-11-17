using System.Windows.Media;

namespace Tetris_WPF.Code
{
    internal class Coord
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coord(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
    internal class Tetromino
    {
        public const int InitialX = (Board.SIZE_X - SIZE)/2;
        public const int SIZE = 4;
        public enum Types
        {
            I, J, L, O, S, T, Z
        }

        private readonly Types type;
        private readonly Coord[] coords;

        public Coord[] Coords { get => coords; }
        public Brush Color
        {
            get
            {
                switch (type)
                {
                    case Types.I:
                        return Brushes.Cyan;
                    case Types.J:
                        return Brushes.Blue;
                    case Types.L:
                        return Brushes.Orange;
                    case Types.O:
                        return Brushes.Yellow;
                    case Types.S:
                        return Brushes.Green;
                    case Types.T:
                        return Brushes.Purple;
                    case Types.Z:
                        return Brushes.Red;
                    default:
                        return Brushes.Black;
                }
            }
        }


        public Tetromino(Types type)
        {
            coords = new Coord[SIZE];
            this.type = type;

            switch (type)
            {
                case Types.I:
                    for (int i = 0; i < SIZE; i++)
                    {
                        coords[i] = new Coord(InitialX + i, 0);
                    }
                    break;
                case Types.J:
                    coords[0] = new Coord(InitialX, 0);
                    for (int i = 0; i < SIZE - 1; i++)
                    {
                        coords[i+1] = new Coord(InitialX + i, 1);
                    }
                    break;
                case Types.L:
                    coords[0] = new Coord(InitialX + SIZE - 2, 0);
                    for (int i = 0; i < SIZE - 1; i++)
                    {
                        coords[i+1] = new Coord(InitialX + i, 1);
                    }
                    break;
                case Types.O:
                    for (int i = 0; i < SIZE / 2; i++)
                    {
                        coords[i] = new Coord(InitialX + i, 0);
                        coords[i + SIZE / 2] = new Coord(InitialX + i, 1);
                    }
                    break;
                case Types.S:
                    for (int i = 0; i < SIZE / 2; i++)
                    {
                        coords[i] = new Coord(InitialX + i, 1);
                        coords[i + SIZE / 2] = new Coord(InitialX + i + SIZE / 2 - 1, 0);
                    }
                    break;
                case Types.T:
                    coords[0] = new Coord(InitialX + SIZE / 2 - 1, 0);
                    for (int i = 0; i < SIZE - 1; i++)
                    {
                        coords[i+1] = new Coord(InitialX + i, 1);
                    }
                    break;
                case Types.Z:
                    for (int i = 0; i < SIZE / 2; i++)
                    {
                        coords[i] = new Coord(InitialX + i, 0);
                        coords[i + SIZE / 2] = new Coord(InitialX + i + SIZE / 2 - 1, 1);
                    }
                    break;
            }

        }

    }
}
