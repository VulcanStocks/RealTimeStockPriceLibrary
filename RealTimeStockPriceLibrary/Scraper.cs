using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeStockPriceLibrary
{
    public class Scraper
    {
        HtmlAgilityPack.HtmlWeb htmlWeb = new HtmlAgilityPack.HtmlWeb();

        private double ParseInnerText(string innerText)
        {
            innerText = innerText.Replace(",", "");
            innerText = innerText.Replace('.', ',');

            try
            {
                return double.Parse(innerText);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public double GetPrice(string ticker)
        {
            HtmlAgilityPack.HtmlDocument htmlDocument = htmlWeb.Load("https://finance.yahoo.com/quote/" + ticker + "?p=" + ticker);
            string temp = "";
            foreach (var item in htmlDocument.DocumentNode.SelectNodes("//*[@id=\"quote-header-info\"]/div[3]/div[1]/div/fin-streamer[1]"))
            {                                
                temp += item.InnerText;
            }

            return ParseInnerText(temp);
        }

        public double GetVolume(string ticker)
        {
            HtmlAgilityPack.HtmlDocument htmlDocument = htmlWeb.Load("https://finance.yahoo.com/quote/" + ticker + "?p=" + ticker);
            string temp = "";
            foreach (var item in htmlDocument.DocumentNode.SelectNodes("//*[@id=\"quote-summary\"]/div[1]/table/tbody/tr[7]/td[2]/fin-streamer"))
            {
                temp += item.InnerText;
            }

            return ParseInnerText(temp);
        }
    }
}

