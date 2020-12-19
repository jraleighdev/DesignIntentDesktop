using System.Collections.ObjectModel;
using System.Windows.Input;
using DesignIntentDesktop.ViewModels.Base;
using InventorWrapper;

namespace DesignIntentDesktop.Components.DocumentDisplay
{
    public class DocumentDisplayViewModel : BaseViewModel
    {
        public ObservableCollection<DocumentDisplayViewModelItem> Documents { get; set; }
        
        public ICommand GetDataCommand { get; set; }

        public DocumentDisplayViewModel()
        {
            Documents = new ObservableCollection<DocumentDisplayViewModelItem>();

            GetDataCommand = new RelayCommand(GetData);
        }

        private void GetData()
        {
            if (!InventorApplication.Attached)
            {
                InventorApplication.Attach();

                if (InventorApplication.ActiveDocument.IsAssemblyDoc)
                {
                    var doc = InventorApplication.ActiveDocument.GetAssemblyDocument();

                    var docs = doc.ReferencedDocuments;

                    foreach (var document in docs)
                    {
                        Documents.Add(new DocumentDisplayViewModelItem{Name = document.Name, Path = document.FileName});
                    }
                }
            }
        }
    }
}