using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;
using System.Threading;

namespace PGNSharp
{
    public class Game
    {
        private static readonly Regex _tagPairsRegex = new Regex("(?<=\\s*\\[\\s*)(?<Name>\\w*?)(?:\\s+\")(?<Value>.*?)(?=\"\\s*\\])");
        
        private readonly Dictionary<string, string> _tagPairs = new Dictionary<string, string>();

        private Game()
        {

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

            return rv;
        }

        private static void ParseTagPairs(string pgn)
        {
            
        }

        public Piece GetPiece( Location location )
        {
            throw new NotImplementedException();
        }
    }
}