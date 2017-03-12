using System;
using MoveTheBox;
using System.Collections.Generic;

namespace Solver
{
    public class Solve
    {
        public Solution FindSolution(Board board, int maxMoves)
        {
            var solutionMoves = new List<Move>();
            return FindSolutionIteration(solutionMoves, board, 0, maxMoves);
        }

        private Solution FindSolutionIteration(List<Move> moves, Board board, int boxNr, int maxMoves)
        {
            if (board.IsEmpty())
            {
                return Solution.Create(moves);
            }
            var oldMoves = new Move[moves.Count];
            moves.CopyTo(oldMoves);
            
            if (maxMoves == 0)
                return Solution.CreateNoSolution();

            var positions = board.GetCurrentBoxPositions();
            if (boxNr >= positions.Count)
                return Solution.CreateNoSolution();

            var position = positions[boxNr];

            var testBoardLeft = board.Clone();
            if(testBoardLeft.TryMoveLeft(position))
            {
                var movesLeft = new List<Move>(oldMoves);
                movesLeft.Add(Move.Create(position, Move.MoveDirectionEnum.Left));
                var sLeft = FindSolutionIteration(movesLeft, testBoardLeft, boxNr, maxMoves - 1);
                if (sLeft.Found)
                {
                    return sLeft;
                }
            }

            var testBoardRight = board.Clone();
            if(testBoardRight.TryMoveRight(position))
            {
                var movesRight = new List<Move>(oldMoves);
                movesRight.Add(Move.Create(position, Move.MoveDirectionEnum.Right));
                var sRight = FindSolutionIteration(movesRight, testBoardRight, boxNr, maxMoves - 1);
                if (sRight.Found)
                {
                    return sRight;

                }
            }

            var testBoardUp = board.Clone();
            if(testBoardUp.TryMoveUp(position))
            {
                var movesUp = new List<Move>(oldMoves);
                movesUp.Add(Move.Create(position, Move.MoveDirectionEnum.Up));
                var sUp = FindSolutionIteration(movesUp, testBoardUp, boxNr, maxMoves - 1);
                if (sUp.Found)
                {
                    return sUp;
                }
            }

            var sNext = FindSolutionIteration(moves, board, boxNr + 1, maxMoves);
            if (sNext.Found)
            {
                return sNext;
            }
            else
            {
                return Solution.CreateNoSolution();
            }
        }
    }
}
