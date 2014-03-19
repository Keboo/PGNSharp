using System;
using Android.App;
using Android.Views;
using Android.Widget;
using PGNSharp.Core;

namespace PGNSharp.Driod.Resources
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
            switch (piece.Type)
            {
                case PieceType.Pawn:
                    return piece.Color == PieceColor.White ? "P " : "p ";
                case PieceType.Rook:
                    return piece.Color == PieceColor.White ? "R " : "r ";
                case PieceType.Knight:
                    return piece.Color == PieceColor.White ? "N " : "n ";
                case PieceType.Bishop:
                    return piece.Color == PieceColor.White ? "B " : "b ";
                case PieceType.Queen:
                    return piece.Color == PieceColor.White ? "Q " : "q ";
                case PieceType.King:
                    return piece.Color == PieceColor.White ? "K " : "k ";
            }
            throw new InvalidOperationException();
        }
    }
}