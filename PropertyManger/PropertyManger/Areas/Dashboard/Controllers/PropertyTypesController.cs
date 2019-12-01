using PropertyManager.Services;
using PropertyManger.Areas.Dashboard.ViewModels;
using PropertyManger.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PropertyManger.Areas.Dashboard.Controllers
{
    public class PropertyTypesController : Controller
    {
        PropertyTypesService propertyTypesService = new PropertyTypesService();
        public ActionResult Index(string searchTerm)
        {
            PropertyTypesListingModel model = new PropertyTypesListingModel();

            model.SearchTerm = searchTerm;

            model.PropertyTypes = propertyTypesService.SearchPropertyTypes(searchTerm);

            return View(model);
        }
        
        [HttpGet]
        public ActionResult Action(int? ID)
        {
            PropertyTypesActionModel model = new PropertyTypesActionModel();

            if (ID.HasValue)
            {
                var propertyType = propertyTypesService.GetAllPropertyTypesByID(ID.Value);

                model.ID = propertyType.ID;
                model.Name = propertyType.Name;
                model.Description = propertyType.Description;
            }

            return PartialView("_Action", model);
        }


        [HttpPost]
        public JsonResult Action(PropertyTypesActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;

            if (model.ID > 0)
            {
                var propertyType = propertyTypesService.GetAllPropertyTypesByID(model.ID);

                propertyType.Name = model.Name;
                propertyType.Description = model.Description;

                result = propertyTypesService.UpdatePropertyType(propertyType);
            }
            else
            {
                PropertyType propertyType = new PropertyType();

                propertyType.Name = model.Name;
                propertyType.Description = model.Description;

                result = propertyTypesService.SavePropertyType(propertyType);
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
            PropertyTypesActionModel model = new PropertyTypesActionModel();

            var propertyType = propertyTypesService.GetAllPropertyTypesByID(ID);

            model.ID = propertyType.ID;


            return PartialView("_Delete", model);
        }

        [HttpPost]
        public JsonResult Delete(PropertyTypesActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;

            var propertyType = propertyTypesService.GetAllPropertyTypesByID(model.ID);
                        
            result = propertyTypesService.DeletePropertyType(propertyType);

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