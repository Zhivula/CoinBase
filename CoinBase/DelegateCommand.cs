using System;
using System.Windows.Input;
namespace CoinBase
{
    public class DelegateCommand : ICommand
    {
        #region Fields
        readonly Action<object> _execut;
        readonly Predicate<object> _canExecut;
        #endregion

        #region Constructors
        public DelegateCommand(Action<object> execut) : this(execut, null) { }
        public DelegateCommand(Action<object> execut, Predicate<object> canExecut)
        {
            if (execut == null) throw new ArgumentNullException("execut");
            _execut = execut;
            _canExecut = canExecut;
        }
        #endregion
        #region ICommand numbers
        public bool CanExecute(object parametr)
        {
            return _canExecut?.Invoke(parametr) ?? true;
        }
        public void Execute(object parameter)
        {
            _execut(parameter);
        }
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
        #endregion
    }
}