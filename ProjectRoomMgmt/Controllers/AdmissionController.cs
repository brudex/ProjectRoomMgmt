using System.Web.Mvc;
using ProjectRoomMgmt.Models;

namespace ProjectRoomMgmt.Controllers
{
    public class AdmissionController : Controller
    {
        // GET: Admission
        public ActionResult Capture() 
        {
            //Todo Capture a form to create and addmission using the model below
            var vm = new AdmissionViewModel();
            return View(vm);
        }
 
        [HttpPost]
        public ActionResult Capture(AdmissionViewModel model)
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
            //Todo Capture a form to update an admission to Accepted or Rejected
            var vm = new AdmissionViewModel();
            return View(vm);
        }

        public ActionResult Search()
        {
            //Todo Capture a form to search an admission by date and name of applicant 
            var vm = new AdmissionViewModel();
            return View(vm);
        }
    }
}