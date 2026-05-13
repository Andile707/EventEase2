using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Eventease.Models;
using Microsoft.Extensions.Options;

namespace Eventease.Services
{
    public class AzureBlobService : IAzureService
    {
        BlobServiceClient _blobServiceClient;
        BlobContainerClient _blobContainerClient;
        string azureConnectionstring = "DefaultEndpointsProtocol=https;AccountName=blobforeventease10538326;AccountKey=LETJdfirKUHv/n95DTp/JsxiTeu+HuHSFJJGd0XKnPaThdhqlm9oVAPSe0RQEgm6mDzvZpnuROKg+AStDuPFMA==;EndpointSuffix=core.windows.net"; 
        private readonly AzureOptions _azureOptions;

        public AzureBlobService(IOptions<AzureOptions> azureOptions)
        {
            _azureOptions = azureOptions.Value;
            _blobServiceClient = new BlobServiceClient(azureConnectionstring);
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient("EventEaseContainer");
            
        }

        /*public async Task<List<BlobContentInfo>> UploadFiles(List<IFormFile> files)
        {
            var azureResponse = new List<BlobContentInfo>();
            foreach (var file in files)
            {
                string filename = file.Name;
                using (var memorystream = new MemoryStream())
                {
                    file.CopyTo(memorystream);
                    memorystream.Position = 0;

                    var client = await _blobContainerClient.UploadBlobAsync(filename, memorystream, default);
                    azureResponse.Add(client);
                }
            }
            return azureResponse;

        }*/


        /* public void UploadFiles(IFormFile file)
         {   
             string fileExtension = Path.GetExtension(file.FileName);

             using MemoryStream fileUploadStream = new MemoryStream();
             file.CopyTo(fileUploadStream);
             fileUploadStream.Position = 0;
             BlobContainerClient blobContainerClient = new BlobContainerClient(
                 _azureOptions.ConnectionString,
                 _azureOptions.Container);
             // var uniqueName = Guid.NewGuid().ToString() + fileExtension;
             string uniqueName =
                       $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
             BlobClient blobClient = blobContainerClient.GetBlobClient(uniqueName);

             blobClient.Upload(fileUploadStream);



         }*/


        public void UploadFiles(IFormFile file)
        {
            using MemoryStream fileUploadStream = new MemoryStream();

             file.CopyToAsync(fileUploadStream);

            fileUploadStream.Position = 0;
           // _logger.LogInformation("Upload started");

            BlobContainerClient blobContainerClient =
                new BlobContainerClient(
                    _azureOptions.ConnectionString,
                    _azureOptions.Container);

            string originalName = Path.GetFileName(file.FileName);

            string safeFileName = string.Concat(
                originalName.Split(Path.GetInvalidFileNameChars()));

            string uniqueName =
                $"{Guid.NewGuid()}_{safeFileName}";

            BlobClient blobClient =
                blobContainerClient.GetBlobClient(uniqueName);

             blobClient.Upload(fileUploadStream);
        }

        public async Task<List<BlobItem>> GetUploadedBlob()
        {
            var items = new List<BlobItem>();
            var UploadedFiles = _blobContainerClient.GetBlobsAsync();
            await foreach(BlobItem file in UploadedFiles)
            {
                items.Add(file);
            }

            return items;
        }
    }
}
