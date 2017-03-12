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
            Assert.AreEqual(Position.Create(5, 2), Position.Create(5, 2));
            Assert.AreNotEqual(Position.Create(4, 1), Position.Create(5, 2));
        }

        [TestMethod]
        public void MoveLeft_SingleBox_BoxMoved()
        {
            //Arrange
            var boxStartPosition = Position.Create(3, 0);
            var boxEndPosition = Position.Create(2, 0);
            var boxAtStart = Box.Create(1, boxStartPosition);
            var board = Board.Create(new Box[] {boxAtStart });

            //Act
            board.TryMoveLeft(boxStartPosition);

            //Assert
            Assert.IsTrue(board.IsBoxAtPosition(boxEndPosition));
        }

        [TestMethod]
        public void MoveLeft_TwoBoxes_BoxPositionsAreSwitched()
        {
            //Arrange
            var box1 = Box.Create(1,Position.Create(1, 0));
            var box2 = Box.Create(2,Position.Create(2, 0));
            var board = Board.Create(new Box[] { box1, box2 });

            //Act
            board.TryMoveRight(box1.Position);

            //Assert
            Assert.AreEqual(board.GetBoxAtPosition(Position.Create(1,0)), box2);
        }

        [TestMethod]
        public void MoveRight_SingleBox_BoxMoved()
        {
            //Arrange
            var boxStartPosition = Position.Create(3, 0);
            var boxEndPosition = Position.Create(4, 0);
            var boxAtStart = Box.Create(1, boxStartPosition);
            var board = Board.Create(new Box[] { boxAtStart });

            //Act
            board.TryMoveRight(boxStartPosition);

            //Assert
            Assert.IsTrue(board.IsBoxAtPosition(boxEndPosition));
        }

        [TestMethod]
        public void MoveBox_BoxOnTop_BoxFallsDown()
        {
            //Arrange
            var boxDown = Box.Create(1, Position.Create(2, 0));
            var boxTop  = Box.Create(1, Position.Create(2, 1));

            var board = Board.Create(new Box[] { boxDown, boxTop });

            //Act
            board.TryMoveLeft(boxTop.Position);

            //Assert
            Assert.IsTrue(board.IsBoxAtPosition(Position.Create(1,0)));
        }

        [TestMethod]
        public void MoveBox_BoxOnTopOfTower_BoxFallsDown()
        {
            //Arrange
            var boxDown0 = Box.Create(1, Position.Create(2, 0));
            var boxDown1 = Box.Create(1, Position.Create(2, 1));
            var boxDown2 = Box.Create(1, Position.Create(2, 2));
            var boxDown3 = Box.Create(1, Position.Create(2, 3));

            var boxTop = Box.Create(1, Position.Create(2, 4));

            var board = Board.Create(new Box[] { boxDown0, boxDown1, boxDown2, boxDown3, boxTop });

            //Act
            board.TryMoveLeft(boxTop.Position);

            //Assert
            Assert.IsTrue(board.IsBoxAtPosition(Position.Create(1, 0)));
        }

        [TestMethod]
        public void MoveBox_BoxOnBottomOfTower_BoxFallsDown()
        {
            //Arrange
            var boxTop0 = Box.Create(1, Position.Create(2, 0));
            var boxTop1 = Box.Create(2, Position.Create(2, 1));
            var boxTop2 = Box.Create(3, Position.Create(2, 2));
            var boxTop3 = Box.Create(1, Position.Create(2, 3));

            var boxBottom = Box.Create(1, Position.Create(2, 4));

            var board = Board.Create(new Box[] { boxTop0, boxTop1, boxTop2, boxTop3, boxBottom });

            //Act
            board.TryMoveLeft(boxBottom.Position);

            //Assert
            Assert.IsTrue(board.IsBoxAtPosition(Position.Create(1, 0)));
            Assert.IsTrue(board.IsBoxAtPosition(Position.Create(2, 0)));
            Assert.IsTrue(board.IsBoxAtPosition(Position.Create(2, 1)));
            Assert.IsTrue(board.IsBoxAtPosition(Position.Create(2, 2)));
        }

        [TestMethod]
        public void MoveBox_ThreeInARowOfSameKind_BoxesDisapeer()
        {
            var box1 = Box.Create(1, Position.Create(1, 0));
            var box2 = Box.Create(1, Position.Create(2, 0));
            var box3 = Box.Create(1, Position.Create(4, 0));

            var board = Board.Create(new Box[] { box1, box2, box3});

            //Arrange
            board.TryMoveLeft(box3.Position);

            //Assert
            Assert.IsTrue(board.IsEmpty());
        }

        [TestMethod]
        public void MoveBox_FourInARowOfSameKind_BoxesDisapeer()
        {
            var box1 = Box.Create(1, Position.Create(1, 0));
            var box2 = Box.Create(1, Position.Create(2, 0));
            var box3 = Box.Create(1, Position.Create(3, 0));
            var box4 = Box.Create(1, Position.Create(5, 0));

            var board = Board.Create(new Box[] { box1, box2, box3, box4 });

            //Arrange
            board.TryMoveLeft(box4.Position);

            //Assert
            Assert.IsTrue(board.IsEmpty());
        }

        [TestMethod]
        public void MoveBox_FourInARowOfDifferentKind_BoxesDoNotDisapeer()
        {
            var box1 = Box.Create(1, Position.Create(1, 0));
            var box2 = Box.Create(2, Position.Create(2, 0));
            var box3 = Box.Create(1, Position.Create(3, 0));
            var box4 = Box.Create(1, Position.Create(5, 0));

            var board = Board.Create(new Box[] { box1, box2, box3, box4 });

            //Arrange
            board.TryMoveLeft(box4.Position);

            //Assert
            Assert.IsFalse(board.IsEmpty());
        }

        [TestMethod]
        public void MoveBox_ThreeInARowVertical_BoxesDisapeer()
        {
            var box1 = Box.Create(1, Position.Create(1, 0));
            var box2 = Box.Create(1, Position.Create(1, 1));
            var box3 = Box.Create(2, Position.Create(2, 0));
            var box4 = Box.Create(2, Position.Create(2, 1));
            var box5 = Box.Create(1, Position.Create(2, 2));
            var box6 = Box.Create(2, Position.Create(2, 3));

            var board = Board.Create(new Box[] { box1, box2, box3, box4, box5, box6 });

            //Arrange
            board.TryMoveLeft(box5.Position);

            //Assert
            Assert.IsTrue(board.IsEmpty());
        }

        [TestMethod]
        public void MoveBox_ManyBoxesIsAllignAfterMoved_BoxesDisapeer()
        {
            var box1 = Box.Create(1, Position.Create(1, 0));
            var box2 = Box.Create(1, Position.Create(1, 1));
            var box3 = Box.Create(2, Position.Create(2, 0));
            var box4 = Box.Create(2, Position.Create(2, 1));
            var box5 = Box.Create(1, Position.Create(2, 2));
            var box6 = Box.Create(2, Position.Create(2, 3));
            var box7 = Box.Create(3, Position.Create(2, 4));
            var box8 = Box.Create(3, Position.Create(3, 0));
            var box9 = Box.Create(3, Position.Create(4, 0));

            var board = Board.Create(new Box[] { box1, box2, box3, box4, box5, box6, box7, box8, box9 });

            //Arrange
            board.TryMoveLeft(box5.Position);

            //Assert
            Assert.IsTrue(board.IsEmpty());
        }

        public void MoveBox_MoveBoxUpNoBoxOnTop_NoDifferent()
        {
            //Arrange
            var box = Box.Create(1, Position.Create(1, 0));
            var board = Board.Create(new Box[] { box });

            //Act
            board.TryMoveUp(box.Position);

            //Assert
            Assert.AreEqual(Position.Create(1, 0), box.Position);
        }

        [TestMethod]
        public void MoveUp_NoBoxOnTop_NoDifferent()
        {
            //Arrange
            var box = Box.Create(1, Position.Create(1, 0));
            var board = Board.Create(new Box[] { box });

            //Act
            board.TryMoveUp(box.Position);

            //Assert
            Assert.AreEqual(Position.Create(1, 0), box.Position);
        }

        [TestMethod]
        public void MoveUp_BoxOnTop_BoxesAreSwitched()
        {
            //Arrange
            var box1 = Box.Create(1, Position.Create(1, 0));
            var box2 = Box.Create(2, Position.Create(1, 1));

            var board = Board.Create(new Box[] { box1, box2 });

            //Act
            board.TryMoveUp(box1.Position);

            //Assert
            Assert.AreEqual(Position.Create(1, 0), box2.Position);
            Assert.AreEqual(Position.Create(1, 1), box1.Position);
        }
    }
}
