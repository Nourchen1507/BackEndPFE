using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Services
{
    public class NexusService
    {
        private readonly HttpClient _httpClient;
        private readonly string _nexusUrl;
        private readonly string _nexusApiKey;

        public NexusService(HttpClient httpClient, string nexusUrl, string nexusApiKey)
        {
            _httpClient = httpClient;
            _nexusUrl = nexusUrl;
            _nexusApiKey = nexusApiKey;
        }

        public async Task PushPackageAsync(string packagePath)
        {
            var packageContent = new MultipartFormDataContent();
            packageContent.Add(new ByteArrayContent(System.IO.File.ReadAllBytes(packagePath)), "file", "package.nupkg");

            _httpClient.DefaultRequestHeaders.Add("X-NuGet-ApiKey", _nexusApiKey);
            var response = await _httpClient.PostAsync($"{_nexusUrl}/v3/package", packageContent);

            response.EnsureSuccessStatusCode();
        }
    }

}
