using PropertyManager.Data;
using PropertyManger.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManager.Services
{
   public class PropertyTypesService
    {
        public IEnumerable<PropertyType> GetAllPropertyTypes()
        {
            var context = new PropertyManagerContext();

            return context.PropertyTypes.ToList();
        }

        public IEnumerable<PropertyType> SearchPropertyTypes(string searchTerm)
        {
            var context = new PropertyManagerContext();

            var propertyTypes = context.PropertyTypes.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                propertyTypes = propertyTypes.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            return propertyTypes.ToList();
        }

        public PropertyType GetAllPropertyTypesByID(int ID)
        {
            var context = new PropertyManagerContext();

            return context.PropertyTypes.Find(ID);
        }


        public bool SavePropertyType(PropertyType propertyType)
        {
            var context = new PropertyManagerContext();

           context.PropertyTypes.Add(propertyType);

            return context.SaveChanges() > 0;
        }

        public bool UpdatePropertyType(PropertyType propertyType)
        {
            var context = new PropertyManagerContext();

            context.Entry(propertyType).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;
        }

        public bool DeletePropertyType(PropertyType propertyType)
        {
            var context = new PropertyManagerContext();

            context.Entry(propertyType).State = System.Data.Entity.EntityState.Deleted;

            return context.SaveChanges() > 0;
        }
    }
}
