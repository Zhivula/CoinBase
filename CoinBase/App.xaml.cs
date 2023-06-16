using CoinBase.Data;
using CoinBase.DataBase;
using System.Windows;

namespace CoinBase
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            CoinGecko.UpDateList();
            using (var context = new MyDbContext())
            {
            }
        }
    }
}
