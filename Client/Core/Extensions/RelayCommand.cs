using System;

namespace SharpDj.Core.Extensions
{
    public class RelayCommand : CommandBase
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            if (canExecute == null)
            {
                // no can execute provided, then always executable
                canExecute = (o) => true;
            }
            this._execute = execute ??  throw new ArgumentNullException(nameof(execute)); 
            this._canExecute = canExecute;
        }

        public override bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        protected override void OnExecute(object parameter)
        {
            _execute(parameter);
        }
    }
}
