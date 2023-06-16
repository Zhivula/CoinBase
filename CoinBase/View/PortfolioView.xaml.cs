using CoinBase.Data;
using CoinBase.Data.Gecko;
using CoinBase.Model;
using CoinBase.ViewModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CoinBase.View
{
    /// <summary>
    /// Логика взаимодействия для PortfolioView.xaml
    /// </summary>
    public partial class PortfolioView : UserControl
    {
        public PortfolioView()
        {
            InitializeComponent();
            DataContext = new PortfolioViewModel();
        }
        private void CoinPortfolioViewCommand(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var grid = sender as Grid;
            var coinPortfolio = grid.DataContext as CoinPortfolio;
            var coin = new PortfolioModel().GetCoin(coinPortfolio.Transaction.Ids);
            var window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            window.GridChange.Children.Add(new CoinPortfolioView(coin));
        }
    }
}
