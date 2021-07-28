using System;
using System.Windows.Input;

namespace IRecommendGames.ViewModel.BaseClass
{
    class RelayCommand : ICommand
    {
        private Action<object> execute;
        private Predicate<object> canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter) { return canExecute?.Invoke(parameter) ?? true; }

        public void Execute(object parameter) { execute(parameter); }

        public event EventHandler CanExecuteChanged
        {
            add { if (canExecute != null) CommandManager.RequerySuggested += value; }
            remove { if (canExecute != null) CommandManager.RequerySuggested -= value; }
        }
    }
}