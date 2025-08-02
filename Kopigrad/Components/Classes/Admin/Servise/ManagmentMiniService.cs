
using Kopigrad.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Kopigrad.Components.Classes.Admin.Servise
{
    public class ManagmentMiniService

    {



        public void Add(int idService, string nameMiniService, string TopName, string BottomName, List<string> NameColumns, List<string> MaterialsString, List<List<decimal>> Prices)
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
            foreach (string material in MaterialsString)
            {
                int idMaterial = addMaterial(material, idMiniService);
                foreach (int idColums in idColumsId)
                {

                    AddTableMiniServie(idMiniService, idMaterial, idColums, Prices[i][j]);
                    j++;
                }
                i++;
                j = 0;
            }



        }

        public int addMaterial(string material, int idMiniService)
        {
            using (var context = new KopigradContext())
            {
                var materilNew = new Material
                {
                    IdMiniService = idMiniService,
                    NameMaterial = material,
                };

                context.Materials.Add(materilNew);
                context.SaveChanges();
                return materilNew.IdMaterial;
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


        public void Change(int idMiniService, string nameMiniService, string topName, string bottomName,
                               List<string> nameColumns, List<string> materialsString, List<List<decimal>> prices)
        {
            using (var context = new KopigradContext())
            {
                // 1. Обновляем основной объект Miniservice
                var miniService = context.Miniservices.FirstOrDefault(x => x.IdMiniService == idMiniService);
                if (miniService == null)
                    return;

                miniService.NameMiniServise = nameMiniService;
                miniService.TopName = topName;
                miniService.BottomName = bottomName;
                context.SaveChanges();

                // 2. Удаляем старые заголовки, материалы и таблицу
                var oldColumns = context.Columnnames.Where(c => c.IdMiniService == idMiniService).ToList();
                var oldMaterials = context.Materials.Where(m => m.IdMiniService == idMiniService).ToList();
                var oldPrices = context.Tableminiservices.Where(t => t.IdMiniService == idMiniService).ToList();

                context.Columnnames.RemoveRange(oldColumns);
                context.Materials.RemoveRange(oldMaterials);
                context.Tableminiservices.RemoveRange(oldPrices);
                context.SaveChanges();

                // 3. Добавляем новые заголовки и сохраняем их ID
                List<int> idColumns = new List<int>();
                foreach (var column in nameColumns)
                {
                    var newCol = new Columnname
                    {
                        IdMiniService = idMiniService,
                        NameColumn = column
                    };
                    context.Columnnames.Add(newCol);
                    context.SaveChanges();
                    idColumns.Add(newCol.IdColumnNames);
                }

                // 4. Добавляем материалы и цены
                for (int i = 0; i < materialsString.Count; i++)
                {
                    var material = new Material
                    {
                        IdMiniService = idMiniService,
                        NameMaterial = materialsString[i]
                    };
                    context.Materials.Add(material);
                    context.SaveChanges();

                    int idMaterial = material.IdMaterial;

                    for (int j = 0; j < idColumns.Count; j++)
                    {
                        var tableEntry = new Tableminiservice
                        {
                            IdMiniService = idMiniService,
                            IdMaterial = idMaterial,
                            IdColumnName = idColumns[j],
                            Price = prices[i][j]
                        };
                        context.Tableminiservices.Add(tableEntry);
                    }
                    context.SaveChanges();
                }
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


        public List<Miniservice> getMiniServiceToPdf()
        {
            using(var context = new KopigradContext())
            {
                List<Miniservice> one = GetMiniServices(1);
                List<Miniservice> two = GetMiniServices(4);
                List<Miniservice> result = one.Concat(two).ToList();
                return result;
            }
        }


        public List<Material> getAllMaterial()
        {
            using (var context = new KopigradContext())
            {
                return context.Materials
                    .Include(m => m.Tableminiservices)
                    .ToList();
            }
        }


        public List<Models.Columnname> getAllColums()
        {
            using (var context = new KopigradContext())
            {
                return context.Columnnames.ToList();
            }
        }


        public List<Models.Miniservice> getMiniService()
        {
            using (var context = new KopigradContext())
            {
                return context.Miniservices.Include(x => x.Tableminiservices)
                    .Where(x => x.Tableminiservices.Any(t => t.Price > 0))
                    .Distinct()
                    .ToList();
            }
        }



    }
}
