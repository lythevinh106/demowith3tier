using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Azure;
using MydemoFirst.Services.Contracts;
namespace MydemoFirst.Services.Blob
{
    public class BLobService : IBlob
    {

        private BlobServiceClient _client1Blob;

        private BlobContainerClient _clientWithName;

        public static readonly List<string> ImageExtensions = new List<string> { ".jpg", ".jpeg", ".png" };

        public BLobService(
            IAzureClientFactory<BlobServiceClient> blobClientFactory


            )
        {
            _client1Blob = blobClientFactory.CreateClient("blob1");

            // storage name 
            _clientWithName = _client1Blob.GetBlobContainerClient("azureblobwith3tier");



        }




        public async Task<string> UploadBlobFile(Stream file, string fileName, string contentTypeFile)
        {



            ///create blob instance
            var blobClient = _clientWithName.GetBlobClient($"{fileName}+{Guid.NewGuid()}");

            //option

            BlobUploadOptions uploadOptions = new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = contentTypeFile
                }
            };


            var status = await blobClient.UploadAsync(file, uploadOptions);

            return blobClient.Uri.AbsoluteUri;
        }
        public async Task<bool> DeleteBlobAsync(string blobFileName)
        {

            bool result = false;
            BlobClient file = _clientWithName.GetBlobClient(blobFileName);


            if (await file.ExistsAsync())
            {
                result = await file.DeleteIfExistsAsync();
            }
            else
            {
                throw new FileNotFoundException("File Không tồn tại trên hệ thống");
            }

            return result;


        }

        public async Task<Stream> DownLoadBlobAsync(string blobFileName)
        {

            BlobClient file = _clientWithName.GetBlobClient(blobFileName);

            if (await file.ExistsAsync())
            {
                var downloadContent = await file.DownloadAsync();

                return downloadContent.Value.Content;
            }
            else
            {
                throw new FileNotFoundException("File Không tồn tại trên hệ thống");
            }



        }

        public Task<List<string>> ListBlob()
        {
            throw new NotImplementedException();
        }


    }
}
