using Avalonia.Media.Imaging;
using GalaSoft.MvvmLight.Helpers;
using System.ComponentModel;

namespace Tic_tac_toe.Models
{
    public class Box : INotifyPropertyChanged
    {
        private Bitmap? boxImage;
        private string? symbolName;
        public bool IsEmpty { get; set; } = true;

        public Bitmap BoxImage
        {
            get { return boxImage; }
            private set
            {
                boxImage = value;
            }
        }

        public string SymbolName
        {
            get { return symbolName; }
            private set
            {
                symbolName = value;
            }
        }

        public void BoxReset()
        {
            IsEmpty = true;
            OnPropertyChanged(nameof(IsEmpty));
            BoxImage = null;
            OnPropertyChanged(nameof(BoxImage));
            SymbolName = null;
            OnPropertyChanged(nameof(SymbolName));
        }

        public void BoxSetValues(Bitmap image, string symbolName)
        {
            BoxImage = image;
            OnPropertyChanged(nameof(BoxImage));
            SymbolName = symbolName;
            OnPropertyChanged(nameof(SymbolName));
            IsEmpty = false;
            OnPropertyChanged(nameof(IsEmpty));
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
