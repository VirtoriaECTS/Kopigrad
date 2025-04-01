using Kopigrad.Components.Pages.Admin.Material;
using Kopigrad.Models;

namespace Kopigrad.Components.Classes.Admin.Servise
{
    public class MiniService

    {

        public void Add(int idService, string nameMiniService, string TopName, string BottomName, List<string> NameColumns, List<int> Material, List<List<decimal>> Prices)
        {

            int idMiniService = AddMiniService(idService, nameMiniService, TopName, BottomName); 


            for(int column = 0; column < NameColumns.Count; column ++) 
            {
                int idColums = AddColumnNames(idMiniService, NameColumns[column]);


                for (int row = 0; row < Material.Count; row++)
                {
                    AddTableMiniServie(idMiniService, Material[row], idColums, Prices[row][column]);
                }


            }



        }


        public int AddMiniService(int idService, string nameMiniService, string TopName, string BottomName)
        {
            int id = 0;
            using (var context = new KopigradContext())
            {
                var miniServicelNew = new Miniservice
                {
                    IdService = idService,
                    NameMiniServise = nameMiniService,
                    TopName = TopName,
                    BottomName = BottomName

                };

                context.Miniservices.Add(miniServicelNew);
                context.SaveChanges();

                id = context.Miniservices.Select(x => x.IdMiniService).LastOrDefault();
            }
            return id;
        }


        public int AddColumnNames(int idMiniService, string nameColumn)
        {
            int id = 0;
            using (var context = new KopigradContext())
            {
                var columnlNew = new Columnname
                {
                    IdMiniService = idMiniService,
                    NameColumn = nameColumn

                };

                context.Columnnames.Add(columnlNew);
                context.SaveChanges();

                id = context.Columnnames.Select(x => x.IdColumnNames).LastOrDefault();
            }
            return id;
        }


        public void AddTableMiniServie(int idMiniService, int idMaterial, int idColumnNames, decimal price)
        {
            using (var context = new KopigradContext())
            {
                var tableMiniServiceNew = new Tableminiservice
                {
                    IdMiniService = idMiniService,
                    IdMaterial = idMaterial,
                    IdColumnNames = idColumnNames,
                    Price = price

                };

                context.Tableminiservices.Add(tableMiniServiceNew);
                context.SaveChanges();

            }
        }
    }
}
