using System;
using System.Linq;
using System.Windows.Controls;
using PGNSharp.Core;

namespace PGNSharp.WP8
{
    public partial class Board
    {
        private readonly TextBlock[,] _boardSpaces = new TextBlock[8,8];

        public Board()
        {
            InitializeComponent();
            foreach (var grid in LayoutRoot.Children.Cast<Grid>())
            {
                //adjust for a1 being in the botton left
                int row = 7 - Grid.GetRow(grid);
                int column = Grid.GetColumn(grid);

                _boardSpaces[column, row] = grid.Children.Cast<TextBlock>().Single();
            }
        }

        public void SetPieces(Game game)
        {
            if (game == null) throw new ArgumentNullException("game");
            for (int rank = 1; rank <= 8; rank++)
            {
                for (char file = 'a'; file <= 'h'; file++)
                {
                    Piece piece = game.GetPiece(new Location(file, rank));
                    _boardSpaces[file - 'a', rank - 1].Text = piece != null ? AsciiPiece.GetCharForPiece(piece).ToString() : "";
                }
            }
        }
    }
}
