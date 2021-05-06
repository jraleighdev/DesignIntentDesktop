using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DesignIntentDesktop.Helpers;
using DesignIntentDesktop.ServiceProxies;
using DesignIntentDesktop.ViewModels.Base;

namespace DesignIntentDesktop.Components.DocumentDisplay
{
    public class CloudFileItemViewModel : BaseViewModel
    {
        public System.Guid Id { get; set; }

        public string FilePath { get; set; }

        public string FileName { get; set; }

        public System.Guid SourceFileReferenceId { get; set; }

        public System.Guid ViewerFileReferenceId { get; set; }

        public ViewerFileType ViewerFileType { get; set; }

        public FileType FileType { get; set; }

        public string ForgeUrn { get; set; }

        public string ForgeBucket { get; set; }

        public int Version { get; set; }

        public ObservableCollection<BillItemViewModel> Bill { get; set; }

        public CloudFileDto ToDto()
        {
            var cloudFile = new CloudFileDto();

            cloudFile.Map(this, new List<string> { "Bill" });

            if (Bill.Count > 0)
            {
                cloudFile.Bill = new List<BillItemDto>();
                foreach (var b in Bill)
                {
                    cloudFile.Bill.Add(b.ToDto());
                }
            }

            return cloudFile;
        }

        public CloudFileItemViewModel(string filePath, string fileName, ViewerFileType viewerFileType, FileType fileType, string forgeUrn, string forgeBucket)
        {
            FilePath = filePath;
            FileName = fileName;
            ViewerFileType = viewerFileType;
            FileType = fileType;
            ForgeUrn = forgeUrn;
            ForgeBucket = forgeBucket;
            Id = Guid.NewGuid();
            SourceFileReferenceId = Guid.NewGuid();
            ViewerFileReferenceId = Guid.NewGuid();
            Bill = new ObservableCollection<BillItemViewModel>();
        }
    }
}