using Kopigrad.Models;

namespace Kopigrad.Components.Classes.Admin.Servise
{
    public class MiniService
    {
        public string SaveCategory(string topCategory, string bottomCategory, int idMiniService)
        {
            if (topCategory.Length == 0) return "error";
            else if(bottomCategory.Length == 0)  return "error"; 

            using (var context = new KopigradContext())
            {
                var category = new Category
                {

                };

                
            }
            return "";
        }
    }
}
