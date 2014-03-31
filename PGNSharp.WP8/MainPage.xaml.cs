using System.Windows;
using PGNSharp.Core;
using TestData;

namespace PGNSharp.WP8
{
    public partial class MainPage
    {
        private readonly Game _game;
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            _game = Game.Load(TestGame.PGN);
            Board.SetPieces(_game);
        }

        private void NextMoveOnClick(object sender, RoutedEventArgs e)
        {
            _game.NextMove();
            Board.SetPieces(_game);
        }

        private void ResetOnClick(object sender, RoutedEventArgs e)
        {
            _game.ResetMoves();
            Board.SetPieces(_game);
        }
    }
}