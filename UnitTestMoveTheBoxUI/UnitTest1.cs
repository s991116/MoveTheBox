using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoveTheBoxUI;
using MoveTheBox;

namespace UnitTestMoveTheBoxUI
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var target = new BoardImage();
            var board = new Board(new Box[] {
                new Box(1, new Position(2, 3)),
                new Box(2, new Position(3, 3)),
                new Box(3, new Position(2, 2)),
                new Box(4, new Position(2, 1))
            });

            var image = target.CreateBitmap(board);

            image.Save("Test.bmp");
        }
    }
}
