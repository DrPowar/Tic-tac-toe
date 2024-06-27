using Avalonia.Media.Imaging;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using Tic_tac_toe.Constants;

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

        public void UpdatUserImage()
        {
            if(this.UserSymbol == null)
            {
                if(this.UserSymbolName == SymbolsConst.SymbolX)
                {
                    UserSymbol = new Bitmap(Symbols.SymbolPath.XPath);
                }
                else
                {
                    UserSymbol = new Bitmap(Symbols.SymbolPath.OPath);
                }
            }
        }
    }
}
