using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_WPF.Code
{
    internal class Board
    {
        const int SIZE_X = 10;
        const int SIZE_Y = 20;
        const double InitialDelay = 1000;

        private readonly bool[][] board; // true - occupied, false - empty
        private Tetromino currentPiece;
        private Tetromino nextPiece;
        private int score;
        private int level;
        private int lines;
        private double delay;
        private readonly Random generator;

        public Board(int Seed)
        {
            score = 0;
            level = 0;
            lines = 0;
            delay = InitialDelay;

            board = new bool[SIZE_Y][];
            for (int row = 0; row < SIZE_Y; row++) board[row] = new bool[SIZE_X];

            generator = new Random(Seed);
            currentPiece = GenerateNewTetromino();
            nextPiece = GenerateNewTetromino();
        }

        public Board() : this((int)DateTimeOffset.UtcNow.ToUnixTimeSeconds())
        { }

        private Tetromino GenerateNewTetromino()
        {
            Array values = Enum.GetValues(typeof(Tetromino.Types));
            Tetromino.Types type = (Tetromino.Types)values.GetValue(generator.Next(values.Length));

            Tetromino t = new Tetromino(type);
            return t;
        }
    }
}
