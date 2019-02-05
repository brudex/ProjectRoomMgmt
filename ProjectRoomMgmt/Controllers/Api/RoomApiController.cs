using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using ProjectRoomMgmt.Models;

namespace ProjectRoomMgmt.Controllers.Api
{
    public class RoomApiController : ApiController
    {

        public ServiceResponse RoomBooking([FromBody]JObject data)
        {
            var handler = new RoomViewModel(data);
            var response =  handler.DoRoomBooking();
            return response;
            
        }
    }
}