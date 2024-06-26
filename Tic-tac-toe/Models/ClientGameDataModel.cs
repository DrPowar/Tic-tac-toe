using Newtonsoft.Json;
using System.Text;

namespace Tic_tac_toe.Models
{
    public class ClientGameDataModel
    {
        public Box[] BoxCollection { get; set; }
        public Move Move { get; set; }

        public ClientGameDataModel()
        {

        }

        public ClientGameDataModel(Box[] boxCollection, Move move)
        {
            BoxCollection = boxCollection;
            Move = move;
        }

        public byte[] ToJsonData(ClientGameDataModel gmd)
        {
            string gameDataJson = JsonConvert.SerializeObject(gmd);
            byte[] data = Encoding.UTF8.GetBytes(gameDataJson);
            return data;
        }

        public static ClientGameDataModel FromJsonData(string jsonData)
        {
            return JsonConvert.DeserializeObject<ClientGameDataModel>(jsonData);
        }
    }
}
