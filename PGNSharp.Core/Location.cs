using System;

namespace PGNSharp.Core
{
    public class Location
    {
        private readonly int _rank ;
        private readonly char _file;

        public static Location Parse(string location)
        {
            if (location == null) throw new ArgumentNullException(nameof(location));
            if (location.Length != 2) throw new ArgumentException($"Could not parse '{location}' and a location");

            int rank = int.Parse(location[1].ToString());
            return new Location(location[0], rank);
        }

        public static bool TryParse(string locationString, out Location location)
        {
            location = null;
            if (locationString?.Length != 2) return false;

            int rank;
            if (int.TryParse(locationString[1].ToString(), out rank))
            {
                location = new Location(locationString[0], rank);
                return true;
            }
            return false;
        }

        public static Location FromOffset(Location location, int fileOffset, int rankOffset)
        {
            var rank = location.Rank + rankOffset;
            var file = (char) (location.File + fileOffset);
            if (rank < 1 || rank > 8)
                return null;
            if (file < 'a' || file > 'h')
                return null;
            return new Location(file, rank);
        }

        public Location( char file, int rank )
        {
            file = char.ToLower(file);
            if (file < 'a' || file > 'h')
                throw new ArgumentException("File must be between 'A' and 'H'");
            if (rank < 1 || rank > 8)
                throw new ArgumentException("Rank must be between 1 and 8");
            _rank = rank;
            _file = file;
        }

        public int Rank => _rank;

        public char File => _file;

        public bool MatchesHint(string fromLocationHint)
        {
            if (string.IsNullOrEmpty(fromLocationHint))
                return true; //No hint so assume a match
            return fromLocationHint.Equals(ToString()) ||
                   fromLocationHint.Equals(File.ToString()) ||
                   fromLocationHint.Equals(Rank.ToString());
        }

        public override bool Equals( object obj )
        {
            var location = obj as Location;
            if (location != null)
                return Equals(location);
            return false;
        }

        private bool Equals( Location other )
        {
            return _rank == other._rank && _file == other._file;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ( _rank * 397 ) ^ _file.GetHashCode();
            }
        }

        public override string ToString()
        {
            return string.Concat(File, Rank);
        }

        public static Location A1 => new Location('A', 1);

        public static Location A2 => new Location( 'A', 2 );

        public static Location A3 => new Location( 'A', 3 );

        public static Location A4 => new Location( 'A', 4 );

        public static Location A5 => new Location( 'A', 5 );

        public static Location A6 => new Location( 'A', 6 );

        public static Location A7 => new Location( 'A', 7 );

        public static Location A8 => new Location( 'A', 8 );

        public static Location B1 => new Location( 'B', 1 );

        public static Location B2 => new Location( 'B', 2 );

        public static Location B3 => new Location( 'B', 3 );

        public static Location B4 => new Location( 'B', 4 );

        public static Location B5 => new Location( 'B', 5 );

        public static Location B6 => new Location( 'B', 6 );

        public static Location B7 => new Location( 'B', 7 );

        public static Location B8 => new Location( 'B', 8 );

        public static Location C1 => new Location( 'C', 1 );

        public static Location C2 => new Location( 'C', 2 );

        public static Location C3 => new Location( 'C', 3 );

        public static Location C4 => new Location( 'C', 4 );

        public static Location C5 => new Location( 'C', 5 );

        public static Location C6 => new Location( 'C', 6 );

        public static Location C7 => new Location( 'C', 7 );

        public static Location C8 => new Location( 'C', 8 );

        public static Location D1 => new Location( 'D', 1 );

        public static Location D2 => new Location( 'D', 2 );

        public static Location D3 => new Location( 'D', 3 );

        public static Location D4 => new Location( 'D', 4 );

        public static Location D5 => new Location( 'D', 5 );

        public static Location D6 => new Location( 'D', 6 );

        public static Location D7 => new Location( 'D', 7 );

        public static Location D8 => new Location( 'D', 8 );

        public static Location E1 => new Location( 'E', 1 );

        public static Location E2 => new Location( 'E', 2 );

        public static Location E3 => new Location( 'E', 3 );

        public static Location E4 => new Location( 'E', 4 );

        public static Location E5 => new Location( 'E', 5 );

        public static Location E6 => new Location( 'E', 6 );

        public static Location E7 => new Location( 'E', 7 );

        public static Location E8 => new Location( 'E', 8 );

        public static Location F1 => new Location( 'F', 1 );

        public static Location F2 => new Location( 'F', 2 );

        public static Location F3 => new Location( 'F', 3 );

        public static Location F4 => new Location( 'F', 4 );

        public static Location F5 => new Location( 'F', 5 );

        public static Location F6 => new Location( 'F', 6 );

        public static Location F7 => new Location( 'F', 7 );

        public static Location F8 => new Location( 'F', 8 );

        public static Location G1 => new Location( 'G', 1 );

        public static Location G2 => new Location( 'G', 2 );

        public static Location G3 => new Location( 'G', 3 );

        public static Location G4 => new Location( 'G', 4 );

        public static Location G5 => new Location( 'G', 5 );

        public static Location G6 => new Location( 'G', 6 );

        public static Location G7 => new Location( 'G', 7 );

        public static Location G8 => new Location( 'G', 8 );

        public static Location H1 => new Location( 'H', 1 );

        public static Location H2 => new Location( 'H', 2 );

        public static Location H3 => new Location( 'H', 3 );

        public static Location H4 => new Location( 'H', 4 );

        public static Location H5 => new Location( 'H', 5 );

        public static Location H6 => new Location( 'H', 6 );

        public static Location H7 => new Location( 'H', 7 );

        public static Location H8 => new Location( 'H', 8 );
    }
}