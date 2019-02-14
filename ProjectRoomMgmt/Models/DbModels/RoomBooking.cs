using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectRoomMgmt.Models.DbModels
{
    public class RoomBooking
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int TrainingStudentId { get; set; }
        public string StudentNo { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime? ExpectedVacationDate { get; set; }
        public DateTime? VacatedAt { get; set; }
        public  string  BookingStatus{ get; set; }
        public  bool Active { get; set; }
    }
}