﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveTheBox
{
    public class Board
    {
        private List<Box> boxes;

        private Board() { }

        public static Board Create(Box[] boxes)
        {
            return Board.Create(boxes.ToList());
        }

        public static Board Create(List<Box> b)
        {
            return new Board()
            {
                boxes = b
            };
        }

        public Board Clone()
        {
            var newBoxes = new List<Box>();
            foreach(var box in boxes)
            {
                newBoxes.Add(box.Clone());
            }
            return Board.Create(newBoxes);
        }

        public List<Position> GetCurrentBoxPositions()
        {
            var boxPositions = new List<Position>();

            foreach(var box in boxes)
            {
                boxPositions.Add(Position.Create(box.Position.X, box.Position.Y));
            }

            return boxPositions;
        }

        public bool IsEmpty()
        {
            return boxes.Count() == 0;
        }

        public bool TryMoveLeft(Position p)
        {
            if (boxes.Exists(x => x.Position.Equals(Position.CreateLeft(p))))
            {
                var box1 = boxes.Where(x => x.Position.Equals(p)).First();
                var box2 = boxes.Where(x => x.Position.Equals(Position.CreateLeft(p))).First();
                box1.MoveLeft();
                box2.MoveRight();

                if(box1.Kind == box2.Kind)
                {
                    return false;
                }
            }
            else
            {
                boxes.Where(x => x.Position.Equals(p)).First().MoveLeft();
            }

            RemoveBoxesInARow();
            while (GravityOnBoxes())
            {
                RemoveBoxesInARow();
            }

            return true;
        }

        public bool TryMoveRight(Position p)
        {
            if(boxes.Exists(x => x.Position.Equals(Position.CreateRight(p))))
            {
                var box1 = boxes.Where(x => x.Position.Equals(p)).First();
                var box2 = boxes.Where(x => x.Position.Equals(Position.CreateRight(p))).First();
                if(box1.Kind == box2.Kind)
                {
                    return false;
                }
                box1.MoveRight();
                box2.MoveLeft();
            }
            else
            {
                boxes.Where(x => x.Position.Equals(p)).First().MoveRight();
            }
            RemoveBoxesInARow();
            while (GravityOnBoxes())
            {
                RemoveBoxesInARow();
            }
            return true;
        }

        public bool TryMoveUp(Position p)
        {
            if(boxes.Exists(x => x.Position.Equals(Position.CreateTop(p))))
            {
                var box1 = boxes.Where(x => x.Position.Equals(p)).First();
                var box2 = boxes.Where(x => x.Position.Equals(Position.CreateTop(p))).First();
                if(box1.Kind == box2.Kind)
                {
                    return false;
                }
                box1.MoveUp();
                box2.MoveDown();
            }
            else
            {
                return false;
            }
            RemoveBoxesInARow();
            while (GravityOnBoxes())
            {
                RemoveBoxesInARow();
            }
            return true;
        }

        public Box GetBoxAtPosition(Position p)
        {
            return boxes.Where(x => x.Position.Equals(p)).First();
        }

        public bool IsBoxAtPosition(Position p)
        {
            return boxes.Where(x => x.Position.Equals(p)).Count() > 0;
        }

        private bool GravityOnBoxes()
        {
            var boxesToMove = boxes.Where(x => !IsBoxBeneath(x.Position));
            var boxesMoved = false;

            while (boxesToMove.Count() > 0)
            {
                boxesMoved = true;
                foreach (var box in boxesToMove)
                {
                    box.MoveDown();
                }
                boxesToMove = boxes.Where(x => !IsBoxBeneath(x.Position));
            }

            return boxesMoved;
        }

        private bool IsBoxBeneath(Position p)
        {
            return p.Y == 0 || boxes.Count(x => Position.CreateDown(p).Equals(x.Position)) > 0;
        }

        private void RemoveBoxesInARow()
        {
            var boxPositionsToRemove = new List<Position>();

            foreach(var box in boxes)
            {
                var p = box.Position;
                var k = box.Kind;
                RemoveBoxInMiddleHorizontal(boxPositionsToRemove, p, k);
                RemoveBoxToLeftHorizontal(boxPositionsToRemove, p, k);
                RemoveBoxToRightHorizontal(boxPositionsToRemove, p, k);
                RemoveBoxInMiddleVertical(boxPositionsToRemove, p, k);
                RemoveBoxOnTopVertical(boxPositionsToRemove, p, k);
                RemoveBoxOnBottomVertical(boxPositionsToRemove, p, k);
            }

            RemoveBoxes(boxPositionsToRemove);
        }

        private void RemoveBoxToRightHorizontal(List<Position> boxPositionsToRemove, Position p, int k)
        {
            if (IsBoxAtPositionKind(Position.CreateRight(Position.CreateRight(p)), k) && IsBoxAtPositionKind(Position.CreateRight(p), k))
            {
                boxPositionsToRemove.Add(p);
            }
        }

        private void RemoveBoxToLeftHorizontal(List<Position> boxPositionsToRemove, Position p, int k)
        {
            if (IsBoxAtPositionKind(Position.CreateLeft(Position.CreateLeft(p)), k) && IsBoxAtPositionKind(Position.CreateLeft(p), k))
            {
                boxPositionsToRemove.Add(p);
            }
        }

        private void RemoveBoxInMiddleHorizontal(List<Position> boxPositionsToRemove, Position p, int k)
        {
            if (IsBoxAtPositionKind(Position.CreateLeft(p), k) && IsBoxAtPositionKind(Position.CreateRight(p), k))
            {
                boxPositionsToRemove.Add(p);
            }
        }

        private void RemoveBoxOnTopVertical(List<Position> boxPositionsToRemove, Position p, int k)
        {
            if (IsBoxAtPositionKind(Position.CreateTop(Position.CreateTop(p)), k) && IsBoxAtPositionKind(Position.CreateTop(p), k))
            {
                boxPositionsToRemove.Add(p);
            }
        }

        private void RemoveBoxOnBottomVertical(List<Position> boxPositionsToRemove, Position p, int k)
        {
            if (IsBoxAtPositionKind(Position.CreateDown(Position.CreateDown(p)), k) && IsBoxAtPositionKind(Position.CreateDown(p), k))
            {
                boxPositionsToRemove.Add(p);
            }
        }

        private void RemoveBoxInMiddleVertical(List<Position> boxPositionsToRemove, Position p, int k)
        {
            if (IsBoxAtPositionKind(Position.CreateDown(p), k) && IsBoxAtPositionKind(Position.CreateTop(p), k))
            {
                boxPositionsToRemove.Add(p);
            }
        }

        private bool IsBoxAtPositionKind(Position p, int kind)
        {
            return IsBoxAtPosition(p) && GetBoxAtPosition(p).Kind == kind;
        }

        private void RemoveBoxes(List<Position> positions)
        {
            boxes = boxes.Where(x => !positions.Contains(x.Position)).ToList();
        }

    }
}