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
            var board = Board.Create(new Box[] {
                Box.Create(1, Position.Create(2, 3)),
                Box.Create(2, Position.Create(3, 3)),
                Box.Create(3, Position.Create(2, 2)),
                Box.Create(4, Position.Create(2, 1))
            });

            var image = target.CreateBitmap(board);

            image.Save("Test.bmp");
        }
    }
}
