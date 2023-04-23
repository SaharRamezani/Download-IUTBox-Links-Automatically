using System.Net;
using HtmlAgilityPack;

class Program
{
    static void Main(string[] args)
    {
        // آدرس فایلی که لینک ها توشه
        string[] addresses = File.ReadAllLines(@"F:\Uni\ترم 8\شبکه 2\URLs.txt");

        foreach (string address in addresses)
        {
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    string htmlCode = webClient.DownloadString(address);

                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(htmlCode);

                    HtmlNode movieLink = doc.DocumentNode.SelectSingleNode("//a[contains(@href, '.mp4')]");

                    if (movieLink != null)
                    {
                        string movieUrl = movieLink.GetAttributeValue("href", "");
                        string localFileName = Path.GetFileName(movieUrl);

                        webClient.DownloadFile(movieUrl, localFileName);

                        Console.WriteLine($"Downloaded movie: {localFileName}");
                    }
                    else
                    {
                        Console.WriteLine("No movie link found.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error downloading {address}");
                }
            }
        }

        Console.WriteLine("All movies are downloaded.");
    }
}
