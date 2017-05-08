using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoveTheBox;
using Solver;

namespace UnitTestSolver
{
    [TestClass]
    public class AcceptanceTestSolver
    {
        [TestMethod]
        public void AdvancedMoveTheBoxLevel()
        {
            //Arrange
            var box1_oldbag = new Box(1, new Position(2, 5));
            var box2_oldbag = new Box(1, new Position(3, 4));
            var box3_oldbag = new Box(1, new Position(4, 3));

            var box1_packet = new Box(2, new Position(2, 4));
            var box2_packet = new Box(2, new Position(3, 0));
            var box3_packet = new Box(2, new Position(4, 4));

            var box1_metal = new Box(3, new Position(2, 3));
            var box2_metal = new Box(3, new Position(3, 3));
            var box3_metal = new Box(3, new Position(4, 2));

            var box1_wood = new Box(4, new Position(2, 2));
            var box2_wood = new Box(4, new Position(3, 2));
            var box3_wood = new Box(4, new Position(4, 1));
            var box4_wood = new Box(4, new Position(4, 0));
            var box5_wood = new Box(4, new Position(6, 0));
            var box6_wood = new Box(4, new Position(7, 0));

            var box1_red = new Box(5, new Position(2, 0));
            var box2_red = new Box(5, new Position(2, 1));
            var box3_red = new Box(5, new Position(3, 1));

            var board = new Board(new Box[] { box1_oldbag, box2_oldbag, box3_oldbag, box1_packet, box2_packet, box3_packet, box1_metal, box2_metal, box3_metal,
                box1_wood, box2_wood, box3_wood, box4_wood, box5_wood, box6_wood, box1_red, box2_red, box3_red});
            var solve = new Solve();

            //Act
            var solution = solve.FindSolution(board, 4);

            //Assert
            Assert.IsTrue(solution.Found);
        }

        [TestMethod]
        public void FindSolution_SemiAdvancedSetup_SolutionIsFound()
        {
            //Arrange
            var box1_red = new Box(0, new Position(2, 0));
            var box2_red = new Box(0, new Position(3, 0));
            var box3_red = new Box(0, new Position(5, 1));

            var box1_blue = new Box(1, new Position(5, 0));
            var box2_blue = new Box(1, new Position(7, 0));
            var box3_blue = new Box(1, new Position(8, 0));

            var board = new Board(new Box[] { box1_red, box2_red, box3_red, box1_blue, box2_blue, box3_blue });
            var solve = new Solve();

            //Act
            var result = solve.FindSolution(board, 2);

            //Assert
            Assert.IsTrue(result.Found);
        }
        [TestMethod]
        public void FindSolution_MapSeattleLevel1_SolutionIsFound()
        {
            //Arrange
            var board = new TestFixture().FromFile(@"c:\users\Philip Juhl\Downloads\SeattleLevel1");

            //Act

            //Assert
        }


    }
    class TestFixture
    {
        private List<Box> boxes;

        public TestFixture()
        {
            boxes = new List<Box>();
        }

        public TestFixture FromFile(string filename)
        {
            var fs = new FileInfo(filename).OpenRead();
            var sr = new StreamReader(fs);
            var serializedBoxes = sr.ReadToEnd();
            //var boxes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Box>>(serializedBoxes);

            sr.Close();
            fs.Close();
            return this;
        }

        public Board CreateTarget()
        {
            return new Board(boxes);
        }
    }

}