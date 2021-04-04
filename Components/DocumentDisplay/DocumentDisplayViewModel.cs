using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using DesignIntentDesktop.Helpers;
using DesignIntentDesktop.ServiceProxies;
using DesignIntentDesktop.services.Authentication;
using DesignIntentDesktop.ViewModels.Base;
using InventorWrapper;
using InventorWrapper.IProps;
using Microsoft.Extensions.DependencyInjection;

namespace DesignIntentDesktop.Components.DocumentDisplay
{
    public class DocumentDisplayViewModel : BaseViewModel
    {

        private ICloudFileService _cloudFilesServices;
        private ICloudStorageService _cloudStorageService;

        private IAuthService _authService;
        
        public ObservableCollection<DocumentDisplayViewModelItem> Documents { get; set; }
        
        public ICommand GetDataCommand { get; set; }
        
        public ICommand UploadBillCommand { get; set; }

        public ICommand TestCommand { get; set; }

        public bool ButtonsEnabled => !Loading;
        
        public bool Loading { get; set; }

        public DocumentDisplayViewModel()
        {
            Documents = new ObservableCollection<DocumentDisplayViewModelItem>();

            GetDataCommand = new RelayCommand(async () => await GetData());
            UploadBillCommand = new RelayCommand(async () => await UploadBill());
            TestCommand = new RelayCommand(Test);

            _cloudFilesServices = App.ServiceProvider.GetRequiredService<ICloudFileService>();
            _cloudStorageService = App.ServiceProvider.GetRequiredService<ICloudStorageService>();
        }

        private async Task UploadBill()
        {
            await RunCommand(() => Loading, async () =>
            {
                if (Documents != null && Documents.Count > 0)
                {
                    var service = _cloudFilesServices;

                    var doc = InventorApplication.ActiveDocument;

                    var file = new CloudFile
                    {
                        Id = Guid.NewGuid(),
                        FilePath = doc.FileName,
                        FileName = System.IO.Path.GetFileName(doc.FileName),
                        ThreeDfile = true,
                        DrawingFile = false,
                        ForgeUrn = "",
                        ForgeBucket = "",
                        Bill = Documents.Select(document => Mapping.Map<BillItem>(document)).ToList()
                    };

                    await _cloudFilesServices.AddFileAsync(file);

                    using (Stream stream = File.Open(file.FilePath, FileMode.Open))
                    {
                        await _cloudStorageService.UploadAsync(file.Id, new FileParameter(stream, file.FilePath));
                    }
                }
            });
        }

        private async Task GetData()
        {
            await RunCommand(() => Loading, async () =>
            {
                await Task.Run(() =>
                {
                    if (!InventorApplication.Attached)
                    {
                        InventorApplication.Attach();
                    }

                    if (InventorApplication.ActiveDocument.IsAssemblyDoc)
                    {
                        var doc = InventorApplication.ActiveDocument.GetAssemblyDocument();

                        doc.RepresentationManager.ActivateLevelOfDetail("Master");

                        doc.Bom.PartsOnlyViewEnabled = true;

                        doc.Bom.View = "Parts Only";

                        var bill = doc.Bom.GetGill();

                        Documents.Clear();

                        foreach (var b in bill)
                        {
                            Documents.Add(new DocumentDisplayViewModelItem
                            (
                                b.Document.Name,
                                b.Document.Properties.GetPropertyValue(IpropertyEnum.PartNumber),
                                b.Document.Properties.GetPropertyValue(IpropertyEnum.Description),
                                b.Document.Properties.GetPropertyValue(IpropertyEnum.StockNumber),
                                b.Qty,
                                ConvererHelpers.ConvertDouble((b.Document.Properties.GetPropertyValue(IpropertyEnum.Custom, "Length")))
                            ));
                        }
                    }

                });
            });
        }

        public void Test()
        {
            ;
        }
    }
}