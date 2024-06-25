using System.Collections.Generic;

namespace Tic_tac_toe.Models
{
    internal class GameHistory
    {
        private List<Move> moves;

        public GameHistory()
        {
            moves = new List<Move>();
        }

        public void AddMove(Move move)
        {
            moves.Add(move);
        }

        public List<Move> GetMoves()
        {
            return moves;
        }

        public void ClearHistory()
        {
            moves.Clear();
        }
    }
}
