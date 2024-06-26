using Avalonia.Media.Imaging;
using Newtonsoft.Json;

namespace Tic_tac_toe.Models
{
    public class User
    {
        [JsonIgnore]
        public Bitmap? UserSymbol { get; set; }

        public string UserSymbolName { get; set; }

        public bool IsActived { get; set; }

        public User(Bitmap userSymbol, string symbolName, bool isActived)
        {
            UserSymbol = userSymbol;
            UserSymbolName = symbolName;
            IsActived = isActived;
        }
        public User()
        {

        }
        public User(string symbolName, bool isActived)
        {
            UserSymbolName = symbolName;
            IsActived = isActived;
        }
    }
}
