using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver
{
    public class Solution
    {
        public List<Move> Moves { get; private set; }
        public bool Found { get; private set; }

        public static Solution CreateNoSolution()
        {
            return new Solution()
            {
                Moves = new List<Move>(),
                Found = false
            };
        }

        public static Solution Create(List<Move> moves)
        {
            return new Solution()
            {
                Moves = moves,
                Found = true
            };
        }
    }
}
