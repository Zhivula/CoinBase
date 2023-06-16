using CoinBase.Data.Gecko;
using CoinBase.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CoinBase.View
{
    /// <summary>
    /// Логика взаимодействия для CoinPortfolioView.xaml
    /// </summary>
    public partial class CoinPortfolioView : UserControl
    {
        public CoinPortfolioView(Coin coin)
        {
            InitializeComponent();
            DataContext = new CoinPortfolioViewModel(coin);
        }
    }
}
