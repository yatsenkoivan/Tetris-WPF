using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_WPF.Code
{
    internal class Coords
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coords(int x, int y)
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
        private readonly Coords[] coords;

        public Coords[] GetCoords { get => coords; }

        public Tetromino(Types type)
        {
            coords = new Coords[SIZE];
            this.type = type;

            switch (type)
            {
                case Types.I:
                    for (int i = 0; i < SIZE; i++)
                    {
                        coords[i] = new Coords(InitialX + i, 0);
                    }
                    break;
                case Types.J:
                    for (int i = 0; i < SIZE - 1; i++)
                    {
                        coords[i] = new Coords(InitialX + i, 1);
                    }
                    coords[SIZE - 1] = new Coords(InitialX, 0);
                    break;
                case Types.L:
                    for (int i = 0; i < SIZE - 1; i++)
                    {
                        coords[i] = new Coords(InitialX + i, 1);
                    }
                    coords[SIZE - 1] = new Coords(InitialX + SIZE - 2, 0);
                    break;
                case Types.O:
                    for (int i = 0; i < SIZE / 2; i++)
                    {
                        coords[i] = new Coords(InitialX + i, 0);
                        coords[i + SIZE / 2] = new Coords(InitialX + i, 1);
                    }
                    break;
                case Types.S:
                    for (int i = 0; i < SIZE / 2; i++)
                    {
                        coords[i] = new Coords(InitialX + i, 1);
                        coords[i + SIZE / 2] = new Coords(InitialX + i + SIZE / 2 - 1, 0);
                    }
                    break;
                case Types.T:
                    for (int i = 0; i < SIZE - 1; i++)
                    {
                        coords[i] = new Coords(InitialX + i, 1);
                    }
                    coords[SIZE - 1] = new Coords(InitialX + SIZE / 2 - 1, 0);
                    break;
                case Types.Z:
                    for (int i = 0; i < SIZE / 2; i++)
                    {
                        coords[i] = new Coords(InitialX + i + SIZE / 2 - 1, 1);
                        coords[i + SIZE / 2] = new Coords(InitialX + i, 0);
                    }
                    break;
            }

        }

    }
}
