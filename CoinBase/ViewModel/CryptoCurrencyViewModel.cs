using CoinBase.Model;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using CoinBase.Data;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows;
using System.Linq;
using CoinBase.View;
using CoinBase.Data.Gecko;

namespace CoinBase.ViewModel
{
    class CryptoCurrencyViewModel : INotifyPropertyChanged
    {
        private List<CoinFormat> items;
        public List<CoinFormat> ItemsSource
        {
            get => items;
            set
            {
                items = value;
                OnPropertyChanged(nameof(ItemsSource));
            }
        }

        private string cmc_rank;
        public string Cmc_rank
        {
            get => cmc_rank;
            set
            {
                cmc_rank = value;
                OnPropertyChanged(nameof(Cmc_rank));
            }
        }
        private string imageSource;
        public string ImageSource
        {
            get => imageSource;
            set
            {
                imageSource = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }
        

        public CryptoCurrencyViewModel()
        {
            CryptoCurrencyModel model = new CryptoCurrencyModel();
            ItemsSource = new List<CoinFormat>();
            ItemsSource.AddRange(GetFormat(model.ItemsSource));
        }
        public List<CoinFormat> GetFormat(List<Coin> coins)
        {
            var result = new List<CoinFormat>();
            foreach(var item in coins)
            {
                var formatCoin = new CoinFormat(item);
                result.Add(formatCoin);
            }
            return result;
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