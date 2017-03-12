using MoveTheBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver
{
    public class Move
    {
        public enum MoveDirectionEnum { Left, Right, Up};

        public Position Position { get; private set; }
        public MoveDirectionEnum MoveDirection { get; private set; }

        private Move()
        {

        }

        static public Move Create(Position p, MoveDirectionEnum direction)
        {
            return new Move()
            {
                Position = p,
                MoveDirection = direction
            };
        }

        public override bool Equals(object obj)
        {
            var item = obj as Move;

            if (item == null)
            {
                return false;
            }

            return item.MoveDirection.Equals(MoveDirection) && item.Position.Equals(Position);
        }
    }
}
