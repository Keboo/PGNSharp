using System;

namespace PGNSharp
{
    public class Location
    {
        private readonly int _rank ;
        private readonly char _file;

        public Location( char file, int rank )
        {
            file = char.ToUpper(file);
            if (file < 'A' || file > 'H')
                throw new ArgumentException("File must be between 'A' and 'H'");
            if (rank < 1 || rank > 8)
                throw new ArgumentException("Rank must be between 1 and 8");
            _rank = rank;
            _file = file;
        }

        public int Rank
        {
            get { return _rank; }
        }

        public char File
        {
            get { return _file; }
        }

        public override bool Equals( object obj )
        {
            var location = obj as Location;
            if (location != null)
                return Equals(location);
            return false;
        }

        protected bool Equals( Location other )
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

        public static Location A1
        {
            get { return new Location('A', 1);}
        }

        public static Location A2
        {
            get { return new Location( 'A', 2 ); }
        }

        public static Location A3
        {
            get { return new Location( 'A', 3 ); }
        }

        public static Location A4
        {
            get { return new Location( 'A', 4 ); }
        }

        public static Location A5
        {
            get { return new Location( 'A', 5 ); }
        }

        public static Location A6
        {
            get { return new Location( 'A', 6 ); }
        }

        public static Location A7
        {
            get { return new Location( 'A', 7 ); }
        }

        public static Location A8
        {
            get { return new Location( 'A', 8 ); }
        }

        public static Location B1
        {
            get { return new Location( 'B', 1 ); }
        }

        public static Location B2
        {
            get { return new Location( 'B', 2 ); }
        }

        public static Location B3
        {
            get { return new Location( 'B', 3 ); }
        }

        public static Location B4
        {
            get { return new Location( 'B', 4 ); }
        }

        public static Location B5
        {
            get { return new Location( 'B', 5 ); }
        }

        public static Location B6
        {
            get { return new Location( 'B', 6 ); }
        }

        public static Location B7
        {
            get { return new Location( 'B', 7 ); }
        }

        public static Location B8
        {
            get { return new Location( 'B', 8 ); }
        }

        public static Location C1
        {
            get { return new Location( 'C', 1 ); }
        }

        public static Location C2
        {
            get { return new Location( 'C', 2 ); }
        }

        public static Location C3
        {
            get { return new Location( 'C', 3 ); }
        }

        public static Location C4
        {
            get { return new Location( 'C', 4 ); }
        }

        public static Location C5
        {
            get { return new Location( 'C', 5 ); }
        }

        public static Location C6
        {
            get { return new Location( 'C', 6 ); }
        }

        public static Location C7
        {
            get { return new Location( 'C', 7 ); }
        }

        public static Location C8
        {
            get { return new Location( 'C', 8 ); }
        }

        public static Location D1
        {
            get { return new Location( 'D', 1 ); }
        }

        public static Location D2
        {
            get { return new Location( 'D', 2 ); }
        }

        public static Location D3
        {
            get { return new Location( 'D', 3 ); }
        }

        public static Location D4
        {
            get { return new Location( 'D', 4 ); }
        }

        public static Location D5
        {
            get { return new Location( 'D', 5 ); }
        }

        public static Location D6
        {
            get { return new Location( 'D', 6 ); }
        }

        public static Location D7
        {
            get { return new Location( 'D', 7 ); }
        }

        public static Location D8
        {
            get { return new Location( 'D', 8 ); }
        }

        public static Location E1
        {
            get { return new Location( 'E', 1 ); }
        }

        public static Location E2
        {
            get { return new Location( 'E', 2 ); }
        }

        public static Location E3
        {
            get { return new Location( 'E', 3 ); }
        }

        public static Location E4
        {
            get { return new Location( 'E', 4 ); }
        }

        public static Location E5
        {
            get { return new Location( 'E', 5 ); }
        }

        public static Location E6
        {
            get { return new Location( 'E', 6 ); }
        }

        public static Location E7
        {
            get { return new Location( 'E', 7 ); }
        }

        public static Location E8
        {
            get { return new Location( 'E', 8 ); }
        }

        public static Location F1
        {
            get { return new Location( 'F', 1 ); }
        }

        public static Location F2
        {
            get { return new Location( 'F', 2 ); }
        }

        public static Location F3
        {
            get { return new Location( 'F', 3 ); }
        }

        public static Location F4
        {
            get { return new Location( 'F', 4 ); }
        }

        public static Location F5
        {
            get { return new Location( 'F', 5 ); }
        }

        public static Location F6
        {
            get { return new Location( 'F', 6 ); }
        }

        public static Location F7
        {
            get { return new Location( 'F', 7 ); }
        }

        public static Location F8
        {
            get { return new Location( 'F', 8 ); }
        }

        public static Location G1
        {
            get { return new Location( 'G', 1 ); }
        }

        public static Location G2
        {
            get { return new Location( 'G', 2 ); }
        }

        public static Location G3
        {
            get { return new Location( 'G', 3 ); }
        }

        public static Location G4
        {
            get { return new Location( 'G', 4 ); }
        }

        public static Location G5
        {
            get { return new Location( 'G', 5 ); }
        }

        public static Location G6
        {
            get { return new Location( 'G', 6 ); }
        }

        public static Location G7
        {
            get { return new Location( 'G', 7 ); }
        }

        public static Location G8
        {
            get { return new Location( 'G', 8 ); }
        }

        public static Location H1
        {
            get { return new Location( 'H', 1 ); }
        }

        public static Location H2
        {
            get { return new Location( 'H', 2 ); }
        }

        public static Location H3
        {
            get { return new Location( 'H', 3 ); }
        }

        public static Location H4
        {
            get { return new Location( 'H', 4 ); }
        }

        public static Location H5
        {
            get { return new Location( 'H', 5 ); }
        }

        public static Location H6
        {
            get { return new Location( 'H', 6 ); }
        }

        public static Location H7
        {
            get { return new Location( 'H', 7 ); }
        }

        public static Location H8
        {
            get { return new Location( 'H', 8 ); }
        }
    }
}