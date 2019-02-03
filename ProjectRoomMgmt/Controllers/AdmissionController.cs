using System.Web.Mvc;
using ProjectRoomMgmt.Models;

namespace ProjectRoomMgmt.Controllers
{
    public class AdmissionController : Controller
    {
        // GET: Admission
        public ActionResult Capture() 
        {
            var vm = new AdmissionViewModel();
            return View(vm);
        }
 
        [HttpPost]
        public ActionResult Capture(AdmissionViewModel model)
        { 
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid data.");
                ViewBag.Error = "Error saving data";
                return View(model);
            }
            ViewBag.Success = "Data successfully saved";
            model.SaveAdmission();
            return View(model); 
        }

        public ActionResult AdmissionStatus()
        {
            //Todo Capture a form to update an admission to Accepted or Rejected
            var vm = new AdmissionViewModel();
            return View(vm);
        }

        public ActionResult Manage()
        {
            var vm = new AdmissionViewModel();  
            return View(vm);
        }
    }
}