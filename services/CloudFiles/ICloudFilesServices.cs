using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DesignIntentDesktop.Models.CloudStorage;

namespace DesignIntentDesktop.HttpHelpers.CloudFiles
{
    public interface ICloudFilesServices
    {
        Task<IEnumerable<CloudFile>> GetFiles();

        Task<IEnumerable<CloudFile>> AddFile(CloudFile file); 
    }
}