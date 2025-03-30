using Kopigrad.Models;
using System.Collections.Generic;

namespace Kopigrad.Components.Classes.Admin.Servise
{
    public class ManagerMaterial
    {

        public List<Viewmaterial> GetList()
        {
            List < Viewmaterial> viewmaterials = new List < Viewmaterial > ();
            using (var context = new KopigradContext())
            {
                viewmaterials = context.Viewmaterials.Where(x => x.Count > -1).ToList();

            }
            return viewmaterials;
        }


        public string Add(string nameMaterial, int count)
        {
            if (nameMaterial == null) return "Выберите название";
            else if (count < -1) return "Количество не может быть отрицательным";

            using (var context = new KopigradContext())
            {
                var materialNew = new Viewmaterial
                {
                    NameView = nameMaterial,
                    Count = count
                };

                context.Viewmaterials.Add(materialNew);
                context.SaveChanges();

            }

            return "";

        }

        public void Delete(int idMateral)
        {
            using (var context = new KopigradContext())
            {
                var material = context.Viewmaterials.Where(x => x.IdView == idMateral).FirstOrDefault();

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
                var materal = context.Viewmaterials.Where(x => x.IdView == id).FirstOrDefault();

                materal.NameView = nameMaterial;
                materal.Count = count;

                context.SaveChanges();

            }
            return "";
        }



    }
}
