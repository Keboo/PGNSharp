using System.Collections.Generic;
using System.IO;

namespace PGNSharp
{
    public class Game
    {
        private readonly Dictionary<string, string> _tagPairs = new Dictionary<string, string>();

        private Game()
        {

        }

        //TODO: Make immutable
        public IDictionary<string, string> TagPairs
        {
            get { return _tagPairs; }
        }

        public static Game Load(Stream stream)
        {
            return null;
        }
    }
}