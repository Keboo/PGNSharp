using Android.App;
using Android.Views;
using Android.Widget;
using PGNSharp.Core;

namespace PGNSharp.Driod
{
    public class BoardAdapter : BaseAdapter<string>
    {
        private readonly Game _game;
        private readonly Activity _context;

        public BoardAdapter(Activity context, Game game)
        {
            _context = context;
            _game = game;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
                convertView = _context.LayoutInflater.Inflate(Resource.Layout.board_cell, null);

            var textView = convertView.FindViewById<TextView>(Resource.Id.cellText);
            textView.Text = BoardCharacterFromPosition(position);
            return convertView;
        }

        public override int Count
        {
            get { return 64; }
        }

        public override string this[int position]
        {
            get { return BoardCharacterFromPosition(position); }
        }

        private string BoardCharacterFromPosition(int position)
        {
            var file = (char)('a' + position % 8);
            int rank = position / 8 + 1;
            var piece = _game.GetPiece(new Location(file, rank));
            if (piece == null)
                return "-";
            return AsciiPiece.GetCharForPiece(piece) + " ";
        }
    }
}