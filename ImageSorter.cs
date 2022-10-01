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

        var imagesSortedBlobClient = metaClient.GetBlobClient("images_ordered.txt");

        var orderedContent = await imagesSortedBlobClient.DownloadContentAsync();

        var orderedContentDownloadResult = orderedContent.Value;

        var orderedContentString = orderedContentDownloadResult.Content.ToString();

        List<string> imageFiles = new List<string>();
        using (var sr = new StringReader(orderedContentString))
        {
            string? line = sr.ReadLine();
            while (line != null) {
                if (!String.IsNullOrEmpty(line)) {
                    imageFiles.Add(line);
                }
                line = sr.ReadLine();
            }
        }

        if (!Directory.Exists("output_images"))
        {
            Directory.CreateDirectory("output_images");
        }

        int i = 0;
        foreach (var imageFile in imageFiles)
        {
            var imageClient = photosClient.GetBlobClient(imageFile);
            var imageContent = await imageClient.DownloadContentAsync();
            var imageBytes = imageContent.Value.Content.ToArray();
            File.WriteAllBytes(Path.Combine("output_images", i.ToString("000") + ". " + imageFile), imageBytes);
            i += 1;
        }
    }
}