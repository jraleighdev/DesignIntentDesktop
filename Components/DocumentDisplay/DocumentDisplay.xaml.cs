using System.Windows.Controls;

namespace DesignIntentDesktop.Components.DocumentDisplay
{
    public partial class DocumentDisplay : UserControl
    {
        public DocumentDisplay()
        {
            InitializeComponent();
            
            var viewModel = new DocumentDisplayViewModel();

            this.DataContext = viewModel;
        }
    }
}