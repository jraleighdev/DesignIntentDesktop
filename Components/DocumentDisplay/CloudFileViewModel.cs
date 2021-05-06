using System;
using System.Collections.Generic;
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
using InventorWrapper.BOM;
using InventorWrapper.Documents;
using InventorWrapper.IProps;
using Microsoft.Extensions.DependencyInjection;

namespace DesignIntentDesktop.Components.DocumentDisplay
{
    public class CloudFileViewModel : BaseViewModel
    {

        private ICloudFileService _cloudFilesServices;
        private ICloudStorageService _cloudStorageService;

        private IAuthService _authService;
        
        public ObservableCollection<CloudFileItemViewModel> CloudFiles { get; set; }
        
        public ICommand GetDataCommand { get; set; }
        
        public ICommand UploadBillCommand { get; set; }

        public ICommand TestCommand { get; set; }

        public bool ButtonsEnabled => !Loading;
        
        public bool Loading { get; set; }

        public CloudFileViewModel()
        {
            CloudFiles = new ObservableCollection<CloudFileItemViewModel>();

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
                if (CloudFiles != null && CloudFiles.Count > 0)
                {
                    var service = _cloudFilesServices;

                    foreach (var file in CloudFiles)
                    {
                        await _cloudFilesServices.CreateAsync(file.ToDto());

                        using (Stream stream = File.Open(file.FilePath, FileMode.Open))
                        {
                            await _cloudStorageService.UploadAsync(file.Id, new FileParameter(stream, file.FilePath));
                        }
                    }
                }
            });
        }

        private async Task GetData()
        {
            await RunCommand(() => Loading, async () =>
            {
                try
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

                            doc.RepresentationManager.ActivateLevelOfDetail("Ilogic");

                            var documents = doc.ReferencedDocuments;

                            var bill = InventorBomHelpers.GetStructuredBill(doc);

                            CloudFiles.Clear();

                            var parent = new CloudFileItemViewModel(doc.FileName, Path.GetFileName(doc.FileName),
                                ViewerFileType.Stl, FileType.Assembly, "", "");

                            parent.Bill.AddRange(bill.Select(b => new BillItemViewModel(b)));

                            CloudFiles.Add(parent);

                            foreach (var d in documents)
                            {
                                try
                                {
                                    if (d.FileName == null) continue;

                                    var cloudFile = new CloudFileItemViewModel
                                    (
                                        d.FileName,
                                        System.IO.Path.GetFileName(d.FileName),
                                        ViewerFileType.Stl,
                                        d.IsAssemblyDoc ? FileType.Assembly : FileType.Part,
                                        "",
                                        ""
                                    );

                                    if (d.IsAssemblyDoc)
                                    {
                                        foreach (var b in bill)
                                        {
                                            if (b.Document.FileName == d.FileName)
                                            {
                                                cloudFile.Bill.Add(new BillItemViewModel(b));
                                            }

                                            if (b.Children.Count > 0)
                                            {
                                                RecurseBill(cloudFile, d, b.Children);
                                            }
                                        }
                                    }

                                    CloudFiles.Add(cloudFile);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                }
                            }

                            bill.ForEach(item => item.Dispose());
                            doc.Dispose();
                        }
                    });
                }
                catch (Exception ex)
                {

                }
            });
        }

        private void RecurseBill(CloudFileItemViewModel cloudFile, InventorDocument doc, IEnumerable<InventorBomItem> bill)
        {
            foreach (var b in bill)
            {
                if (b.Document.Name == doc.FileName)
                {
                    cloudFile.Bill.Add(new BillItemViewModel(b));
                }

                if (b.Children.Count > 0)
                {
                    RecurseBill(cloudFile, doc, b.Children);
                }
            }
        }
        
        public void Test()
        {
            ;
        }
    }
}