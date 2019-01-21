using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectRoomMgmt.Models.DbModels
{
    public class AdmissionApplication
    {
        public int Id { get; set; }
        public int BioDataId { get; set; }
        public string CourseName { get; set; } //Name of course being applied for
        public string AdmissionStatus { get; set; } //Pending, Rejected, Accepted
    }
}