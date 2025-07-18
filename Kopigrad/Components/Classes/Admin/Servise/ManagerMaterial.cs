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
                viewmaterials = context.Materials.ToList();

            }
            return viewmaterials;
        }










    }
}
