using Kopigrad.Models;
using System;
using System.Text.RegularExpressions;

namespace Kopigrad.Components.Classes.All
{
    public class SearchPrice
    {
        public decimal priceNotHeader(int idMiniService, int idMaterial, List<Models.Columnname> tiraj, int count)
        {
            decimal result = 0;


            List<decimal> priceList = GetPriceList(idMiniService, idMaterial);
            int vvvv = priceList.Count;
            Dictionary<string, decimal> priceDict = GetDictonary(tiraj, priceList);
            decimal price = FindPrice(priceDict, count);

            result = price * count;

            return result;
        }

        public decimal priceHeader(int idMiniService, int idMaterial, int idColumn, int count)
        {
            decimal result = 0;

            using (var context = new KopigradContext())
            {
                decimal price = context.Tableminiservices.Where(x => x.IdMiniService == idMiniService).Where(X => X.IdMaterial == idMaterial).Where(x => x.IdColumnName == idColumn).Select(x => x.Price).FirstOrDefault();

                
                result = price * count;
            }


            return result;
        }


        private List<decimal> GetPriceList(int idMiniService, int idMaterial)
        {
            
            List<decimal> priceList = new List<decimal>();

            using (var context = new KopigradContext())
            {
                var tableminiService =  context.Tableminiservices.Where(x => x.IdMiniService == idMiniService).ToList();


                priceList = tableminiService.Where(x => x.IdMaterial == idMaterial).Select(x => x.Price).ToList();

            }

            return priceList;
        }



        private Dictionary<string, decimal> GetDictonary(List<Models.Columnname> tiraj, List<decimal> priceList)
        {
            Dictionary<string, decimal> dictonary = new Dictionary<string, decimal>();

            int i = 0;
            foreach (var item in tiraj)
            {
                decimal price = priceList[i];
                dictonary[item.NameColumn] = price;
                i++;
            }

            return dictonary;
        }

        private decimal FindPrice(Dictionary<string, decimal> ranges, int pages)
        {
            decimal result = 0;
            foreach (var entry in ranges)
            {
                string key = entry.Key;

                // Извлекаем все числа из строки (одиночные или диапазоны)
                var matches = Regex.Matches(key, @"\d+");
                if (matches.Count == 2)
                {
                    int start = int.Parse(matches[0].Value);
                    int end = int.Parse(matches[1].Value);
                    if (pages >= start && pages <= end)
                    {
                        return entry.Value;
                    }
                }
                else if (matches.Count == 1 && key.Contains("и более"))
                {
                    int min = int.Parse(matches[0].Value);
                    if (pages >= min)
                    {
                        return entry.Value;
                    }
                }
            }


            return result;
        } 
    }
}
