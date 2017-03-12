namespace MoveTheBox
{
    public class Box
    {
        public int Kind { private set; get; }
        public Position Position { private set; get; }

        private Box()
        {
        }

        public Box Clone()
        {
            return Box.Create(Kind, Position.Clone());
        }

        public static Box Create(int kind, Position position)
        {
            return new Box()
            {
                Kind = kind,
                Position = position
            };
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
