using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace SharpDj.Extensions
{
    public class NotifyObject : INotifyPropertyChanged, IDisposable
    {
        #region Event

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Event

        #region Methods

        public void Dispose()
        {
            if (PropertyChanged != null)
            {
                var delgates = PropertyChanged.GetInvocationList().ToList();
                foreach (var del in delgates)
                {
                    PropertyChanged -= (PropertyChangedEventHandler)del;
                }
            }

            IsDisposed = true;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            try
            {
                var handler = System.Threading.Interlocked.CompareExchange(ref PropertyChanged, null, null);
                handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            catch (Exception e)
            {
                new Models.Helpers.ExceptionLogger(e);
            }
        }

        protected virtual void OnPropertyChanged(Expression<Func<object>> extension)
        {
            try
            {
                //debug
                var name = (extension.Body as MemberExpression ?? ((UnaryExpression)extension.Body).Operand as MemberExpression).Member.Name;
                Console.WriteLine(name);
          
                var handler = System.Threading.Interlocked.CompareExchange(ref PropertyChanged, null, null);
                handler?.Invoke(this, new PropertyChangedEventArgs(extension.GetPropertyName()));
            }
            catch (Exception e)
            {
                new Models.Helpers.ExceptionLogger(e);
            }
        }

        #endregion Methods

        #region Properties

        public bool IsDisposed { get; set; }

        #endregion Properties
    }
}
