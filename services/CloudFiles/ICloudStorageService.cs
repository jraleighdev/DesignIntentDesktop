using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignIntentDesktop.services.CloudFiles
{
    public interface ICloudStorageService
    {
        Task<bool> UploadFile(string file, Guid id);
    }
}
