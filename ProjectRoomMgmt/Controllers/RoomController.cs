using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectRoomMgmt.Models;

namespace ProjectRoomMgmt.Controllers
{
    public class RoomController : Controller
    {
        // GET: Room
        public ActionResult Booking()
        {
            var vm = new RoomViewModel();
            return View();
        }
    }
}