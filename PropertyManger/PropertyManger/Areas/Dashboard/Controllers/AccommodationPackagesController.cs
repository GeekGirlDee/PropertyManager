using PropertyManager.Services;
using PropertyManger.Areas.Dashboard.ViewModels;
using PropertyManger.Entities;
using PropertyManger.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PropertyManger.Areas.Dashboard.Controllers
{
    public class AccommodationPackagesController : Controller
    {
       
        AccommodationPackageService accommodationPackageService = new AccommodationPackageService();
        PropertyTypesService propertyTypesService = new PropertyTypesService();
        public ActionResult Index(string searchTerm, int? propertyTypeID, int? page)
        {
            int recordSize = 5;
            page = page ?? 1;

            AccommodationPackagesListingModel model = new AccommodationPackagesListingModel();

            model.SearchTerm = searchTerm;
            model.PropertyTypeID = propertyTypeID;

            model.AccommodationPackages = accommodationPackageService.SearchAccommodationPackages(searchTerm, propertyTypeID, page.Value, recordSize);

            model.PropertyTypes = propertyTypesService.GetAllPropertyTypes();

            var totalRecords = accommodationPackageService.SearchAccommodationPackagesCount(searchTerm, propertyTypeID);

            model.Pager = new Pager(totalRecords, page, recordSize);

            return View(model);
        }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            AccommodationPackagesActionModel model = new AccommodationPackagesActionModel();

            if (ID.HasValue)
            {
                var accommodationPackage = accommodationPackageService.GetAllPropertyTypesByID(ID.Value);

                model.ID = accommodationPackage.ID;
                model.PropertyTypeID = accommodationPackage.PropertyTypeID;
                model.Name = accommodationPackage.Name;
                model.NoOfRooms = accommodationPackage.NoOfRooms;
                model.FeePerMonth = accommodationPackage.FeePerMonth;
            }

            model.PropertyTypes = propertyTypesService.GetAllPropertyTypes();

            return PartialView("_Action", model);
        }


        [HttpPost]
        public JsonResult Action(AccommodationPackagesActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;

            if (model.ID > 0)
            {
                var accommodationPackage = accommodationPackageService.GetAllPropertyTypesByID(model.ID);

                accommodationPackage.PropertyTypeID = model.PropertyTypeID;
                accommodationPackage.Name = model.Name;
                accommodationPackage.NoOfRooms = model.NoOfRooms;
                accommodationPackage.FeePerMonth = model.FeePerMonth;

                result = accommodationPackageService.UpdateAccommodationPackage(accommodationPackage);
            }
            else
            {
                AccommodationPackage accommodationPackage = new AccommodationPackage();

                accommodationPackage.PropertyTypeID = model.PropertyTypeID;
                accommodationPackage.Name = model.Name;
                accommodationPackage.NoOfRooms = model.NoOfRooms;
                accommodationPackage.FeePerMonth = model.FeePerMonth;

                result = accommodationPackageService.SavePropertyType(accommodationPackage);
            }

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action Property Types." };
            }

            return json;
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            AccommodationPackagesActionModel model = new AccommodationPackagesActionModel();

            var accommodationPackage = accommodationPackageService.GetAllPropertyTypesByID(ID);

            model.ID = accommodationPackage.ID;


            return PartialView("_Delete", model);
        }

        [HttpPost]
        public JsonResult Delete(AccommodationPackagesActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;

            var propertyType = accommodationPackageService.GetAllPropertyTypesByID(model.ID);

            result = accommodationPackageService.DeleteAccommodationPackage(propertyType);

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action Property Types." };
            }

            return json;
        }
    }
}
