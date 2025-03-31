using Kopigrad.Models;
using System.Collections.Generic;

namespace Kopigrad.Components.Classes.Admin.Servise
{
    public class ManagerMaterial
    {

        public List<Material> GetList()
        {
            List<Material> viewmaterials = new List<Material>();
            using (var context = new KopigradContext())
            {
                viewmaterials = context.Materials.Where(x => x.Count > -1).ToList();

            }
            return viewmaterials;
        }


        public string Add(string nameMaterial, int count)
        {
            if (nameMaterial == null) return "Выберите название";
            else if (count < -1) return "Количество не может быть отрицательным";

            using (var context = new KopigradContext())
            {
                var materialNew = new Material
                {
                    NameMaterial = nameMaterial,
                    Count = count
                };

                context.Materials.Add(materialNew);
                context.SaveChanges();

            }

            return "";

        }

        public void Delete(int idMateral)
        {
            using (var context = new KopigradContext())
            {
                var material = context.Materials.Where(x => x.IdMaterial == idMateral).FirstOrDefault();

                material.Count = -1;

                context.SaveChanges();

            }

        }


        public string Change(string nameMaterial, int count, int id)
        {
            if (nameMaterial == null) return "Выберите название";
            else if (count < -1) return "Количество не может быть отрицательным";

            using (var context = new KopigradContext())
            {
                var materal = context.Materials.Where(x => x.IdMaterial == id).FirstOrDefault();

                materal.NameMaterial = nameMaterial;
                materal.Count = count;

                context.SaveChanges();

            }
            return "";
        }



    }
}
