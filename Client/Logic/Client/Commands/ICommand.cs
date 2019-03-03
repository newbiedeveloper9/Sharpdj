using System.Collections.Generic;
using SharpDj.ViewModel;

namespace SharpDj.Logic.Client.Commands
{
    public interface ICommand
    {
        string CommandText { get; }
        void Run(SdjMainViewModel sdjMainViewModel, List<string> parameters);
    }
}