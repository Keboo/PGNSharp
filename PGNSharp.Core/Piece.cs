using System;

namespace PGNSharp.Core
{
    public class Piece
    {
        private readonly PieceType _type;
        private readonly PieceColor _color;

        public Piece( PieceType type, PieceColor color )
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

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Piece) obj);
        }

        protected bool Equals(Piece other)
        {
            return _type == other._type && _color == other._color;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int)_type * 397) ^ (int)_color;
            }
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Color, Type);
        }

        public static Piece WhitePawn
        {
            get { return new Piece(PieceType.Pawn, PieceColor.White); }
        }

        public static Piece WhiteRook
        {
            get { return new Piece(PieceType.Rook, PieceColor.White); }
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
            get { return new Piece( PieceType.Rook, PieceColor.Black ); }
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
        Rook,
        Queen,
        King
    }
}