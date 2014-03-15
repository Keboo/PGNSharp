namespace PGNSharp
{
    public class Move
    {
        public Location From { get; private set; }
        public Location To { get; private set; }
        public Piece Piece { get; private set; }

        public Move(Piece piece, Location from, Location to)
        {
            From = from;
            To = to;
            Piece = piece;
        }
    }
}