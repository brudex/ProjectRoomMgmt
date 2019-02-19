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
            var list = DbHandler.Instance.GetAllRooms();
            var response = new ServiceResponse();
            response.data = list;
            response.Status = "00";
            return response; 
        }


    }
}