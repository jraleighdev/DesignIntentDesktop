using System.Windows.Controls;
using DesignIntentDesktop.Components.DocumentDisplay;

namespace DesignIntentDesktop.Components.Commands
{
    public partial class CommandView : UserControl
    {
        public CommandView()
        {
            InitializeComponent();
            
            var viewModel = new CommandsViewModel();

            this.DataContext = viewModel;
        }
    }
}