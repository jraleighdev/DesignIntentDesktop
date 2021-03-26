using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DesignIntentDesktop.services.CloudFiles
{
    public class CloudStorageService : ICloudStorageService
    {
        private readonly HttpClient _client;

        public CloudStorageService(HttpClient client)
        {
            _client = client;
        }

        public async Task<bool> UploadFile(string file, Guid id)
        {
            using (var form = new MultipartFormDataContent())
            {
                using (var fs = File.OpenRead(file))
                {
                    using (var streamContent = new StreamContent(fs))
                    {
                        using (var fileContent = new ByteArrayContent(await streamContent.ReadAsByteArrayAsync()))
                        {
                            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                             
                            // "file" parameter name should be the same as the server side input parameter name
                            form.Add(fileContent, "file", Path.GetFileName(file));
                            HttpResponseMessage response = await _client.PostAsync($"CloudStorageService/Upload?id={id}", form);

                            if (response.IsSuccessStatusCode)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }
    }
}
