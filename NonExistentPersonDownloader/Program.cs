using System;
using System.IO;
using System.Net;

namespace NonExistentPersonDownloader
{
    class Program
    {

        static void Main(string[] args)
        {
                /*
                 * setup
                 */
            // this many images you will download
            int maxImage = 10000;

            // this many images will be in a single numbered folder
            int imagePerFolder = 200;

            // you wait for this many milliseconds between each images. 
            // Faster than 1500 will result in lot of duplicate images. 
            // Even 1500 have some repeat but it is good enough.
            int msBetweenImages = 1500;

                /*
                 * other wariables 
                 */
            int folderCounter = 0;
            int perFolderCounter = 0;

            // tweek folder name here
            string folderNow = "faces" + folderCounter;
            
            // other online image generators could be used here
            string url = "https://thispersondoesnotexist.com/image"; 

            WebClient client = new WebClient();

            for (int i = 0; i < maxImage; i++) {

                if (!Directory.Exists(folderNow))
                    Directory.CreateDirectory(folderNow);
                
                Console.Clear();
                Console.WriteLine("Downloading face: " + folderNow + "\\face" + i + ".jpg");

                // you need to fake a HttpRequestHeader so the site let you download the image.
                client.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";
                client.DownloadFile(new Uri(url), folderNow + "\\face" + i + ".jpg");

                System.Threading.Thread.Sleep(msBetweenImages);

                if (perFolderCounter >= imagePerFolder-1)
                {
                    perFolderCounter = 0;
                    folderCounter++;
                    folderNow = "faces" + folderCounter;

                    continue;
                }

                perFolderCounter++;

            }
        }
    }
}
