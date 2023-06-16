using CoinBase.Model;
using CoinBase.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CoinBase.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private MainWindow window;

        public MainWindowViewModel()
        {
            var model = new MainWindowModel();
            window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            window.GridChange.Children.Add(new View.FirstPage());
        }
        public ICommand CloseWindow => new DelegateCommand(o =>
        {
            window.Close();
        });
        public ICommand CryptocurrenciesCommand => new DelegateCommand(o =>
        {
            window.GridChange.Children.Clear();
            window.GridChange.Children.Add(new CryptoCurrencyView());
        });
        
        public ICommand PortfolioCommand => new DelegateCommand(o =>
        {
            window.GridChange.Children.Clear();
            window.GridChange.Children.Add(new PortfolioView());
        });
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
