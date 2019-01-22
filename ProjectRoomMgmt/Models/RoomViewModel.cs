using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using ProjectRoomMgmt.Models.DbModels;

namespace ProjectRoomMgmt.Models
{
    public class RoomViewModel
    {
        public RoomViewModel(JObject data)
        {
            throw new NotImplementedException();
        }

        public ServiceResponse DoRoomBooking()
        {
                var booking = new RoomBooking();
            return null;
        }
    }
}