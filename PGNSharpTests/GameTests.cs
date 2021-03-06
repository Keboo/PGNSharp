﻿using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PGNSharp.Core.Tests
{
    [TestClass]
    public class GameTests
    {
        private static readonly string PGN = string.Format(@"
[Event {0}F/S Return Match{0}][Site {0}Belgrade, Serbia JUG{0}]     [    Date {0}1992.11.04{0}   ]   
  [ Round {0}29{0} ] 


[  White          {0}Fischer, Robert J.{0}  ]

[
    Black    
{0}Spassky, Boris V.{0}
]
[Result {0}1/2-1/2{0}]
[ Things {0}Thing 1:Thing 2{0}]

1. e4 e5 2. Nf3 Nc6 3. Bb5 a6 4. Ba4 Nf6 5. O-O Be7 6. Re1 b5 7. Bb3 d6 8. c3
O-O 9. h3 Nb8 10. d4 Nbd7 11. c4 c6 12. cxb5 axb5 13. Nc3 Bb7 14. Bg5 b4 15. 
Nb1 h6 16. Bh4 c5 17. dxe5 Nxe4 18. Bxe7 Qxe7 19. exd6 Qf6 20. Nbd2 Nxd6 21.
Nc4 Nxc4 22. Bxc4 Nb6 23. Ne5 Rae8 24. Bxf7+ Rxf7 25. Nxf7 Rxe1+ 26. Qxe1 Kxf7
27. Qe3 Qg5 28. Qxg5 hxg5 29. b3 Ke6 30. a3 Kd6 31. axb4 cxb4 32. Ra5 Nd5 33.
f3 Bc8 34. Kf2 Bf5 35. Ra7 g6 36. Ra6+ Kc5 37. Ke1 Nf4 38. g3 Nxh3 39. Kd2 Kb5
40. Rd6 Kc5 41. Ra6 Nf2 42. g4 Bd3 43. Re6 1/2-1/2
", "\"");

        [TestMethod]
        public void TestLoadingTagPairs()
        {
            Game game = LoadGame();

            Assert.IsNotNull(game);

            Assert.AreEqual(@"F/S Return Match", game.TagPairs["Event"]);
            Assert.AreEqual(@"F/S Return Match", game.Event);
            Assert.AreEqual(@"Belgrade, Serbia JUG", game.TagPairs["Site"]);
            Assert.AreEqual(@"Belgrade, Serbia JUG", game.Site);
            Assert.AreEqual(@"1992.11.04", game.TagPairs["Date"]);
            Assert.AreEqual(new DateTime(1992, 11, 4), game.Date);
            Assert.AreEqual(@"29", game.TagPairs["Round"]);
            Assert.AreEqual(29, game.Round);
            Assert.AreEqual(@"Fischer, Robert J.", game.TagPairs["White"]);
            Assert.AreEqual(@"Fischer, Robert J.", game.WhitePlayer);
            Assert.AreEqual(@"Spassky, Boris V.", game.TagPairs["Black"]);
            Assert.AreEqual(@"Spassky, Boris V.", game.BlackPlayer);
            Assert.AreEqual(@"1/2-1/2", game.TagPairs["Result"]);
            Assert.AreEqual(GameResult.Draw, game.Result);
            Assert.AreEqual(@"Thing 1:Thing 2", game.TagPairs["Things"]);
        }

        [TestMethod]
        public void PiecesStartInTheCorrectLocations()
        {
            Game game = LoadGame();

            Assert.AreEqual(Piece.WhitePawn, game.GetPiece(Location.A2));
            Assert.AreEqual(Piece.WhitePawn, game.GetPiece(Location.B2));
            Assert.AreEqual(Piece.WhitePawn, game.GetPiece(Location.C2));
            Assert.AreEqual(Piece.WhitePawn, game.GetPiece(Location.D2));
            Assert.AreEqual(Piece.WhitePawn, game.GetPiece(Location.E2));
            Assert.AreEqual(Piece.WhitePawn, game.GetPiece(Location.F2));
            Assert.AreEqual(Piece.WhitePawn, game.GetPiece(Location.G2));
            Assert.AreEqual(Piece.WhitePawn, game.GetPiece(Location.H2));
            Assert.AreEqual(Piece.WhiteRook, game.GetPiece(Location.A1));
            Assert.AreEqual(Piece.WhiteKnight, game.GetPiece(Location.B1));
            Assert.AreEqual(Piece.WhiteBishop, game.GetPiece(Location.C1));
            Assert.AreEqual(Piece.WhiteQueen, game.GetPiece(Location.D1));
            Assert.AreEqual(Piece.WhiteKing, game.GetPiece(Location.E1));
            Assert.AreEqual(Piece.WhiteBishop, game.GetPiece(Location.F1));
            Assert.AreEqual(Piece.WhiteKnight, game.GetPiece(Location.G1));
            Assert.AreEqual(Piece.WhiteRook, game.GetPiece(Location.H1));

            Assert.AreEqual(Piece.BlackPawn, game.GetPiece(Location.A7));
            Assert.AreEqual(Piece.BlackPawn, game.GetPiece(Location.B7));
            Assert.AreEqual(Piece.BlackPawn, game.GetPiece(Location.C7));
            Assert.AreEqual(Piece.BlackPawn, game.GetPiece(Location.D7));
            Assert.AreEqual(Piece.BlackPawn, game.GetPiece(Location.E7));
            Assert.AreEqual(Piece.BlackPawn, game.GetPiece(Location.F7));
            Assert.AreEqual(Piece.BlackPawn, game.GetPiece(Location.G7));
            Assert.AreEqual(Piece.BlackPawn, game.GetPiece(Location.H7));
            Assert.AreEqual(Piece.BlackRook, game.GetPiece(Location.A8));
            Assert.AreEqual(Piece.BlackKnight, game.GetPiece(Location.B8));
            Assert.AreEqual(Piece.BlackBishop, game.GetPiece(Location.C8));
            Assert.AreEqual(Piece.BlackQueen, game.GetPiece(Location.D8));
            Assert.AreEqual(Piece.BlackKing, game.GetPiece(Location.E8));
            Assert.AreEqual(Piece.BlackBishop, game.GetPiece(Location.F8));
            Assert.AreEqual(Piece.BlackKnight, game.GetPiece(Location.G8));
            Assert.AreEqual(Piece.BlackRook, game.GetPiece(Location.H8));
        }

        [TestMethod]
        public void MovesAdjustPiecesToTheCorrectLocations()
        {
            /*
             * 1. e4 e5 
             * 2. Nf3 Nc6 
             * 3. Bb5 a6 
             * 4. Ba4 Nf6 
             * 5. O-O Be7 
             * 6. Re1 b5 
             * 7. Bb3 d6 
             * 8. c3 O-O 
             * 9. h3 Nb8 
             * 10. d4 Nbd7 
             * 11. c4 c6 
             * 12. cxb5 axb5 
             * 13. Nc3 Bb7 
             * 14. Bg5 b4 
             * 15. Nb1 h6 
             * 16. Bh4 c5 
             * 17. dxe5 Nxe4 
             * 18. Bxe7 Qxe7 
             * 19. exd6 Qf6 
             * 20. Nbd2 Nxd6 
             * 21. Nc4 Nxc4 
             * 22. Bxc4 Nb6 
             * 23. Ne5 Rae8 
             * 24. Bxf7+ Rxf7 
             * 25. Nxf7 Rxe1+ 
             * 26. Qxe1 Kxf7 
             * 27. Qe3 Qg5 
             * 28. Qxg5 hxg5 
             * 29. b3 Ke6 
             * 30. a3 Kd6 
             * 31. axb4 cxb4 
             * 32. Ra5 Nd5 
             * 33. f3 Bc8 
             * 34. Kf2 Bf5 
             * 35. Ra7 g6 
             * 36. Ra6+ Kc5 
             * 37. Ke1 Nf4 
             * 38. g3 Nxh3 
             * 39. Kd2 Kb5
             * 40. Rd6 Kc5 
             * 41. Ra6 Nf2 
             * 42. g4 Bd3 
             * 43. Re6 1/2-1/2
             */

            Game game = LoadGame();

            Assert.IsNotNull(game);

            game.NextMove();
            Assert.AreEqual(null, game.GetPiece(Location.E2));
            Assert.AreEqual(Piece.WhitePawn, game.GetPiece(Location.E4));
            game.NextMove();
            Assert.AreEqual(null, game.GetPiece(Location.E7));
            Assert.AreEqual(Piece.BlackPawn, game.GetPiece(Location.E5));

            game.NextMove();
            Assert.AreEqual(null, game.GetPiece(Location.G1));
            Assert.AreEqual(Piece.WhiteKnight, game.GetPiece(Location.F3));
            game.NextMove();
            Assert.AreEqual(null, game.GetPiece(Location.B8));
            Assert.AreEqual(Piece.BlackKnight, game.GetPiece(Location.C6));

            for (int i = 0; i <= 38; i++)
            {
                game.NextMove();
                game.NextMove();
            }
            //Now at move 42

            game.NextMove();
            Assert.AreEqual(Piece.WhiteRook, game.GetPiece(Location.A6));
            Assert.AreEqual(Piece.WhitePawn, game.GetPiece(Location.B3));
            Assert.AreEqual(Piece.BlackPawn, game.GetPiece(Location.B4));
            Assert.AreEqual(Piece.BlackKing, game.GetPiece(Location.C5));
            Assert.AreEqual(Piece.WhiteKing, game.GetPiece(Location.D2));
            Assert.AreEqual(Piece.BlackKnight, game.GetPiece(Location.F2));
            Assert.AreEqual(Piece.WhitePawn, game.GetPiece(Location.F3));
            Assert.AreEqual(Piece.BlackBishop, game.GetPiece(Location.F5));
            Assert.AreEqual(Piece.WhitePawn, game.GetPiece(Location.G4));
            Assert.AreEqual(Piece.BlackPawn, game.GetPiece(Location.G5));
            Assert.AreEqual(Piece.BlackPawn, game.GetPiece(Location.G6));
            game.NextMove();
            Assert.AreEqual(null, game.GetPiece(Location.F5));
            Assert.AreEqual(Piece.BlackBishop, game.GetPiece(Location.D3));
            game.NextMove();
            Assert.AreEqual(null, game.GetPiece(Location.A6));
            Assert.AreEqual(Piece.WhiteRook, game.GetPiece(Location.E6));
        }

        private static Game LoadGame()
        {
            Game game;
            using (var stream = new MemoryStream())
            using (var sw = new StreamWriter(stream) { AutoFlush = true })
            {
                sw.Write(PGN);
                stream.Position = 0;

                game = Game.Load(stream);
            }
            return game;
        }
    }
}
