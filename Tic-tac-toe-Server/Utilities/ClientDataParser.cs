using Tic_tac_toe.Models;

namespace Tic_tac_toe_Server.Utilities
{
    internal static class ClientDataParser
    {
        public static void ParseClientData(ClientGameDataModel gmd, ref GameHistory gameHistory, ref Cell[] boxes)
        {
            if (gmd == null)
            {
                return;
            }
            else
            {
                gameHistory.AddMove(gmd.Move);
                boxes = gmd.BoxCollection;
            }
        }
    }
}
