namespace MoveTheBox
{
    public class Position
    {
        public int X { private set; get; }
        public int Y { private set; get; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Position Clone()
        {
            return new Position(X, Y);
        }

        public static Position CreateLeft(Position p)
        {
            return new Position(p.X - 1, p.Y);
        }

        public static Position CreateRight(Position p)
        {
            return new Position(p.X + 1, p.Y);
        }

        public static Position CreateDown(Position p)
        {
            return new Position(p.X, p.Y - 1);
        }

        public static Position CreateTop(Position p)
        {
            return new Position(p.X, p.Y + 1);
        }

        public override bool Equals(object obj)
        {
            var item = obj as Position;

            if (item == null)
            {
                return false;
            }

            return X.Equals(item.X) && Y.Equals(item.Y); 
        }

        public override int GetHashCode()
        {
            return X ^ Y;
        }
    }
}
