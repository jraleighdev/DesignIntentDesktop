using System.Collections.ObjectModel;
using DesignIntentDesktop.ViewModels.Base;

namespace DesignIntentDesktop.Components.Commands
{
    public class CommandsViewModel : BaseViewModel
    {
        public ObservableCollection<CommandViewModel> Commands { get; set; }
        
        public CommandsViewModel()
        {
            Commands = new ObservableCollection<CommandViewModel>();
        }
    }
}