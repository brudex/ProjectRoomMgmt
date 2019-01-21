using System;


namespace ProjectRoomMgmt.Models.DbModels
{
    public class TrainingStudent
    {
        public int Id { get; set; }
        public int BioDataId { get; set; }
        public string StudentNo { get; set; }
       
        public DateTime CreatedAt { get; set; }   
    }
}