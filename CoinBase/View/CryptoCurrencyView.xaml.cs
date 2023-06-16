using CoinBase.Data;
using CoinBase.Data.Gecko;
using CoinBase.ViewModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CoinBase.View
{
    /// <summary>
    /// Логика взаимодействия для CryptoCurrencyView.xaml
    /// </summary>
    public partial class CryptoCurrencyView : UserControl
    {
        public CryptoCurrencyView()
        {
            InitializeComponent();
            DataContext = new CryptoCurrencyViewModel();
        }

        private void ListBoxItemPageViewCommand(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var grid = sender as Grid;
            var element = grid.DataContext as CoinFormat;
            var window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            window.GridChange.Children.Add(new ListBoxItemPageView(element));
        }
    }
}
