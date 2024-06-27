using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_tac_toe.Models
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
