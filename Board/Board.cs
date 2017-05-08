using System.Collections.Generic;
using System.Linq;

namespace MoveTheBox
{
    public class Board
    {
        private List<Box> boxes;
        public List<Box> Boxes { get { return boxes; } }

        public Board(Box[] boxes)
        {
            this.boxes =  boxes.ToList();
        }

        public Board(List<Box> boxes)
        {
            this.boxes = boxes;
        }

        public void AddBoxes(List<Box> boxes)
        {
            this.boxes.AddRange(boxes);
        }

        public Board Clone()
        {
            var newBoxes = new List<Box>();
            foreach(var box in boxes)
            {
                newBoxes.Add(box.Clone());
            }
            return new Board(newBoxes);
        }

        public List<Position> GetCurrentBoxPositions()
        {
            var boxPositions = new List<Position>();

            foreach(var box in boxes)
            {
                if(box.Exists)
                  boxPositions.Add(new Position(box.Position.X, box.Position.Y));
            }

            return boxPositions;
        }

        public void AddBox(Box box)
        {
            if(!boxes.Exists(x => x.Position.Equals(box.Position)))
            {
                boxes.Add(box);
            }
        }

        public void Clear()
        {
            boxes.Clear();
        }

        public void RemoveBox(Box box)
        {
            if (boxes.Exists(x => x.Position.Equals(box.Position)))
            {
                boxes.Remove(box);
            }
        }


        public bool IsEmpty()
        {
            return boxes.Count(x => x.Exists) == 0;
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
                boxes.Where(x => x.Position.Equals(p) && x.Exists).First().MoveLeft();
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
                boxes.Where(x => x.Position.Equals(p) && x.Exists).First().MoveRight();
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
            if(boxes.Exists(x => x.Position.Equals(Position.CreateTop(p)) && x.Exists))
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
            return boxes.Where(x => x.Position.Equals(p) && x.Exists).First();
        }

        public bool IsBoxAtPosition(Position p)
        {
            return boxes.Where(x => x.Position.Equals(p) && x.Exists).Count() > 0;
        }

        private bool GravityOnBoxes()
        {
            var boxesToMove = boxes.Where(x => !IsBoxBeneath(x.Position) && x.Exists);
            var boxesMoved = false;

            while (boxesToMove.Count() > 0)
            {
                boxesMoved = true;
                foreach (var box in boxesToMove)
                {
                    box.MoveDown();
                }
                boxesToMove = boxes.Where(x => !IsBoxBeneath(x.Position) && x.Exists);
            }

            return boxesMoved;
        }

        private bool IsBoxBeneath(Position p)
        {
            return p.Y == 0 || boxes.Count(x => Position.CreateDown(p).Equals(x.Position) && x.Exists) > 0;
        }

        private void RemoveBoxesInARow()
        {
            var boxPositionsToRemove = new List<Position>();

            foreach(var box in boxes)
            {
                if (box.Exists)
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
            var b = boxes.Where(x => positions.Contains(x.Position) && x.Exists).ToList();
            foreach(var box in b)
            {
                box.Exists = false;
            }
        }
    }
}
