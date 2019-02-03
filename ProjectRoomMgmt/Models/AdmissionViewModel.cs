using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;
using ProjectRoomMgmt.Models.DbModels;

namespace ProjectRoomMgmt.Models
{
    public class AdmissionViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OtherNames { get; set; }
        public string FullName { get; set; }
       
        public string CountryOfOrigin { get; set; }
        public string Nationality { get; set; }
        public string CityOfOrigin { get; set; }
        public string Occupation { get; set; }
        public string OriginatingInstitute { get; set; } //e.g., Police, Immigration etc.
        public string CourseName { get; set; } //Name of course being applied for
        public string AdmissionStatus { get; set; } //Pending, Rejected, Accepted
         


        public int SaveAdmission()
        {
            var biodata = new ApplicantBioData(this);
            biodata.Save();
            var admission = new AdmissionApplication();
            admission.CourseName = CourseName;
            admission.CreatedAt = DateTime.Now;
            admission.BioDataId = biodata.Id;
            admission.AdmissionStatus = Constants.AdmissionStatus.Pending;
           return DbHandler.Instance.Save(admission); 
        }

        public static ServiceResponse SearchAdmissions(JObject data)
        {
            var response = new ServiceResponse();
            response.Status = "00";
            var searchMode = data["searchMode"].ToStringOrEmpty();
            var list = new List<AdmissionViewModel>();
            switch (searchMode)
            {
                case "list":
                {
                     list = DbHandler.Instance.GetAdmissions();
                     break;
                }
                case "bydate":
                {
                    string fromDate = data["fromDate"].ToStringOrEmpty();
                    string toDate = data["toDate"].ToStringOrEmpty();
                    list = DbHandler.Instance.SearchAdmissionsByDate(fromDate,toDate);
                    break;

                }
                case "byname":
                {
                    string text = data["text"].ToStringOrEmpty();
                    list = DbHandler.Instance.SearchAdmissionsByDescription(text);
                    break; 
                }
                case "bystatus":
                {
                    string text = data["status"].ToStringOrEmpty();
                    list = DbHandler.Instance.SearchAdmissionsByStatus(text);
                    break;
                }
            }
            response.data = list;
            return response;

        }
    }
}