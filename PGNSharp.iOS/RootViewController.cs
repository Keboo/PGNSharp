using System;
using MonoTouch.UIKit;
using PGNSharp.Core;

namespace PGNSharp.iOS
{
	public class RootViewController : UIViewController
	{
		private readonly UITextView[,] _board = new UITextView[8,8];
		private readonly Game _game;

		public RootViewController ()
		{
			_game = Game.Load (TestData.TestGame.PGN);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			var board = GetBoard ();
			View.Add (board);
			View.BackgroundColor = UIColor.Gray;
			UIButton nextMoveButton = new UIButton (UIButtonType.RoundedRect);
			var a1 = _board [0, 0];
			nextMoveButton.Frame = new System.Drawing.RectangleF (a1.Frame.Left, a1.Frame.Bottom + 20, 100, 20);
			nextMoveButton.SetTitle("Next", UIControlState.Normal);
			nextMoveButton.AddTarget ((s, e) => { _game.NextMove(); SetPieces();}, UIControlEvent.TouchUpInside);
			View.Add (nextMoveButton);

			UIButton resetButton = new UIButton (UIButtonType.RoundedRect);
			resetButton.Frame = new System.Drawing.RectangleF (nextMoveButton.Frame.Left, nextMoveButton.Frame.Bottom + 20, 100, 20);
			resetButton.SetTitle("Reset", UIControlState.Normal);
			resetButton.AddTarget ((s, e) => {_game.ResetMoves(); SetPieces();}, UIControlEvent.TouchUpInside);
			View.Add (resetButton);

			SetPieces ();
		}

		private void SetPieces()
		{
			for (int rank = 1; rank <= 8; rank++) {
				for(char file = 'a'; file <= 'h'; file++){
					Piece piece = _game.GetPiece (new Location (file, rank));
					if (piece != null) {
						_board [file - 'a', rank - 1].Text = AsciiPiece.GetCharForPiece (piece).ToString ();
					} else {
						_board [file - 'a', rank - 1].Text = "";
					}
				}
			}
		}

		private UIView GetBoard()
		{
			var board = new UIView ();
			board.BackgroundColor = UIColor.DarkGray;

			const int cellSize = 30;

			for (int reversedRank = 0; reversedRank < 8; reversedRank++) {
				bool white = reversedRank % 2 == 1;
				for (int file = 0; file < 8; file++) {
					var cell = new UITextView ();
					cell.BackgroundColor = (white = !white) ? UIColor.White : UIColor.Brown;
					cell.Frame = new System.Drawing.RectangleF (file * cellSize + file, reversedRank * cellSize + reversedRank, cellSize, cellSize);
					cell.TextAlignment = UITextAlignment.Center;
					board.Add (cell);
					//a8 is top left, make adjustments so the board locations line up correctly
					int rank = 7 - reversedRank;
					_board [file, rank] = cell;
				}
			}

			return board;
		}
	}
}

