namespace MoveTheBox
{
    public class Position
    {
        public int X { private set; get; }
        public int Y { private set; get; }


        private Position()
        {

        }

        public Position Clone()
        {
            return Position.Create(X, Y);
        }

        public static Position Create(int x, int y)
        {
            return new Position
            {
                X = x,
                Y = y
            };
        }

        public static Position CreateLeft(Position p)
        {
            return new Position
            {
                X = p.X-1,
                Y = p.Y
            };
        }

        public static Position CreateRight(Position p)
        {
            return new Position
            {
                X = p.X + 1,
                Y = p.Y
            };
        }

        public static Position CreateDown(Position p)
        {
            return new Position
            {
                X = p.X,
                Y = p.Y-1
            };
        }

        public static Position CreateTop(Position p)
        {
            return new Position
            {
                X = p.X,
                Y = p.Y + 1
            };
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
