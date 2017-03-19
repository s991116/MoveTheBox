using MoveTheBox;
using System.Collections.Generic;

namespace MoveTheBoxUI
{
    public class BoardEditor
    {
        private Board board;

        public Board Board { get { return board; } }

        public BoardEditor(Board b)
        {
            board = b;
        }

        public void UpdateFromState(string state)
        {
            var boxes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Box>>(state);
            board.Clear();
            board.AddBoxes(boxes);
        }

        public void UpdateBoxField(Position position)
        {
            var boxes = board.GetCurrentBoxPositions();
            var b = boxes.Find(x => x.X == position.X && x.Y == position.Y);
            if(b == null)
            {
                board.AddBox(new Box(0, position));
            }
            else
            {
                var box = board.GetBoxAtPosition(position);
                if(box.Kind < 5)
                {
                    board.RemoveBox(box);
                    board.AddBox(new Box(box.Kind + 1, position));
                }
                else
                {
                    board.RemoveBox(box);
                }
            }
        }

        public void ClearBoard()
        {
            board.Clear();
        }

        internal string Serialize()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(board.Boxes, Newtonsoft.Json.Formatting.Indented);
        }
    }
}
