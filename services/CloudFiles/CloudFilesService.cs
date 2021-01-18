using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DesignIntentDesktop.Models.CloudStorage;

namespace DesignIntentDesktop.HttpHelpers.CloudFiles
{
    public class CloudFilesService : ICloudFilesServices
    {
        private HttpClient _client;

        private const string controller = "CloudFiles";
        
        public CloudFilesService(HttpClient client)
        {
            _client = client;
        }
        
        public async Task<IEnumerable<CloudFile>> GetFiles()
        {
            using (var request = await _client.GetAsync($"{controller}"))
            {
                if (request.IsSuccessStatusCode)
                {
                    return await request.Content.ReadAsAsync<IEnumerable<CloudFile>>();
                }
            }

            return null;
        }

        public async Task<IEnumerable<CloudFile>> AddFile(CloudFile file)
        {
            using (var request = await _client.PostAsJsonAsync($"{controller}", file))
            {
                if (request.IsSuccessStatusCode)
                {
                    return await request.Content.ReadAsAsync<IEnumerable<CloudFile>>();
                }
            }

            return null;
        }
    }
}