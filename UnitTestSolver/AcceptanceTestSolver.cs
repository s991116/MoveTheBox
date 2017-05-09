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
            var board = new BoardTestBuilder()
                .FromFile(@"TestData\AdvancedMoveTheBoxLevel.json")
                .Build();
            var solve = new Solve();

            //Act
            var result = solve.FindSolution(board, 4);

            //Assert
            Assert.IsTrue(result.Found);
        }

        [TestMethod]
        public void FindSolution_SemiAdvancedSetup_SolutionIsFound()
        {
            //Arrange
            var board = new BoardTestBuilder()
                .FromFile(@"TestData\SemiAdvancedSetup.json")
                .Build();
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
            var board = new BoardTestBuilder()
                .FromFile(@"TestData\SeattleLevel1.json")
                .Build();
            var solve = new Solve();

            //Act
            var result = solve.FindSolution(board, 2);

            //Assert
            Assert.IsTrue(result.Found);
        }

        [TestMethod]
        public void FindSolution_MapSeattleLevel3_SolutionIsFound()
        {
            //Arrange
            var board = new BoardTestBuilder()
                .FromFile(@"TestData\SeattleLevel3.json")
                .Build();
            var solve = new Solve();

            //Act
            var result = solve.FindSolution(board, 2);

            //Assert
            Assert.IsTrue(result.Found);
        }

    }
    class BoardTestBuilder
    {
        private List<Box> boxes;

        public BoardTestBuilder()
        {
            boxes = new List<Box>();
        }

        public BoardTestBuilder FromFile(string filename)
        {
            var fs = new FileInfo(filename).OpenRead();
            var sr = new StreamReader(fs);
            var serializedBoxes = sr.ReadToEnd();
            boxes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Box>>(serializedBoxes);

            sr.Close();
            fs.Close();
            return this;
        }

        public Board Build()
        {
            return new Board(boxes);
        }
    }

}