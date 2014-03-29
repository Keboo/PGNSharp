using System;
using System.IO;
using System.Text;
using Android.App;
using Android.Widget;
using Android.OS;
using PGNSharp.Core;
using TestData;

namespace PGNSharp.Driod
{
    [Activity(Label = "PGNSharp.Driod", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        private Game _game;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.board);

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(TestGame.PGN)))
            {
                _game = Game.Load(stream);
            }

            SetPieces();

            //Wire up button click handles
            var nextMove = FindViewById<Button>(Resource.Id.btnNextMove);
            nextMove.Click += NextMoveOnClick;

            var previousMove = FindViewById<Button>(Resource.Id.btnPreviousMove);
            previousMove.Click += PreviousMoveOnClick;


            var resetBoard = FindViewById<Button>(Resource.Id.btnResetBoard);
            resetBoard.Click += ResetBoardOnClick;
        }

        private void ResetBoardOnClick(object sender, EventArgs eventArgs)
        {
            _game.ResetMoves();
            SetPieces();
        }

        private void PreviousMoveOnClick(object sender, EventArgs eventArgs)
        {
            Toast.MakeText(this, "Previous not implemented", ToastLength.Short).Show();
        }

        private void NextMoveOnClick(object sender, EventArgs eventArgs)
        {
            _game.NextMove();
            SetPieces();
        }

        private void SetPieces()
        {
            var table = FindViewById<TableLayout>(Resource.Id.tableBoard);
            for (int rank = 1; rank <= 8; rank++)
            {
                //Need to reverse the rank so that a1 is in the bottom left
                var row = (TableRow)table.GetChildAt(8 - rank);
                for (char file = 'a'; file <= 'h'; file++)
                {
                    var cell = (TextView) row.GetChildAt(file - 'a');
                    Piece piece = _game.GetPiece(new Location(file, rank));
                    cell.Text = piece != null ? AsciiPiece.GetCharForPiece(piece).ToString() : "";
                }
            }

        }
    }
}

