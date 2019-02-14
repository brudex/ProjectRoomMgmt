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
        public ActionResult RoomAllocation()
        {
            var studentNo = Request.QueryString["studentNo"];
            var roomNo = Request.QueryString["roomNo"];
            var vm = new RoomViewModel();
            vm.StudentNo = studentNo;
            vm.RoomNo = roomNo;
            return View(vm);
        }

        // GET: Room
        public ActionResult ManageBookings()
        {
            var vm = new RoomViewModel();
            return View();
        }

        // GET: Room
        public ActionResult ManageRooms()
        {
            var vm = new RoomViewModel();
            return View();
        }
    }
}