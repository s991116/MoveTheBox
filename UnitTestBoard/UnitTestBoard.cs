using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoveTheBox;

namespace UnitTestBoard
{
    [TestClass]
    public class UnitTestBoard
    {
        [TestMethod]
        public void Position_Equals()
        {
            Assert.AreEqual(new Position(5, 2), new Position(5, 2));
            Assert.AreNotEqual(new Position(4, 1), new Position(5, 2));
        }

        [TestMethod]
        public void MoveLeft_SingleBox_BoxMoved()
        {
            //Arrange
            var boxStartPosition = new Position(3, 0);
            var boxEndPosition = new Position(2, 0);
            var boxAtStart = new Box(1, boxStartPosition);
            var board = new Board(new Box[] {boxAtStart });

            //Act
            board.TryMoveLeft(boxStartPosition);

            //Assert
            Assert.IsTrue(board.IsBoxAtPosition(boxEndPosition));
        }

        [TestMethod]
        public void MoveLeft_TwoBoxes_BoxPositionsAreSwitched()
        {
            //Arrange
            var box1 = new Box(1,new Position(1, 0));
            var box2 = new Box(2,new Position(2, 0));
            var board = new Board(new Box[] { box1, box2 });

            //Act
            board.TryMoveRight(box1.Position);

            //Assert
            Assert.AreEqual(board.GetBoxAtPosition(new Position(1,0)), box2);
        }

        [TestMethod]
        public void MoveRight_SingleBox_BoxMoved()
        {
            //Arrange
            var boxStartPosition = new Position(3, 0);
            var boxEndPosition = new Position(4, 0);
            var boxAtStart = new Box(1, boxStartPosition);
            var board = new Board(new Box[] { boxAtStart });

            //Act
            board.TryMoveRight(boxStartPosition);

            //Assert
            Assert.IsTrue(board.IsBoxAtPosition(boxEndPosition));
        }

        [TestMethod]
        public void MoveBox_BoxOnTop_BoxFallsDown()
        {
            //Arrange
            var boxDown = new Box(1, new Position(2, 0));
            var boxTop  = new Box(1, new Position(2, 1));

            var board = new Board(new Box[] { boxDown, boxTop });

            //Act
            board.TryMoveLeft(boxTop.Position);

            //Assert
            Assert.IsTrue(board.IsBoxAtPosition(new Position(1,0)));
        }

        [TestMethod]
        public void MoveBox_BoxOnTopOfTower_BoxFallsDown()
        {
            //Arrange
            var boxDown0 = new Box(1, new Position(2, 0));
            var boxDown1 = new Box(1, new Position(2, 1));
            var boxDown2 = new Box(1, new Position(2, 2));
            var boxDown3 = new Box(1, new Position(2, 3));

            var boxTop = new Box(1, new Position(2, 4));

            var board = new Board(new Box[] { boxDown0, boxDown1, boxDown2, boxDown3, boxTop });

            //Act
            board.TryMoveLeft(boxTop.Position);

            //Assert
            Assert.IsTrue(board.IsBoxAtPosition(new Position(1, 0)));
        }

        [TestMethod]
        public void MoveBox_BoxOnBottomOfTower_BoxFallsDown()
        {
            //Arrange
            var boxTop0 = new Box(1, new Position(2, 0));
            var boxTop1 = new Box(2, new Position(2, 1));
            var boxTop2 = new Box(3, new Position(2, 2));
            var boxTop3 = new Box(1, new Position(2, 3));

            var boxBottom = new Box(1, new Position(2, 4));

            var board = new Board(new Box[] { boxTop0, boxTop1, boxTop2, boxTop3, boxBottom });

            //Act
            board.TryMoveLeft(boxBottom.Position);

            //Assert
            Assert.IsTrue(board.IsBoxAtPosition(new Position(1, 0)));
            Assert.IsTrue(board.IsBoxAtPosition(new Position(2, 0)));
            Assert.IsTrue(board.IsBoxAtPosition(new Position(2, 1)));
            Assert.IsTrue(board.IsBoxAtPosition(new Position(2, 2)));
        }

        [TestMethod]
        public void MoveBox_ThreeInARowOfSameKind_BoxesDisapeer()
        {
            var box1 = new Box(1, new Position(1, 0));
            var box2 = new Box(1, new Position(2, 0));
            var box3 = new Box(1, new Position(4, 0));

            var board = new Board(new Box[] { box1, box2, box3});

            //Arrange
            board.TryMoveLeft(box3.Position);

            //Assert
            Assert.IsTrue(board.IsEmpty());
        }

        [TestMethod]
        public void MoveBox_FourInARowOfSameKind_BoxesDisapeer()
        {
            var box1 = new Box(1, new Position(1, 0));
            var box2 = new Box(1, new Position(2, 0));
            var box3 = new Box(1, new Position(3, 0));
            var box4 = new Box(1, new Position(5, 0));

            var board = new Board(new Box[] { box1, box2, box3, box4 });

            //Arrange
            board.TryMoveLeft(box4.Position);

            //Assert
            Assert.IsTrue(board.IsEmpty());
        }

        [TestMethod]
        public void MoveBox_FourInARowOfDifferentKind_BoxesDoNotDisapeer()
        {
            var box1 = new Box(1, new Position(1, 0));
            var box2 = new Box(2, new Position(2, 0));
            var box3 = new Box(1, new Position(3, 0));
            var box4 = new Box(1, new Position(5, 0));

            var board = new Board(new Box[] { box1, box2, box3, box4 });

            //Arrange
            board.TryMoveLeft(box4.Position);

            //Assert
            Assert.IsFalse(board.IsEmpty());
        }

        [TestMethod]
        public void MoveBox_ThreeInARowVertical_BoxesDisapeer()
        {
            var box1 = new Box(1, new Position(1, 0));
            var box2 = new Box(1, new Position(1, 1));
            var box3 = new Box(2, new Position(2, 0));
            var box4 = new Box(2, new Position(2, 1));
            var box5 = new Box(1, new Position(2, 2));
            var box6 = new Box(2, new Position(2, 3));

            var board = new Board(new Box[] { box1, box2, box3, box4, box5, box6 });

            //Arrange
            board.TryMoveLeft(box5.Position);

            //Assert
            Assert.IsTrue(board.IsEmpty());
        }

        [TestMethod]
        public void MoveBox_ManyBoxesIsAllignAfterMoved_BoxesDisapeer()
        {
            var box1 = new Box(1, new Position(1, 0));
            var box2 = new Box(1, new Position(1, 1));
            var box3 = new Box(2, new Position(2, 0));
            var box4 = new Box(2, new Position(2, 1));
            var box5 = new Box(1, new Position(2, 2));
            var box6 = new Box(2, new Position(2, 3));
            var box7 = new Box(3, new Position(2, 4));
            var box8 = new Box(3, new Position(3, 0));
            var box9 = new Box(3, new Position(4, 0));

            var board = new Board(new Box[] { box1, box2, box3, box4, box5, box6, box7, box8, box9 });

            //Arrange
            board.TryMoveLeft(box5.Position);

            //Assert
            Assert.IsTrue(board.IsEmpty());
        }

        public void MoveBox_MoveBoxUpNoBoxOnTop_NoDifferent()
        {
            //Arrange
            var box = new Box(1, new Position(1, 0));
            var board = new Board(new Box[] { box });

            //Act
            board.TryMoveUp(box.Position);

            //Assert
            Assert.AreEqual(new Position(1, 0), box.Position);
        }

        [TestMethod]
        public void MoveUp_NoBoxOnTop_NoDifferent()
        {
            //Arrange
            var box = new Box(1, new Position(1, 0));
            var board = new Board(new Box[] { box });

            //Act
            board.TryMoveUp(box.Position);

            //Assert
            Assert.AreEqual(new Position(1, 0), box.Position);
        }

        [TestMethod]
        public void MoveUp_BoxOnTop_BoxesAreSwitched()
        {
            //Arrange
            var box1 = new Box(1, new Position(1, 0));
            var box2 = new Box(2, new Position(1, 1));

            var board = new Board(new Box[] { box1, box2 });

            //Act
            board.TryMoveUp(box1.Position);

            //Assert
            Assert.AreEqual(new Position(1, 0), box2.Position);
            Assert.AreEqual(new Position(1, 1), box1.Position);
        }
    }
}
