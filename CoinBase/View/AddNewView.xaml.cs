using CoinBase.Data;
using CoinBase.ViewModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CoinBase.View
{
    /// <summary>
    /// Логика взаимодействия для AddNewView.xaml
    /// </summary>
    public partial class AddNewView : UserControl
    {
        AddNewViewModel viewModel;
        public AddNewView()
        {
            InitializeComponent();
            viewModel = new AddNewViewModel();
            DataContext = viewModel;
        }
        private void ListBoxItemCommand(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var grid = sender as Grid; 
            var element = grid.DataContext as Item;
            viewModel.SelectedItem = element;
            viewModel.HintVisibility = Visibility.Hidden;
        }
    }
}
