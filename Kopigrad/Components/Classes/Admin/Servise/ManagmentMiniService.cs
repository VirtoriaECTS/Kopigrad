using Kopigrad.Components.Pages.Admin.Material;
using Kopigrad.Models;

namespace Kopigrad.Components.Classes.Admin.Servise
{
    public class ManagmentMiniService

    {



        public void Add(int idService, string nameMiniService, string TopName, string BottomName, List<string> NameColumns, List<int> Material, List<List<decimal>> Prices)
        {

            int idMiniService = AddMiniService(idService, nameMiniService, TopName, BottomName);


            for (int column = 0; column < NameColumns.Count; column++)
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

                id = context.Miniservices.OrderBy(x => x.IdMiniService).Select(x => x.IdMiniService).LastOrDefault();
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

                id = context.Columnnames.OrderBy(x => x.IdColumnNames).Select(x => x.IdColumnNames).LastOrDefault();
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
                    IdColumnName = idColumnNames,
                    Price = price

                };

                context.Tableminiservices.Add(tableMiniServiceNew);
                context.SaveChanges();

            }
        }

        public List<Miniservice> GetMiniServices()
        {
            List<Miniservice> miniServices = new List<Miniservice>();
            using (var context = new KopigradContext())
            {
                miniServices = context.Miniservices.ToList();

            }
            return miniServices;

        }


        public Miniservice GetMiniService(int idMiniService)
        {

            Miniservice miniService = new Miniservice();
            using (var context = new KopigradContext())
            {
                miniService = context.Miniservices.Where(x => x.IdMiniService == idMiniService).FirstOrDefault();

            }
            return miniService;
        }


        public List<string> GetNameColums(int inMiniService)
        {
            List<string> headers = new List<string>();

            using (var context = new KopigradContext())
            {
                headers = context.Columnnames.Where(x => x.IdMiniService == inMiniService).Select(x => x.NameColumn).ToList();
            }

            return headers;
        }


        public List<Material> GetMaterial(int idMiniService)
        {
            List<Material> materials = new List<Material>();

            using (var context = new KopigradContext())
            {
                List<Tableminiservice> tableMiniService = context.Tableminiservices.Where(x => x.IdMiniService == idMiniService).ToList();

                foreach (var table in tableMiniService)
                {
                    Material material = context.Materials.Where(x => x.IdMaterial == table.IdMaterial).FirstOrDefault();

                    materials.Add(material);
                }
            }

            return materials;
        }

        public List<List<decimal>> GetPrice(int idMiniService, int countColumn)
        {
            List<List<decimal>> priceAll = new List<List<decimal>>();

            using (var context = new KopigradContext())
            {
                List<Tableminiservice> tableMiniService = context.Tableminiservices.Where(x => x.IdMiniService == idMiniService).ToList();

                int countTr = tableMiniService.Count / countColumn; 

                int index = 0;

                for(int i = 0; i < countTr; i++)
                {
                    List<decimal> price = new List<decimal>();
                    for(int j = 0; j < countColumn; j++)
                    {
                        price.Add(tableMiniService[index].Price);
                        index++;
                    }
                    priceAll.Add(price);
                }
            }

            return priceAll;
        }


        


    }
}
