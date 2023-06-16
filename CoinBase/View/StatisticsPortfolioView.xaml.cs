using CoinBase.ViewModel;
using System.Windows.Controls;

namespace CoinBase.View
{
    /// <summary>
    /// Логика взаимодействия для StatisticsPortfolioView.xaml
    /// </summary>
    public partial class StatisticsPortfolioView : UserControl
    {
        public StatisticsPortfolioView()
        {
            InitializeComponent();
            DataContext = new StatisticsPortfolioViewModel();
        }
    }
}
