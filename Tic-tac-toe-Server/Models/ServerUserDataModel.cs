using Tic_tac_toe.Models;

namespace Tic_tac_toe_Server.Models
{
    public class ServerUserDataModel
    {
        public User CurrentUser { get; set; } = null!;

        public Box[] Boxes { get; set; } = null!;

        public ServerUserDataModel(User user, Box[] boxes)
        {
            CurrentUser = user;
            Boxes = boxes;
        }
    }
}
