using System;

namespace PGNSharp
{
    public class Piece
    {
        private readonly PieceType _type;
        private readonly PieceColor _color;

        private Piece( PieceType type, PieceColor color )
        {
            if (type == PieceType.None)
                throw new ArgumentException("Type must be specified");
            if (color == PieceColor.None)
                throw new ArgumentException("Color must be specified");
            _type = type;
            _color = color;
        }

        public PieceType Type
        {
            get { return _type; }
        }

        public PieceColor Color
        {
            get { return _color; }
        }

        public static Piece WhitePawn
        {
            get { return new Piece(PieceType.Pawn, PieceColor.White); }
        }

        public static Piece WhiteRook
        {
            get { return new Piece(PieceType.Rock, PieceColor.White); }
        }

        public static Piece WhiteKnight
        {
            get { return new Piece( PieceType.Knight, PieceColor.White); }
        }

        public static Piece WhiteBishop
        {
            get { return new Piece( PieceType.Bishop, PieceColor.White); }
        }

        public static Piece WhiteKing
        {
            get { return new Piece( PieceType.King, PieceColor.White); }
        }

        public static Piece WhiteQueen
        {
            get { return new Piece( PieceType.Queen, PieceColor.White); }
        }

        public static Piece BlackPawn
        {
            get { return new Piece( PieceType.Pawn, PieceColor.Black ); }
        }

        public static Piece BlackRook
        {
            get { return new Piece( PieceType.Rock, PieceColor.Black ); }
        }

        public static Piece BlackKnight
        {
            get { return new Piece( PieceType.Knight, PieceColor.Black ); }
        }

        public static Piece BlackBishop
        {
            get { return new Piece( PieceType.Bishop, PieceColor.Black ); }
        }

        public static Piece BlackKing
        {
            get { return new Piece( PieceType.King, PieceColor.Black ); }
        }

        public static Piece BlackQueen
        {
            get { return new Piece( PieceType.Queen, PieceColor.Black ); }
        }
        
    }

    public enum PieceColor
    {
        None,
        Black,
        White
    }

    public enum PieceType
    {
        None,
        Pawn,
        Knight,
        Bishop,
        Rock,
        Queen,
        King
    }
}