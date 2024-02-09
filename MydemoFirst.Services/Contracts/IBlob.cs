namespace MydemoFirst.Services.Contracts
{
    public interface IBlob
    {

        public Task<string> UploadBlobFile(Stream file, string fileName, string contentTypeFile);
        public Task<List<string>> ListBlob();
        public Task<Stream> DownLoadBlobAsync(string blobFileName);
        public Task<bool> DeleteBlobAsync(string blobFileName);
    }
}
