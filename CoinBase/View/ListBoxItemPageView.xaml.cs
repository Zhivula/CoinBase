using CoinBase.Data;
using CoinBase.Data.Gecko;
using CoinBase.ViewModel;
using System.Windows.Controls;

namespace CoinBase.View
{
    /// <summary>
    /// Логика взаимодействия для ListBoxItemPageView.xaml
    /// </summary>
    public partial class ListBoxItemPageView : UserControl
    {
        public ListBoxItemPageView(CoinFormat element)
        {
            InitializeComponent();
            DataContext = new ListBoxItemPageViewModel(element);
        }
    }
}
