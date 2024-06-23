using Avalonia.Media.Imaging;
using System.ComponentModel;

namespace Tic_tac_toe.Models
{
    public class Box : INotifyPropertyChanged
    {
        private Bitmap boxImage = null!;
        private string symbolName = null!;

        public Bitmap BoxImage
        {
            get { return boxImage; }
            set
            {
                if (boxImage == null)
                {
                    boxImage = value;
                    OnPropertyChanged(nameof(BoxImage));
                }
            }
        }

        public string SymbolName
        {
            get { return symbolName; }
            set
            {
                if (symbolName == null)
                {
                    symbolName = value;
                    OnPropertyChanged(nameof(SymbolName));
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
