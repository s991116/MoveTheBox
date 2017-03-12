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
            var board = Board.Create(new Box[] { });
            var solver = new Solve();

            var solution = solver.FindSolution(board,1);

            Assert.IsTrue(solution.Moves.Count == 0);
        }

        [TestMethod]
        public void Solve_SimpleMoveLeft_SolutionIsFound()
        {
            var box1 = Box.Create(1, Position.Create(1, 0));
            var box2 = Box.Create(1, Position.Create(2, 0));
            var box3 = Box.Create(1, Position.Create(4, 0));
            var board = Board.Create(new Box[] {box1, box2, box3 });
            var solver = new Solve();

            var solution = solver.FindSolution(board,1);

            Assert.IsTrue(solution.Moves.Count == 1);
            Assert.AreEqual(Move.Create(Position.Create(4,0), Move.MoveDirectionEnum.Left), solution.Moves[0]);
        }

        [TestMethod]
        public void Solve_SimpleMoveRight_SolutionIsFound()
        {
            var box1 = Box.Create(1, Position.Create(1, 0));
            var box2 = Box.Create(1, Position.Create(3, 0));
            var box3 = Box.Create(1, Position.Create(4, 0));
            var board = Board.Create(new Box[] { box1, box2, box3 });
            var solver = new Solve();

            var solution = solver.FindSolution(board, 1);

            Assert.IsTrue(solution.Moves.Count == 1);
            Assert.AreEqual(Move.Create(Position.Create(1, 0), Move.MoveDirectionEnum.Right), solution.Moves[0]);
        }

        [TestMethod]
        public void Solve_Simple2MovesLeftAndRight_SolutionIsFound()
        {
            var box1 = Box.Create(1, Position.Create(1, 0));
            var box2 = Box.Create(1, Position.Create(2, 0));
            var box3 = Box.Create(1, Position.Create(5, 0));
            var board = Board.Create(new Box[] { box3, box2, box1});
            var solver = new Solve();

            var solution = solver.FindSolution(board, 2);

            Assert.IsTrue(solution.Moves.Count == 2);
            Assert.AreEqual(Move.Create(Position.Create(5, 0), Move.MoveDirectionEnum.Left), solution.Moves[0]);
            Assert.AreEqual(Move.Create(Position.Create(4, 0), Move.MoveDirectionEnum.Left), solution.Moves[1]);
        }

        [TestMethod]
        public void Solve_Simple3MovesSwitchPosition_SolutionIsFound()
        {
            var box1 = Box.Create(2, Position.Create(1, 0));
            var box2 = Box.Create(1, Position.Create(2, 0));
            var box3 = Box.Create(2, Position.Create(3, 0));
            var box4 = Box.Create(1, Position.Create(4, 0));
            var box5 = Box.Create(2, Position.Create(5, 0));
            var box6 = Box.Create(1, Position.Create(6, 0));
            var board = Board.Create(new Box[] { box1,box2,box3,box4, box5, box6 });
            var solver = new Solve();

            var solution = solver.FindSolution(board, 3);

            Assert.IsTrue(solution.Found);
        }

        [TestMethod]
        public void Solve_Simple1MovesSwitchVerticalPosition_SolutionIsFound()
        {
            var box1 = Box.Create(1, Position.Create(1, 0));
            var box2 = Box.Create(1, Position.Create(1, 1));
            var box3 = Box.Create(2, Position.Create(1, 2));
            var box4 = Box.Create(1, Position.Create(1, 3));
            var box5 = Box.Create(2, Position.Create(1, 4));
            var box6 = Box.Create(2, Position.Create(1, 5));
            var board = Board.Create(new Box[] { box1, box2, box3, box4, box5, box6 });
            var solver = new Solve();

            var solution = solver.FindSolution(board, 1);

            Assert.IsTrue(solution.Found);
        }

        [TestMethod]
        public void AdvancedMoveTheBoxLevel()
        {
            //Arrange
            var box1_oldbag = Box.Create(1, Position.Create(2, 5));
            var box2_oldbag = Box.Create(1, Position.Create(3, 4));
            var box3_oldbag = Box.Create(1, Position.Create(4, 3));

            var box1_packet = Box.Create(2, Position.Create(2, 4));
            var box2_packet = Box.Create(2, Position.Create(3, 0));
            var box3_packet = Box.Create(2, Position.Create(4, 4));

            var box1_metal = Box.Create(3, Position.Create(2, 3));
            var box2_metal = Box.Create(3, Position.Create(3, 3));
            var box3_metal = Box.Create(3, Position.Create(4, 2));

            var box1_wood = Box.Create(4, Position.Create(2, 2));
            var box2_wood = Box.Create(4, Position.Create(3, 2));
            var box3_wood = Box.Create(4, Position.Create(4, 1));
            var box4_wood = Box.Create(4, Position.Create(4, 0));
            var box5_wood = Box.Create(4, Position.Create(6, 0));
            var box6_wood = Box.Create(4, Position.Create(7, 0));

            var box1_red = Box.Create(5, Position.Create(2, 0));
            var box2_red = Box.Create(5, Position.Create(2, 1));
            var box3_red = Box.Create(5, Position.Create(3, 1));

            var board = Board.Create(new Box[] { box1_oldbag, box2_oldbag, box3_oldbag, box1_packet, box2_packet, box3_packet, box1_metal, box2_metal, box3_metal,
            box1_wood, box2_wood, box3_wood, box4_wood, box5_wood, box6_wood, box1_red, box2_red, box3_red});
            var solve = new Solve();


            //Act
            var solution = solve.FindSolution(board, 4);

            //Assert
            Assert.IsTrue(solution.Found);
        }

    }
}
