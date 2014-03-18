using System;
using System.Collections.Generic;
using System.Diagnostics;

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
            for ( int i = 0; i < 8; i++ )
            {
                _board[i] = new Piece[8];
            }
        }

        public void SetupInitialPosition()
        {
            for ( int i = 0; i < 8; i++ )
                for ( int j = 0; j < 8; j++ )
                    _board[i][j] = null;

            SetPiece( Location.A1, Piece.WhiteRook );
            SetPiece( Location.B1, Piece.WhiteKnight );
            SetPiece( Location.C1, Piece.WhiteBishop );
            SetPiece( Location.D1, Piece.WhiteQueen );
            SetPiece( Location.E1, Piece.WhiteKing );
            SetPiece( Location.F1, Piece.WhiteBishop );
            SetPiece( Location.G1, Piece.WhiteKnight );
            SetPiece( Location.H1, Piece.WhiteRook );
            for ( int i = 0; i < 8; i++ )
                SetPiece( new Location( (char)( 'a' + i ), 2 ), Piece.WhitePawn );

            SetPiece( Location.A8, Piece.BlackRook );
            SetPiece( Location.B8, Piece.BlackKnight );
            SetPiece( Location.C8, Piece.BlackBishop );
            SetPiece( Location.D8, Piece.BlackQueen );
            SetPiece( Location.E8, Piece.BlackKing );
            SetPiece( Location.F8, Piece.BlackBishop );
            SetPiece( Location.G8, Piece.BlackKnight );
            SetPiece( Location.H8, Piece.BlackRook );
            for ( int i = 0; i < 8; i++ )
                SetPiece( new Location( (char)( i + 'a' ), 7 ), Piece.BlackPawn );
        }

        public void AddMove( Move move )
        {
            if ( move == null ) throw new ArgumentNullException( "move" );
            var foundPiece = GetPiece( move.From );
            if ( move.Piece.Equals( foundPiece ) == false )
                throw new InvalidOperationException();

            _moves.Add( move );

            NextMove();
        }

        public Move NextMove()
        {
            var move = _moves[_moveIndex++];
            //TODO: remove duplicate calls to GetSpace
            //TODO: Handle en passant
            if ( move.IsCastle )
            {
                if ( move.To.Equals( Location.G1 ) )
                {
                    SetPiece( Location.F1, GetPiece( Location.H1 ) );
                    SetPiece( Location.H1, null );
                }
                else if ( move.To.Equals( Location.C1 ) )
                {
                    SetPiece( Location.D1, GetPiece( Location.A1 ) );
                    SetPiece( Location.A1, null );
                }
                else if ( move.To.Equals( Location.G8 ) )
                {
                    SetPiece( Location.F8, GetPiece( Location.H8 ) );
                    SetPiece( Location.H8, null );
                }
                else if ( move.To.Equals( Location.C8 ) )
                {
                    SetPiece( Location.D8, GetPiece( Location.A8 ) );
                    SetPiece( Location.A8, null );
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
            SetPiece( move.To, GetPiece( move.From ) );
            SetPiece( move.From, null );

            DebugBoard();
            return move;
        }

        public void ResetMoves()
        {
            SetupInitialPosition();
            _moveIndex = 0;
        }

        public Piece GetPiece( Location location )
        {
            var file = location.File - 'a';
            var rank = location.Rank - 1;
            return _board[file][rank];
        }

        private void SetPiece( Location location, Piece piece )
        {
            var file = location.File - 'a';
            var rank = location.Rank - 1;
            _board[file][rank] = piece;
        }

        [Conditional( "DEBUG" )]
        private void DebugBoard()
        {
            Debug.WriteLine( "Move {0} {1}", ( ( _moves.Count - 1 ) / 2 ) + 1, ( _moves.Count % 2 == 0 ) ? "Black" : "White" );
            Debug.WriteLine( "   -----------------" );
            for ( int i = 7; i >= 0; i-- )
            {
                Debug.Write( string.Format( "{0} | ", i + 1 ) );
                for ( int j = 0; j < 8; j++ )
                {
                    var piece = _board[j][i];
                    if ( piece == null )
                        Debug.Write( "- " );
                    else
                    {
                        switch ( piece.Type )
                        {
                            case PieceType.Pawn:
                                Debug.Write( piece.Color == PieceColor.White ? "P " : "p " );
                                break;
                            case PieceType.Rook:
                                Debug.Write( piece.Color == PieceColor.White ? "R " : "r " );
                                break;
                            case PieceType.Knight:
                                Debug.Write( piece.Color == PieceColor.White ? "N " : "n " );
                                break;
                            case PieceType.Bishop:
                                Debug.Write( piece.Color == PieceColor.White ? "B " : "b " );
                                break;
                            case PieceType.Queen:
                                Debug.Write( piece.Color == PieceColor.White ? "Q " : "q " );
                                break;
                            case PieceType.King:
                                Debug.Write( piece.Color == PieceColor.White ? "K " : "k " );
                                break;
                        }
                    }
                }
                Debug.WriteLine( "|" );
            }
            Debug.WriteLine( "   -----------------" );
            Debug.WriteLine( "    A B C D E F G H" );
        }
    }
}