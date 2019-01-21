using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectRoomMgmt.Models.DbModels;

namespace ProjectRoomMgmt.Models
{
    public class AdmissionViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OtherNames { get; set; }
       
        public string CountryOfOrigin { get; set; }
        public string Nationality { get; set; }
        public string CityOfOrigin { get; set; }
        public string Occupation { get; set; }
        public string OriginatingInstitute { get; set; } //e.g., Police, Immigration etc.
        public string CourseName { get; set; } //Name of course being applied for
        public string AdmissionStatus { get; set; } //Pending, Rejected, Accepted


        public void SaveAdmission()
        {
            var biodata = new ApplicantBioData(this);
            biodata.Save();
            var admission = new AdmissionApplication();
            admission.BioDataId = biodata.Id;
            DbHandler.Instance.Save(admission); 
        }
    }
}