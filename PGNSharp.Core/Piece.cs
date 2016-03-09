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

        public PieceType Type => _type;

        public PieceColor Color => _color;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Piece) obj);
        }

        private bool Equals(Piece other)
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
            return $"{Color} {Type}";
        }

        public static Piece WhitePawn => new Piece(PieceType.Pawn, PieceColor.White);

        public static Piece WhiteRook => new Piece(PieceType.Rook, PieceColor.White);

        public static Piece WhiteKnight => new Piece( PieceType.Knight, PieceColor.White);

        public static Piece WhiteBishop => new Piece( PieceType.Bishop, PieceColor.White);

        public static Piece WhiteKing => new Piece( PieceType.King, PieceColor.White);

        public static Piece WhiteQueen => new Piece( PieceType.Queen, PieceColor.White);

        public static Piece BlackPawn => new Piece( PieceType.Pawn, PieceColor.Black );

        public static Piece BlackRook => new Piece( PieceType.Rook, PieceColor.Black );

        public static Piece BlackKnight => new Piece( PieceType.Knight, PieceColor.Black );

        public static Piece BlackBishop => new Piece( PieceType.Bishop, PieceColor.Black );

        public static Piece BlackKing => new Piece( PieceType.King, PieceColor.Black );

        public static Piece BlackQueen => new Piece( PieceType.Queen, PieceColor.Black );
    }
}