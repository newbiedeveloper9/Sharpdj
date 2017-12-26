using System;
using System.ComponentModel;
using System.Windows;

public class BaseViewModel : INotifyPropertyChanged
{
    protected bool IsInDesignMode
    {
        get
        {
            return (bool)DesignerProperties.IsInDesignModeProperty
                .GetMetadata(typeof(DependencyObject)).DefaultValue;
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string a_propertyName)
    {
        PropertyChangedEventHandler handler = PropertyChanged;
        handler?.Invoke(this, new PropertyChangedEventArgs(a_propertyName));
    }
}
