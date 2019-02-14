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
        public  string StudentNo { get; set; }
        public  string RoomNo { get; set; }
        public  int RoomId{ get; set; }
        public  DateTime EvacuationDate{ get; set; }
        public RoomViewModel() { }
        

        public ServiceResponse DoRoomBooking(JObject data)
        {
            RoomId = data["roomId"].ToInteger();
            StudentNo = data["studentNo"].ToStringOrEmpty();
            EvacuationDate = data["evacuationDate"].ToDateTime();
            var response = new ServiceResponse();
            response.Status = "03";
            var room = DbHandler.Instance.GetById<Room>(RoomId);
            var roomAllocations = DbHandler.Instance.GetRoomBookingsByRoomId(room.Id);
            var student = DbHandler.Instance.GetTrainingStudentByNo(StudentNo);
            if (room.Capacity > roomAllocations.Count)
            {
                if (student != null)
                {
                    var booking = new RoomBooking();
                    booking.Active = true;
                    booking.RoomId = room.Id;
                    booking.BookingDate=DateTime.Now;
                    // booking.BookingStatus = "Active";
                    booking.StudentNo = StudentNo;
                    booking.TrainingStudentId = student.StudentId;
                    booking.ExpectedVacationDate = EvacuationDate;
                    DbHandler.Instance.Save(booking);
                    response.Status = "00";
                    response.Message = "Booking successful";
                } 

            }
         
            return response;
        }
    }
}