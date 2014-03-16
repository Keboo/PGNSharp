using System;
using System.Collections.Generic;

namespace PGNSharp
{
    public class Board
    {
        private readonly Piece[][] _board = new Piece[8][];
        //Even numbered moves are white, odd numbered moves are black
        private readonly List<Move> _moves = new List<Move>();
        private int _moveIndex;

        internal Board()
        {
            for (int i = 0; i < 8; i++)
            {
                _board[i] = new Piece[8];
            }
        }

        public void SetupInitialPosition()
        {
            foreach (var space in _board)
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

        public void AddMove(Move move)
        {
            if (move == null) throw new ArgumentNullException("move");
            var foundPiece = GetPiece(move.From);
            if (move.Piece.Equals(foundPiece) == false)
                throw new InvalidOperationException();
            
            _moves.Add(move);

            NextMove();
        }

        public Move NextMove()
        {
            var move = _moves[_moveIndex++];
            //TODO: remove duplicate calls to GetSpace
            //TODO: Handle en passant
            if (move.IsCastle)
            {
                if (move.To.Equals(Location.G1))
                {
                    GetSpace(Location.F1).Piece = GetSpace(Location.H1).Piece;
                    GetSpace(Location.H1).Piece = null;
                }
                else if (move.To.Equals(Location.C1))
                {
                    GetSpace(Location.D1).Piece = GetSpace(Location.A1).Piece;
                    GetSpace(Location.A1).Piece = null;
                }
                else if (move.To.Equals(Location.G8))
                {
                    GetSpace(Location.F8).Piece = GetSpace(Location.H8).Piece;
                    GetSpace(Location.H8).Piece = null;
                }
                else if (move.To.Equals(Location.C8))
                {
                    GetSpace(Location.D8).Piece = GetSpace(Location.A8).Piece;
                    GetSpace(Location.A8).Piece = null;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
            else
            {
                GetSpace(move.To).Piece = GetSpace(move.From).Piece;
                GetSpace(move.From).Piece = null;
            }
            return move;
        }

        public void ResetMoves()
        {
            SetupInitialPosition();
            _moveIndex = 0;
        }

        public Piece GetPiece(Location location)
        {
            var file = location.File - 'a';
            var rank = location.Rank - 1;
            return _board[file][rank];
        }

        private void SetPiece(Location location, Piece piece)
        {
            var file = location.File - 'a';
            var rank = location.Rank - 1;
            _board[file][rank] = piece;
        }
        
    }
}