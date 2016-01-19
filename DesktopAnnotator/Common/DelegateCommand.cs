using System;
using System.Windows.Input;

namespace Common
{
    public class DelegateCommand : ICommand
    {
        public Action<object> ExecuteHandler { get; set; }
        public Func<object, bool> CanExecuteHandler { get; set; }


        #region コンストラクタ
        public DelegateCommand(Action<object> execute)
            : this(execute, (_) => true)
        {
        }

        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            if (canExecute == null)
                throw new ArgumentNullException("canExecute");

            this.ExecuteHandler = execute;
            this.CanExecuteHandler = canExecute;

        }
        #endregion

        #region ICommandの実装
        public bool CanExecute(object parameter)
        {
            var d = CanExecuteHandler;
            return d == null ? true : d(parameter);
        }

        public void Execute(object parameter)
        {
            var d = ExecuteHandler;
            if (d != null)
                d(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            var d = CanExecuteChanged;
            if (d != null)
                d(this, null);
        }
        #endregion
    }
}
