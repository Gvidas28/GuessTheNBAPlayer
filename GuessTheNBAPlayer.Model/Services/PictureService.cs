using HtmlAgilityPack;
using System;
using System.Net;

namespace GuessTheNBAPlayer.Model.Services
{
    public class PictureService : IPictureService
    {
        private static string GoogleImagesURl;

        static PictureService()
        {
            GoogleImagesURl = "https://www.google.com/search?q={0}&tbm=isch";
        }

        public string GetFirstGoogleImage(string searchTerm)
        {
            try
            {
                var url = string.Format(GoogleImagesURl, searchTerm);

                var web = new HtmlWeb();
                var doc = web.Load(url);

                var imageNodes = doc.DocumentNode.SelectNodes("//img[@data-src]");
                if (imageNodes is not null && imageNodes.Count > 0)
                    return WebUtility.HtmlDecode(imageNodes[0].Attributes["data-src"].Value);

                return string.Empty;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}