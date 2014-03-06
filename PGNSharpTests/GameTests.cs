using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PGNSharp;

namespace PGNSharpTests
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void TestLoadingGame()
        {
            using (var stream = new MemoryStream())
            {
                //Arrange
                
                //Act
                var game = Game.Load(stream);

                //Assert
                Assert.IsNotNull(game);
            }
        }
    }
}
