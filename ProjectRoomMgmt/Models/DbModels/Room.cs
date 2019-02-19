using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectRoomMgmt.Models.DbModels
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomType { get; set; }
        public string RoomName { get; set; }
        public string RoomNo { get; set; }
        public string BlockLocation { get; set; }
        public int Capacity { get; set; } 
       
    }
}