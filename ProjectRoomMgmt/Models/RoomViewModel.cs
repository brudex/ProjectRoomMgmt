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
        public string RoomType { get; set; }
        public string RoomName { get; set; }
        public string BlockLocation { get; set; }
        public string ImageUrl { get; set; }
        public int Capacity { get; set; }
        public string Status { get; set; } //Occupied, Free/Empty, Partially occupied

        public List<RoomBooking> Bookings { get; set; } 
        public RoomViewModel() { }


        public RoomViewModel(Room room)
        { 
             RoomNo = room.RoomNo;
             RoomId = room.Id;
             RoomType = room.RoomType;
             RoomName = room.RoomName;
             BlockLocation= room.BlockLocation;
             ImageUrl =room.ImageUrl;
             Capacity  = room.Capacity;
             Status = room.Status;
        }

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
                    booking.OccupantName = student.FullName;
                    booking.TrainingStudentId = student.StudentId;
                    booking.ExpectedVacationDate = EvacuationDate;
                    DbHandler.Instance.Save(booking);
                    response.Status = "00";
                    response.Message = "Booking successful";
                    room.Status = Constants.RoomStatus.Booked;
                    DbHandler.Instance.Save(room);
                } 

            }
         
            return response;
        }
    }
}