namespace MoveTheBox
{
    public class Box
    {
        public int Kind { private set; get; }
        public Position Position { private set; get; }

        public Box Clone()
        {
            return new Box(Kind, Position.Clone());
        }

        public Box(int kind, Position position)
        {
            Kind = kind;
            Position = position;
        }

        public void MoveLeft()
        {
            Position = Position.CreateLeft(Position);
        }

        public void MoveRight()
        {
            Position = Position.CreateRight(Position);
        }

        public void MoveDown()
        {
            Position = Position.CreateDown(Position);
        }

        public void MoveUp()
        {
            Position = Position.CreateTop(Position);
        }

        public override bool Equals(object obj)
        {
            var item = obj as Box;

            if (item == null)
            {
                return false;
            }

            return Position.Equals(item.Position) && Kind.Equals(item.Kind);
        }
    }
}
