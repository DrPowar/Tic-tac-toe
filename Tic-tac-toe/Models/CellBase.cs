using Avalonia.Media.Imaging;

namespace Tic_tac_toe.Models
{
    public abstract class CellBase
    {
        public bool IsEmpty { get; set; } = true;

        public Bitmap BoxImage { get; private set; }

        public string SymbolName { get; private set; }
    }
}
