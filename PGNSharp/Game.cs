using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace PGNSharp
{
    public class Game
    {
        private static readonly Regex _tagPairsRegex = new Regex("(?<=\\s*\\[\\s*)(?<Name>\\w*?)(?:\\s+\")(?<Value>.*?)(?=\"\\s*\\])");
        private static readonly Regex _moveRegex = new Regex(@"((?<MoveNumber>\d+)\.*)?\ (?<WhiteMove>((?<WhitePiece>[PNBRQK]?)((?<WhiteFrom>[a-h][1-8]?)x?)?(?<WhiteTo>[a-h][1-8]))|(?<WhiteCastle>(O-O(-O)?)))\ (?<BlackMove>((?<BlackPiece>[PNBRQK]?)((?<BlackFrom>[a-h][1-8]?)x?)?(?<BlackTo>[a-h][1-8]))|(?<BlackCastle>(O-O(-O)?)))(?=(\ |$))");

        private readonly Dictionary<string, string> _tagPairs = new Dictionary<string, string>();
        private readonly Board _board = new Board();

        private Game()
        {
            _board.SetupInitialPosition();
        }

        //TODO: Make immutable
        //TODO: Add properties for the Seven Tag Roster values (8.1.1)
        public IDictionary<string, string> TagPairs
        {
            get { return _tagPairs; }
        }

        public string Event
        {
            get
            {
                string @event;
                if (TagPairs.TryGetValue("Event", out @event))
                    return @event;
                return null;
            }
        }

        public string Site
        {
            get
            {
                string site;
                if (TagPairs.TryGetValue("Site", out site))
                    return site;
                return null;
            }
        }

        public DateTime? Date
        {
            get
            {
                string dateString;
                if (TagPairs.TryGetValue("Date", out dateString))
                {
                    DateTime dateTime;
                    if (DateTime.TryParseExact(dateString, "yyyy.MM.dd", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out dateTime))
                        return dateTime;
                }
                return null;
            }
        }

        public int? Round
        {
            get
            {
                string roundString;
                if (TagPairs.TryGetValue("Round", out roundString))
                {
                    int round;
                    if (int.TryParse(roundString, out round))
                        return round;
                }
                return null;
            }
        }

        public string WhitePlayer
        {
            get
            {
                string white;
                if (TagPairs.TryGetValue("White", out white))
                    return white;
                return null;
            }
        }

        public string BlackPlayer
        {
            get
            {
                string black;
                if (TagPairs.TryGetValue("Black", out black))
                    return black;
                return null;
            }
        }

        public GameResult Result
        {
            get
            {
                string result;
                if (TagPairs.TryGetValue("Result", out result))
                {
                    switch (result.Trim())
                    {
                        case "1-0":
                            return GameResult.WhiteWins;
                        case "0-1":
                            return GameResult.BlackWins;
                        case "1/2-1/2":
                            return GameResult.Draw;
                    }
                }
                return GameResult.Unknown;
            }
        }

        public static Game Load(Stream stream)
        {
            var rv = new Game();
            string pgn;
            using (var streamReader = new StreamReader(stream))
            {
                pgn = streamReader.ReadToEnd();
            }

            foreach (Match match in _tagPairsRegex.Matches(pgn))
            {
                string name = match.Groups["Name"].Value;
                string value = match.Groups["Value"].Value;

                rv._tagPairs[name] = value;
            }

            foreach (Match match in _moveRegex.Matches(pgn))
            {
                //These are also available

                //string moveNumber = match.Groups["MoveNumber"].Value;
                //string whiteMove = match.Groups["WhiteMove"].Value;
                //string blackMove = match.Groups["BlackMove"].Value;
                
                Piece whitePiece = GetPieceFromLetter(match.Groups["WhitePiece"].Value, PieceColor.White);
                rv.AddMove(match.Groups["WhiteCastle"].Value, whitePiece, match.Groups["WhiteFrom"].Value, match.Groups["WhiteTo"].Value);

                Piece blackPiece = GetPieceFromLetter(match.Groups["BlackPiece"].Value, PieceColor.Black);                
                rv.AddMove(match.Groups["BlackCastle"].Value, blackPiece, match.Groups["BlackFrom"].Value, match.Groups["BlackTo"].Value);
            }

            //Put the pieces back to the beginning.
            rv._board.ResetMoves();

            return rv;
        }

        private void AddMove(string castle, Piece piece, string fromLocation, string toLocation)
        {
            if (false == string.IsNullOrEmpty(castle))
            {
                if (castle == "O-O")
                    _board.AddMove(Move.GetCastle(PieceColor.White, BoardSide.KingSide));
                else if (castle == "O-O-O")
                    _board.AddMove(Move.GetCastle(PieceColor.White, BoardSide.QueenSide));
                else
                    throw new InvalidOperationException(string.Format("Castle pattern '{0}' is unknown", castle));
            }
            else
            {
                Location targetLocation = Location.Parse(toLocation);

                Location sourceLocation;
                if (false == Location.TryParse(fromLocation, out sourceLocation))
                {
                    sourceLocation = GetSourceLocation(piece, targetLocation, fromLocation);
                }

                if (sourceLocation == null)
                    throw new Exception(string.Format("Could not find the original location '{0}'", fromLocation));

                _board.AddMove(new Move(piece, sourceLocation, targetLocation));
            }
        }

        private Location GetSourceLocation(Piece piece, Location destinationLocation, string fromLocationHint)
        {
            if (piece == null) throw new ArgumentNullException("piece");
            if (destinationLocation == null) throw new ArgumentNullException("destinationLocation");

            switch (piece.Type)
            {
                case PieceType.Pawn:
                    return GetPawnSourceLocation(piece, destinationLocation, fromLocationHint);
                case PieceType.Rook:
                    return GetRookSourceLocation(piece, destinationLocation, fromLocationHint);
                case PieceType.Knight:
                    return GetKnightSourceLocation(piece, destinationLocation, fromLocationHint);
                case PieceType.Bishop:
                    return GetBishopSourceLocation(piece, destinationLocation, fromLocationHint);
                case PieceType.King:
                    return GetKingSourceLocation(piece, destinationLocation);
                case PieceType.Queen:
                    return GetQueenSourceLocation(piece, destinationLocation, fromLocationHint);
            }
            throw new InvalidOperationException();
        }

        private Location GetPawnSourceLocation(Piece pawn, Location destinationLocation, string fromLocationHint)
        {
            //Check if the space was occupied
            var previousPiece = GetPiece(destinationLocation);
            if (previousPiece != null)
            {
                //It was a capture check the diagonals
                //TODO: Validate that both locations do not contain a valid pawn
                var leftLocation = Location.FromOffset(destinationLocation, -1, pawn.Color == PieceColor.White ? -1 : 1);
                if (leftLocation != null && pawn.Equals(GetPiece(leftLocation)) && leftLocation.MatchesHint(fromLocationHint))
                {
                    return leftLocation;
                }
                var rightLocation = Location.FromOffset(destinationLocation, 1, pawn.Color == PieceColor.White ? -1 : 1);
                if (rightLocation != null && pawn.Equals(GetPiece(rightLocation)) && rightLocation.MatchesHint(fromLocationHint))
                {
                    return rightLocation;
                }
            }
            else
            {
                //Not a capture look for a pawn in the same file
                var oneBackLocation = Location.FromOffset(destinationLocation, 0, pawn.Color == PieceColor.White ? -1 : 1);
                if (pawn.Equals(GetPiece(oneBackLocation)))
                    return oneBackLocation;

                //Check if the pawn moved two spaces on its first move
                if (pawn.Color == PieceColor.White && destinationLocation.Rank == 4)
                {
                    var startingLocation = Location.FromOffset(destinationLocation, 0, -2);
                    if (pawn.Equals(GetPiece(startingLocation)))
                        return startingLocation;
                }
                else if (pawn.Color == PieceColor.Black && destinationLocation.Rank == 5)
                {
                    var startingLocation = Location.FromOffset(destinationLocation, 0, 2);
                    if (pawn.Equals(GetPiece(startingLocation)))
                        return startingLocation;
                }
            }
            return null;
        }

        private Location GetRookSourceLocation(Piece rook, Location destinationLocation, string fromLocationHint)
        {
            return
                FindSourceLocationForPieceWithStrategy(rook, destinationLocation, l => Location.FromOffset(l, 0, -1), fromLocationHint) ??
                FindSourceLocationForPieceWithStrategy(rook, destinationLocation, l => Location.FromOffset(l, 0, 1), fromLocationHint) ??
                FindSourceLocationForPieceWithStrategy(rook, destinationLocation, l => Location.FromOffset(l, -1, 0), fromLocationHint) ??
                FindSourceLocationForPieceWithStrategy(rook, destinationLocation, l => Location.FromOffset(l, 1, 0), fromLocationHint);
        }

        private Location GetKnightSourceLocation(Piece knight, Location destinationLocation, string fromLocationHint)
        {
            var location = Location.FromOffset(destinationLocation, 2, 1);
            if (location != null && knight.Equals(GetPiece(location)) && location.MatchesHint(fromLocationHint))
                return location;
            location = Location.FromOffset(destinationLocation, 2, -1);
            if (location != null && knight.Equals(GetPiece(location)) && location.MatchesHint(fromLocationHint))
                return location;
            location = Location.FromOffset(destinationLocation, 1, -2);
            if (location != null && knight.Equals(GetPiece(location)) && location.MatchesHint(fromLocationHint))
                return location;
            location = Location.FromOffset(destinationLocation, -1, -2);
            if (location != null && knight.Equals(GetPiece(location)) && location.MatchesHint(fromLocationHint))
                return location;
            location = Location.FromOffset(destinationLocation, -2, -1);
            if (location != null && knight.Equals(GetPiece(location)) && location.MatchesHint(fromLocationHint))
                return location;
            location = Location.FromOffset(destinationLocation, -2, 1);
            if (location != null && knight.Equals(GetPiece(location)) && location.MatchesHint(fromLocationHint))
                return location;
            location = Location.FromOffset(destinationLocation, -1, 2);
            if (location != null && knight.Equals(GetPiece(location)) && location.MatchesHint(fromLocationHint))
                return location;
            location = Location.FromOffset(destinationLocation, 1, 2);
            if (location != null && knight.Equals(GetPiece(location)) && location.MatchesHint(fromLocationHint))
                return location;
            return null;
        }

        private Location GetBishopSourceLocation(Piece bishop, Location destinationLocation, string fromLocationHint)
        {
            return
                FindSourceLocationForPieceWithStrategy(bishop, destinationLocation, l => Location.FromOffset(l, -1, -1), fromLocationHint) ??
                FindSourceLocationForPieceWithStrategy(bishop, destinationLocation, l => Location.FromOffset(l, -1, 1), fromLocationHint) ??
                FindSourceLocationForPieceWithStrategy(bishop, destinationLocation, l => Location.FromOffset(l, 1, 1), fromLocationHint) ??
                FindSourceLocationForPieceWithStrategy(bishop, destinationLocation, l => Location.FromOffset(l, 1, -1), fromLocationHint);

        }

        private Location GetKingSourceLocation(Piece king, Location destinationLocation)
        {
            var locations = new[]
            {
                Location.FromOffset(destinationLocation, 0, 1),
                Location.FromOffset(destinationLocation, 1, 1),
                Location.FromOffset(destinationLocation, 1, 0),
                Location.FromOffset(destinationLocation, 1, -1),
                Location.FromOffset(destinationLocation, 0, -1),
                Location.FromOffset(destinationLocation, -1, -1),
                Location.FromOffset(destinationLocation, -1, 0),
                Location.FromOffset(destinationLocation, -1, 1)
            };
            return locations.FirstOrDefault(location => king.Equals(GetPiece(location)));
        }

        private Location GetQueenSourceLocation(Piece queen, Location destinationLocation, string fromLocationHint)
        {
            return
                FindSourceLocationForPieceWithStrategy(queen, destinationLocation, l => Location.FromOffset(l, 0, 1), fromLocationHint) ??
                FindSourceLocationForPieceWithStrategy(queen, destinationLocation, l => Location.FromOffset(l, 1, 1), fromLocationHint) ??
                FindSourceLocationForPieceWithStrategy(queen, destinationLocation, l => Location.FromOffset(l, 1, 0), fromLocationHint) ??
                FindSourceLocationForPieceWithStrategy(queen, destinationLocation, l => Location.FromOffset(l, 1, -1), fromLocationHint) ??
                FindSourceLocationForPieceWithStrategy(queen, destinationLocation, l => Location.FromOffset(l, 0, -1), fromLocationHint) ??
                FindSourceLocationForPieceWithStrategy(queen, destinationLocation, l => Location.FromOffset(l, -1, -1), fromLocationHint) ??
                FindSourceLocationForPieceWithStrategy(queen, destinationLocation, l => Location.FromOffset(l, -1, 0), fromLocationHint) ??
                FindSourceLocationForPieceWithStrategy(queen, destinationLocation, l => Location.FromOffset(l, -1, 1), fromLocationHint);
        }

        private Location FindSourceLocationForPieceWithStrategy(Piece piece, Location destinationLocation, Func<Location, Location> strategy, string locationHint)
        {
            for (Location location = strategy(destinationLocation);
                location != null;
                location = strategy(location))
            {
                var foundPiece = GetPiece(location);
                if (foundPiece != null)
                {
                    if (piece.Equals(foundPiece) && location.MatchesHint(locationHint))
                        return location;
                    break;
                }
            }
            return null;
        }


        public Piece GetPiece(Location location)
        {
            //For now it is just the initial starting locations since we have not implemented moving
            return _board.GetPiece(location);
        }

        private static Piece GetPieceFromLetter(string piece, PieceColor color)
        {
            if (color == PieceColor.None) throw new ArgumentException("Piece color must be specified");
            switch (piece)
            {
                case null:
                case "":
                case "P":
                    return new Piece(PieceType.Pawn, color);
                case "N":
                    return new Piece(PieceType.Knight, color);
                case "B":
                    return new Piece(PieceType.Bishop, color);
                case "R":
                    return new Piece(PieceType.Rook, color);
                case "Q":
                    return new Piece(PieceType.Queen, color);
                case "K":
                    return new Piece(PieceType.King, color);
                default:
                    throw new InvalidOperationException(string.Format("Could not find piece for '{0}'", piece));
            }
        }
    }

}