using CoinBase.Data;
using CoinBase.Data.Gecko;
using CoinBase.Model;
using CoinBase.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CoinBase.ViewModel
{
    class AddNewViewModel : INotifyPropertyChanged
    {
        private string quantity;
        private string pricePerCoin;
        private string note;
        private string totalSpent;
        private string textSearch; 
        private Item selectedItem;
        private AddNewModel model;
        private DateTime selectedCalandarDate;
        private Visibility hintVisibility;

        public List<Item> FullList { get; set; }
        public ObservableCollection<Item> ItemsSource { get; set; }

        public Item SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
                PricePerCoin = new PortfolioModel().GetLastPrice(value.id).ToString("#.00");
            }
        }
        public string TotalSpent
        {
            get => totalSpent;
            set
            {
                totalSpent = value;
                OnPropertyChanged(nameof(TotalSpent));
            }
        }
        public string Quantity
        {
            get => quantity;
            set
            {
                quantity = value;
                OnPropertyChanged(nameof(Quantity));
                if (double.TryParse(Quantity, out double q) && double.TryParse(PricePerCoin, out double p))
                {
                    TotalSpent = "$" + (p * q).ToString();
                }
            }
        }
        
        public string PricePerCoin
        {
            get => pricePerCoin;
            set
            {
                pricePerCoin = value;
                OnPropertyChanged(nameof(PricePerCoin));
                if (double.TryParse(Quantity, out double q) && double.TryParse(PricePerCoin, out double p))
                {
                    TotalSpent = "$"+(p * q).ToString();
                }
                
            }
        }
        public string Note
        {
            get => note;
            set
            {
                note = value;
                OnPropertyChanged(nameof(Note));
            }
        }
        public Visibility HintVisibility
        {
            get => hintVisibility;
            set
            {
                hintVisibility = value;
                OnPropertyChanged(nameof(HintVisibility));
            }

        }
        public string TextSearch
        {
            get => textSearch;
            set
            {
                textSearch = value;
                OnPropertyChanged(nameof(TextSearch));
                if (!string.IsNullOrWhiteSpace(value))
                {
                    string hintName = string.Empty;
                    HintVisibility = Visibility.Visible;
                    var list = FullList.Where(x => (x.name.Contains(value)) == true || (x.symbol.Contains(value)) == true || (x.id.Contains(value)) == true || (x.symbol.ToUpper().Contains(value)) == true).ToList();
                    ItemsSource.Clear();
                    foreach (var item in list) ItemsSource.Add(item);
                }
                else
                {
                    HintVisibility = Visibility.Hidden;
                }
            }
        }
        public DateTime SelectedCalandarDate
        {
            get => selectedCalandarDate;
            set
            {
                selectedCalandarDate = value;
                OnPropertyChanged(nameof(SelectedCalandarDate));
            }
        }
        public AddNewViewModel()
        {
            var cryptoCurrencyModel = new CryptoCurrencyModel();
            model = new AddNewModel();
            FullList = new List<Item>();

            FullList = CoinGecko.FullList;

            ItemsSource = new ObservableCollection<Item>();

            SelectedItem = FullList.FirstOrDefault();

            HintVisibility = Visibility.Hidden;
        }
        public ICommand CloseWindow => new DelegateCommand(o =>
        {
            Close();
        });
        public ICommand AddTransactionCommand => new DelegateCommand(o =>
        {
            var model = new AddNewModel();

            Quantity = Quantity.Replace(".",",");
            PricePerCoin = PricePerCoin.Replace(".",",");

            if (SelectedCalandarDate == DateTime.MinValue) SelectedCalandarDate = DateTime.Now;

            if (double.TryParse(Quantity, out double quantity) && double.TryParse(PricePerCoin, out double pricePerCoin))
            {
                if (quantity > 0 && pricePerCoin > 0)
                {
                    model.AddNew(SelectedItem.id,quantity, SelectedItem.name, SelectedCalandarDate, SelectedItem.symbol, pricePerCoin, 0, Note);
                    Note = string.Empty;
                    Close();
                }
                else MessageBox.Show("Цена монеты и количество не могут быть меньше или равно нулю.");
            }
            else MessageBox.Show("Упс, что-то пошло не так. Проверьте введенные данные.");
        });
        public ICommand AddNoteCommand => new DelegateCommand(o =>
        {
            MessageBox.Show("Значение сохранено.");
        });
        public ICommand AddCalandarDateCommand => new DelegateCommand(o =>
        {
            MessageBox.Show("Значение сохранено.");
        });
        private void Close()
        {
            var window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            for (int i = window.GridChange.Children.Count - 1; i >= 0; --i)
            {
                var childTypeName = window.GridChange.Children[i].GetType().Name;
                if (childTypeName == "AddNewView")
                {
                    window.GridChange.Children.RemoveAt(i);              
                }
            }
        }
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}