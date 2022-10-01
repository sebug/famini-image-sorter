Console.WriteLine("Famini Image Sorter");
var accountName = Environment.GetEnvironmentVariable("FAMINI_ACCOUNT_NAME") ?? throw new Exception("Define variable FAMINI_ACCOUNT_NAME");
var accountKey = Environment.GetEnvironmentVariable("FAMINI_ACCOUNT_KEY") ?? throw new Exception("Define variable FAMINI_ACCOUNT_KEY");

var imageSorter = new ImageSorter(accountName, accountKey);

await imageSorter.GetAndSaveImages();

