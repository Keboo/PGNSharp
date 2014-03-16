using System;

namespace PGNSharp
{
    public class Move
    {
        public Location From { get; private set; }
        public Location To { get; private set; }
        public Piece Piece { get; private set; }
        public bool IsCastle { get; set; }

        public Move(Piece piece, Location from, Location to)
        {
            From = from;
            To = to;
            Piece = piece;
        }

        public static Move GetCastle(PieceColor color, BoardSide side)
        {
            if (color == PieceColor.None) throw new ArgumentException("Piece Color for castle move must be specified.");
            if (side == BoardSide.None) throw new ArgumentException("Board Side for castle move must be specified.");

            switch (color)
            {
                case PieceColor.White:
                    return new Move(Piece.WhiteKing, Location.E1, side == BoardSide.KingSide ? Location.G1 : Location.C1) { IsCastle = true };
                case PieceColor.Black:
                    return new Move(Piece.BlackKing, Location.E8, side == BoardSide.KingSide ? Location.G8 : Location.C8) { IsCastle = true };
            }
            throw new InvalidOperationException();
        }
    }

    public enum BoardSide
    {
        None,
        KingSide,
        QueenSide
    }
}