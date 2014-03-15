using System.Collections.Generic;

namespace PGNSharp
{
    public class Board
    {
        private readonly BoardSpace[][] _board = new BoardSpace[8][];

        internal Board()
        {
            for (int i = 0; i < 8; i++)
            {
                _board[i] = new BoardSpace[8];
                for (int j = 0; j < 8; j++)
                    _board[i][j] = new BoardSpace(new Location((char)('A' + i), j + 1));
            }
        }

        public void SetupInitialPosition()
        {
            foreach (var space in AllSpaces())
                space.Piece = null;

            GetSpace(Location.A1).Piece = Piece.WhiteRook;
            GetSpace(Location.B1).Piece = Piece.WhiteKnight;
            GetSpace(Location.C1).Piece = Piece.WhiteBishop;
            GetSpace(Location.D1).Piece = Piece.WhiteQueen;
            GetSpace(Location.E1).Piece = Piece.WhiteKing;
            GetSpace(Location.F1).Piece = Piece.WhiteBishop;
            GetSpace(Location.G1).Piece = Piece.WhiteKnight;
            GetSpace(Location.H1).Piece = Piece.WhiteRook;
            for (int i = 0; i < 8; i++)
                GetSpace(new Location((char)(i + 'A'), 2)).Piece = Piece.WhitePawn;

            GetSpace(Location.A8).Piece = Piece.BlackRook;
            GetSpace(Location.B8).Piece = Piece.BlackKnight;
            GetSpace(Location.C8).Piece = Piece.BlackBishop;
            GetSpace(Location.D8).Piece = Piece.BlackQueen;
            GetSpace(Location.E8).Piece = Piece.BlackKing;
            GetSpace(Location.F8).Piece = Piece.BlackBishop;
            GetSpace(Location.G8).Piece = Piece.BlackKnight;
            GetSpace(Location.H8).Piece = Piece.BlackRook;
            for (int i = 0; i < 8; i++)
                GetSpace(new Location((char)(i + 'A'), 7)).Piece = Piece.BlackPawn;
        }

        private BoardSpace GetSpace(Location location)
        {
            var file = location.File - 'A';
            var rank = location.Rank - 1;
            return _board[file][rank];
        }

        private IEnumerable<BoardSpace> AllSpaces()
        {
            for(int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    yield return _board[i][j];
        }

        private class BoardSpace
        {
            public Location Location { get; private set; }
            public Piece Piece { get; set; }

            public BoardSpace(Location location)
            {
                Location = location;
            }
        }

        public Piece GetPiece(Location location)
        {
            return GetSpace(location).Piece;
        }
    }
}