using Kopigrad.Models;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;


namespace Kopigrad.Components.Classes.Admin.Servise
{
    public class ManagmentServiseClasses
    {
        public string Add(byte[]? imageData, string nameServise)
        {
            if (imageData == null) return "Выберите изображение";
            else if (nameServise == null || nameServise == "") return "Введите название";

            using (var context = new KopigradContext())
            {
                var serviseNew = new Service
                {
                    NameService = nameServise,
                    Image = imageData
                };

                context.Services.Add(serviseNew);
                context.SaveChanges();

            }

            return "";

        }

        public Service? Find(int id)
        {
            Service service;

            using (var context = new KopigradContext())
            {
                service = context.Services.Where(x => x.IdService == id).FirstOrDefault();
            }
            return service;
        }

        public string Change(int id, byte[]? imageData, string nameServise)
        {
            if (imageData == null) return "Выберите изображение";
            else if (nameServise == null || nameServise == "") return "Введите название";

            using (var context = new KopigradContext())
            {
                var serviseNew = context.Services.Where(x => x.IdService == id).FirstOrDefault();

                serviseNew.NameService = nameServise;
                serviseNew.Image = imageData;

                context.SaveChanges();

            }
            return "";
        }

        public void Delete(int id)
        {
            using (var context = new KopigradContext())
            {
                var serviseNew = context.Services.Where(x => x.IdService == id).FirstOrDefault();

                context.Services.Remove(serviseNew);

                context.SaveChanges();

            }
        }


        public string GetName(int id)
        {
            string name = "";
            using (var context = new KopigradContext())
            {
                name = context.Services.Where(x => x.IdService == id).Select(x => x.NameService).FirstOrDefault();

            }
            return name;
        }
    }
}
