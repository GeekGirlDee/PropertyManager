using PropertyManager.Data;
using PropertyManger.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManager.Services
{
    public class AccommodationPackageService
    {
        public IEnumerable<AccommodationPackage> GetAllPropertyTypes()
        {
            var context = new PropertyManagerContext();

            return context.AccommodationPackages.ToList();
        }

        public IEnumerable<AccommodationPackage> GetAllAccommodationPackages()      //added
        {
            var context = new PropertyManagerContext();

            return context.AccommodationPackages.ToList();
        }

        public IEnumerable<AccommodationPackage> SearchAccommodationPackages(string searchTerm, int? propertyTypeID, int page, int recordSize)
        {
            var context = new PropertyManagerContext();

            var accommodationPackages = context.AccommodationPackages.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                accommodationPackages = accommodationPackages.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }


            if (propertyTypeID.HasValue && propertyTypeID.Value > 0)
            {
                accommodationPackages = accommodationPackages.Where(a => a.PropertyTypeID == propertyTypeID.Value);
            }

            var skip = (page - 1) * recordSize;

            return accommodationPackages.OrderBy(x=>x.PropertyTypeID).Skip(skip).Take(recordSize).ToList();
        }

        public int SearchAccommodationPackagesCount(string searchTerm, int? propertyTypeID)
        {
            var context = new PropertyManagerContext();

            var accommodationPackages = context.AccommodationPackages.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                accommodationPackages = accommodationPackages.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }


            if (propertyTypeID.HasValue && propertyTypeID.Value > 0)
            {
                accommodationPackages = accommodationPackages.Where(a => a.PropertyTypeID == propertyTypeID.Value);
            }

            return accommodationPackages.Count();
        }


        public AccommodationPackage GetAllPropertyTypesByID(int ID)
        {
            using (var context = new PropertyManagerContext())
            {
                return context.AccommodationPackages.Find(ID);
            }



        }


        public bool SavePropertyType(AccommodationPackage accommodationPackage)
        {
            var context = new PropertyManagerContext();

            context.AccommodationPackages.Add(accommodationPackage);

            return context.SaveChanges() > 0;
        }

        public bool UpdateAccommodationPackage(AccommodationPackage accommodationPackage)
        {
            var context = new PropertyManagerContext();

            context.Entry(accommodationPackage).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;
        }

        public bool DeleteAccommodationPackage(AccommodationPackage accommodationPackage)
        {
            var context = new PropertyManagerContext();

            context.Entry(accommodationPackage).State = System.Data.Entity.EntityState.Deleted;

            return context.SaveChanges() > 0;
        }
    }
}
