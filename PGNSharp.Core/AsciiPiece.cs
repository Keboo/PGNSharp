using System;

namespace PGNSharp.Core
{
    public static class AsciiPiece
    {
        public static char GetCharForPiece(Piece piece)
        {
            switch (piece.Type)
            {
                case PieceType.Pawn:
                    return piece.Color == PieceColor.White ? 'P' : 'p';
                case PieceType.Rook:
                    return piece.Color == PieceColor.White ? 'R' : 'r';
                case PieceType.Knight:
                    return piece.Color == PieceColor.White ? 'N' : 'n';
                case PieceType.Bishop:
                    return piece.Color == PieceColor.White ? 'B' : 'b';
                case PieceType.Queen:
                    return piece.Color == PieceColor.White ? 'Q' : 'q';
                case PieceType.King:
                    return piece.Color == PieceColor.White ? 'K' : 'k';
            }
            throw new InvalidOperationException();
        }
    }
}