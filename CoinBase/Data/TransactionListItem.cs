using CoinBase.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CoinBase.Data
{
    class TransactionListItem
    {
        Transaction transaction;
        public string Ids { get; set; }
        public double Quantity { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Symbol { get; set; }
        public string PricePerCoin { get; set; }
        public double Fee { get; set; }
        public string Notes { get; set; }
        public TransactionListItem(Transaction transaction)
        {
            this.transaction = transaction;

            Ids = transaction.Ids;
            Quantity = transaction.Quantity;
            Name = transaction.Name;
            Date = transaction.Date;
            Symbol = transaction.Symbol;
            PricePerCoin = transaction.PricePerCoin.ToString("0.00");
            Fee = transaction.Fee;
            Notes = transaction.Notes;
        }
        public ICommand DeleteTransactionCommand => new DelegateCommand(o =>
        {
            using (var context = new MyDbContext())
            {
                if (context.Transactions.Where(x => x.Ids == transaction.Ids).Count() > 1)
                {
                    var itemTransaction = context.Transactions.Where(y => y.Id == transaction.Id).First();
                    context.Transactions.Remove(itemTransaction);
                    context.SaveChanges();
                }
                else
                {
                    var itemPortfolio = context.Portfolio.Where(x => x.Ids == transaction.Ids).First();
                    context.Portfolio.Remove(itemPortfolio);
                    var itemTransactions = context.Transactions.First(x => x.Ids == transaction.Ids);
                    context.Transactions.Remove(itemTransactions);
                    context.SaveChanges();
                }
            }
        });
        public ICommand ChangeTransactionCommand => new DelegateCommand(o =>
        {
            MessageBox.Show("Сработало.");
        });
    }
}
