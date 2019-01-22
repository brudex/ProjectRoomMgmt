using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectRoomMgmt.Models;
using ProjectRoomMgmt.Models.DbModels;

namespace ProjectRoomMgmt.Controllers
{
    public class AdmissionController : Controller
    {
        // GET: Admission
        public ActionResult Create() 
        {
            //Todo Create a form to create and addmission using the model below
            var vm = new AdmissionViewModel();
            return View(vm);
        }
 
        [HttpPost]
        public ActionResult Create(AdmissionViewModel model)
        {

            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid data.");
                return View(model);
            }
            model.SaveAdmission();
            return View(model);
            
        }

        public ActionResult AdmissionStatus()
        {
            //Todo Create a form to update an admission to Accepted or Rejected
            var vm = new AdmissionViewModel();
            return View(vm);
        }

        public ActionResult Search()
        {
            //Todo Create a form to search an admission by date and name of applicant 
            var vm = new AdmissionViewModel();
            return View(vm);
        }
    }
}