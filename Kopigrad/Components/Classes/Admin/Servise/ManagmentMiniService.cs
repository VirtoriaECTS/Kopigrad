using Kopigrad.Components.Pages.Admin.Material;
using Kopigrad.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Kopigrad.Components.Classes.Admin.Servise
{
    public class ManagmentMiniService

    {



        public void Add(int idService, string nameMiniService, string TopName, string BottomName, List<string> NameColumns, List<int> Materials, List<List<decimal>> Prices)
        {

            int idMiniService = AddMiniService(idService, nameMiniService, TopName, BottomName);



            List<int> idColumsId = new List<int>();



            foreach(string namecolumn in NameColumns)
            {
                int id = AddColumnNames(idMiniService, namecolumn);
                idColumsId.Add(id);
            }
            int i = 0;
            int j = 0;
            foreach (int material in Materials)
            {
               foreach(int idColums in idColumsId)
                {
                    AddTableMiniServie(idMiniService, material, idColums, Prices[i][j]);
                    j++;
                }
                i++;
                j = 0;
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

        public List<Miniservice> GetMiniServices(int idService)
        {
            List<Miniservice> miniServices = new List<Miniservice>();
            using (var context = new KopigradContext())
            {
                List<int> mini = context.Miniservices
                    .Include(x => x.Tableminiservices).Where(x => x.IdService == idService)
                    .Where(x => x.Tableminiservices.Any(t => t.Price > 0))
                    .Select(x => x.IdMiniService)
                    .Distinct()
                    .ToList();

                var service = context.Miniservices.ToList();

                foreach(int id in mini)
                {
                    miniServices.Add(service.Where(x => x.IdMiniService == id).FirstOrDefault());
                }
                

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


        public void DeleteMiniService(int idMiniService)
        {
            using (var context = new KopigradContext())
            {
                var tableMiniSevice = context.Tableminiservices.Where(x => x.IdMiniService==idMiniService).ToList();

                foreach(var item in tableMiniSevice)
                {
                    item.Price = -1;
                    
                }
                context.SaveChanges();
            }
        }


        public void Change(int idMiniService, string nameMiniService, string TopName, string BottomName, List<string> NameColumns, List<int> Material, List<List<decimal>> Prices)
        {

            ChangeMiniService(idMiniService, nameMiniService, TopName, BottomName);

            RealyDelete(idMiniService);

            List<int> idColumsId = new List<int>();



            foreach (string namecolumn in NameColumns)
            {
                int id = AddColumnNames(idMiniService, namecolumn);
                idColumsId.Add(id);
            }
            int i = 0;
            int j = 0;
            foreach (int material in Material)
            {
                foreach (int idColums in idColumsId)
                {
                    AddTableMiniServie(idMiniService, material, idColums, Prices[i][j]);
                    j++;
                }
                i++;
                j = 0;
            }



        }
        public void ChangeMiniService(int idMiniService, string nameMiniService, string TopName, string BottomName) 
        {
            using (var context = new KopigradContext())
            {
                var miniService = context.Miniservices.Where(x => x.IdMiniService == idMiniService).FirstOrDefault();

                miniService.NameMiniServise = nameMiniService;
                miniService.TopName = TopName;
                miniService.BottomName = BottomName;

                context.SaveChanges();
            }
        }

        public List<Models.Columnname> GetColums(int idMiniService)
        {
            List<Models.Columnname> colums = new List<Columnname>();
            using (var context = new KopigradContext())
            {
                colums = context.Columnnames.Where(x => x.IdMiniServiceNavigation.IdMiniService == idMiniService).ToList();
            }

            return colums;
        }

        public void RealyDelete(int idMiniService)
        {
            using (var context = new KopigradContext())
            {



                var tableMiniService = context.Tableminiservices.Where(x => x.IdMiniService == idMiniService).ToList();

                foreach(var row in tableMiniService)
                {
                    context.Tableminiservices.Remove(row);
                }

                var colums = context.Columnnames.Where(x => x.IdMiniService == idMiniService).ToList();

                foreach (var colum in colums)
                {
                    context.Columnnames.Remove(colum);
                }

                context.SaveChanges();

                context.SaveChanges();
            }
        }

    }
}
