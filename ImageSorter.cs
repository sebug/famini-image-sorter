using Azure.Storage;
using Azure.Storage.Blobs;

public record ImageSorter(string AccountName, string AccountKey) {
    public async Task GetAndSaveImages() {
        var sharedKeyCredential = new StorageSharedKeyCredential(AccountName, AccountKey);
        string blobUri = "https://" + AccountName + ".blob.core.windows.net";
        var blobServiceClient = new BlobServiceClient(new Uri(blobUri), sharedKeyCredential);

        var blobContainers = blobServiceClient.GetBlobContainers();

        var metaClient = blobServiceClient.GetBlobContainerClient("meta");

        var photosClient = blobServiceClient.GetBlobContainerClient("photos");

        var metaFiles = metaClient.GetBlobsAsync();
        
        await foreach (var metaPage in metaFiles.AsPages())
        {
            foreach (var item in metaPage.Values)
            {
                Console.WriteLine(item.Name);
            }
        }
    }
}