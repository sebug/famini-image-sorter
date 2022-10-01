using Azure.Storage;
using Azure.Storage.Blobs;

public record ImageSorter(string AccountName, string AccountKey) {
    public async Task GetAndSaveImages() {
        var sharedKeyCredential = new StorageSharedKeyCredential(AccountName, AccountKey);
        string blobUri = "https://" + AccountName + ".blob.core.windows.net";
        var blobServiceClient = new BlobServiceClient(new Uri(blobUri), sharedKeyCredential);

        var blobContainers = blobServiceClient.GetBlobContainers();

        foreach (var blobContainer in blobContainers) {
            await Console.Out.WriteLineAsync(blobContainer.Name);
        }
    }
}