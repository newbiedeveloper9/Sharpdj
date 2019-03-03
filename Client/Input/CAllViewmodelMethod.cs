using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xaml;

namespace SharpDj.Input
{
    public class CallViemodelMethod : MarkupExtension, ICommand
    {
        private readonly string _methodName;
        private FrameworkElement _rootObject;

        public CallViemodelMethod(string methodName)
        {
            _methodName = methodName;
        }

        public event EventHandler CanExecuteChanged;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            // get root of target page
            var provideRoot = serviceProvider.GetService(typeof(IRootObjectProvider)) as IRootObjectProvider;
            _rootObject = provideRoot?.RootObject as FrameworkElement;
            return this;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            var dataContext = _rootObject?.DataContext;
            if (dataContext == null) return;

            var methodInfo = dataContext.GetType().GetMethod(_methodName);
            if (methodInfo == null) return;

            methodInfo.Invoke(dataContext, new object[0]);
        }
    }
}
