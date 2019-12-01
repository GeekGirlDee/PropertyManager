using PropertyManager.Data;
using PropertyManger.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManager.Services
{
    public class AccommodationsService
    {
        public IEnumerable<Accommodation> GetAllPropertyTypes()
        {
            var context = new PropertyManagerContext();

            return context.Accommodations.ToList();
        }

        public IEnumerable<Accommodation> SearchAccommodations(string searchTerm, int? accommodationPackageID, int? page, int recordSize)
        {
            var context = new PropertyManagerContext();

            var accommodation = context.Accommodations.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                accommodation = accommodation.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                accommodation = accommodation.Where(a => a.Description.ToLower().Contains(searchTerm.ToLower()));
            }


            if (accommodationPackageID.HasValue && accommodationPackageID.Value > 0)
            {
                accommodation = accommodation.Where(a => a.AccommodationPackageID == accommodationPackageID.Value);
            }

            var skip = (page - 1) * recordSize;

            return accommodation.OrderBy(x => x.AccommodationPackageID).Skip(skip.Value).Take(recordSize).ToList(); 
        }

        public int SearchAccommodationPackagesCount(string searchTerm, int? AccommodationPackageID)
        {
            var context = new PropertyManagerContext();

            var accommodation = context.Accommodations.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                accommodation = accommodation.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }


            if (AccommodationPackageID.HasValue && AccommodationPackageID.Value > 0)
            {
                accommodation = accommodation.Where(a => a.AccommodationPackageID == AccommodationPackageID.Value);
            }

            return accommodation.Count();
        }


        public Accommodation GetAccommodationByID(int ID)
        {
            using (var context = new PropertyManagerContext())
            {
                return context.Accommodations.Find(ID);
            }



        }


        public bool SaveAccommodation(Accommodation accommodation)
        {
            var context = new PropertyManagerContext();

            context.Accommodations.Add(accommodation);

            return context.SaveChanges() > 0;
        }

        public bool UpdateAccommodation(Accommodation accommodation)
        {
            var context = new PropertyManagerContext();

            context.Entry(accommodation).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;
        }

        public bool DeleteAccommodation(Accommodation accommodation)
        {
            var context = new PropertyManagerContext();

            context.Entry(accommodation).State = System.Data.Entity.EntityState.Deleted;

            return context.SaveChanges() > 0;
        }
    }
}

