using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using ProjectRoomMgmt.Models;

namespace ProjectRoomMgmt.Controllers.Api
{
    public class RoomApiController : ApiController
    {

        public ServiceResponse RoomBooking([FromBody]JObject data)
        {
            var handler = new RoomViewModel();
            var response =  handler.DoRoomBooking(data);
            return response;
        }



        [HttpGet]
        public ServiceResponse GetAvailableRooms()
        {
            var list = DbHandler.Instance.GetAvailableRooms();
            var response = new ServiceResponse();
            response.data = list;
            response.Status = "00";
            return response; 

        }

        [HttpGet] public ServiceResponse GetAllRooms()
        {
          
            var rooms = DbHandler.Instance.GetAllRooms();
            var list = new List<RoomViewModel>();
            foreach (var room in rooms)
            {
                var vm = new RoomViewModel(room);
                if (room.Status == Constants.RoomStatus.Booked)
                {
                    var booking = DbHandler.Instance.GetRoomBookingsByRoomId(room.Id);
                    vm.Bookings = booking;
                }

                list.Add(vm);
            }
            var response = new ServiceResponse();
            response.data = list;
            response.Status = "00";
            return response;
        }


    }
}