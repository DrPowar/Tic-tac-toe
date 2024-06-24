using Avalonia.Media.Imaging;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Tic_tac_toe.Models
{
    public class Box : INotifyPropertyChanged
    {
        private Bitmap? boxImage;
        private string? symbolName;
        public bool IsEmpty { get; set; } = true;

        [JsonIgnore]
        public Bitmap BoxImage
        {
            get { return boxImage; }
            set
            {
                boxImage = value;
            }
        }

        public string SymbolName
        {
            get { return symbolName; }
            set
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

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }

        public static Box FromJson(string json)
        {
            return JsonSerializer.Deserialize<Box>(json);
        }

    }
}
