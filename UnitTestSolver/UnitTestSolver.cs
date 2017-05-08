using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoveTheBox;
using Solver;
using System.Collections.Generic;

namespace UnitTestSolver
{
    [TestClass]
    public class UnitTestSolver
    {
        [TestMethod]
        public void Solve_EmptyBoard_SolutionIsEmpty()
        {
            var board = new Board(new Box[] { });
            var solver = new Solve();

            var solution = solver.FindSolution(board,1);

            Assert.IsTrue(solution.Moves.Count == 0);
        }

        [TestMethod]
        public void Solve_SimpleMoveLeft_SolutionIsFound()
        {
            var box1 = new Box(1, new Position(1, 0));
            var box2 = new Box(1, new Position(2, 0));
            var box3 = new Box(1, new Position(4, 0));
            var board = new Board(new Box[] {box1, box2, box3 });
            var solver = new Solve();

            var solution = solver.FindSolution(board,1);

            Assert.IsTrue(solution.Moves.Count == 1);
            Assert.AreEqual(Move.Create(new Position(4,0), Move.MoveDirectionEnum.Left), solution.Moves[0]);
        }

        [TestMethod]
        public void Solve_SimpleMoveRight_SolutionIsFound()
        {
            var box1 = new Box(1, new Position(1, 0));
            var box2 = new Box(1, new Position(3, 0));
            var box3 = new Box(1, new Position(4, 0));
            var board = new Board(new Box[] { box1, box2, box3 });
            var solver = new Solve();

            var solution = solver.FindSolution(board, 1);

            Assert.IsTrue(solution.Moves.Count == 1);
            Assert.AreEqual(Move.Create(new Position(1, 0), Move.MoveDirectionEnum.Right), solution.Moves[0]);
        }

        [TestMethod]
        public void Solve_Simple2MovesLeftAndRight_SolutionIsFound()
        {
            var box1 = new Box(1, new Position(1, 0));
            var box2 = new Box(1, new Position(2, 0));
            var box3 = new Box(1, new Position(5, 0));
            var board = new Board(new Box[] { box3, box2, box1});
            var solver = new Solve();

            var solution = solver.FindSolution(board, 2);

            Assert.IsTrue(solution.Moves.Count == 2);
            Assert.AreEqual(Move.Create(new Position(5, 0), Move.MoveDirectionEnum.Left), solution.Moves[0]);
            Assert.AreEqual(Move.Create(new Position(4, 0), Move.MoveDirectionEnum.Left), solution.Moves[1]);
        }

        [TestMethod]
        public void Solve_Simple3MovesSwitchPosition_SolutionIsFound()
        {
            var box1 = new Box(2, new Position(1, 0));
            var box2 = new Box(1, new Position(2, 0));
            var box3 = new Box(2, new Position(3, 0));
            var box4 = new Box(1, new Position(4, 0));
            var box5 = new Box(2, new Position(5, 0));
            var box6 = new Box(1, new Position(6, 0));
            var board = new Board(new Box[] { box1,box2,box3,box4, box5, box6 });
            var solver = new Solve();

            var solution = solver.FindSolution(board, 3);

            Assert.IsTrue(solution.Found);
        }

        [TestMethod]
        public void Solve_Simple1MovesSwitchVerticalPosition_SolutionIsFound()
        {
            var box1 = new Box(1, new Position(1, 0));
            var box2 = new Box(1, new Position(1, 1));
            var box3 = new Box(2, new Position(1, 2));
            var box4 = new Box(1, new Position(1, 3));
            var box5 = new Box(2, new Position(1, 4));
            var box6 = new Box(2, new Position(1, 5));
            var board = new Board(new Box[] { box1, box2, box3, box4, box5, box6 });
            var solver = new Solve();

            var solution = solver.FindSolution(board, 1);

            Assert.IsTrue(solution.Found);
        }
    }
}
