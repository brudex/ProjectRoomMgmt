﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;
using ProjectRoomMgmt.Models.DbModels;

namespace ProjectRoomMgmt.Models
{
    public class StudentViewModel
    {
        public int StudentId { get; set; }
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
        public string StudentNo { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set; }
        public string PrimaryContactMobile { get; set; }
        public  string PrimaryContactName { get; set; }
        public string EmailAddress { get; set; }
        public string HomeAddress { get; set; }
        public string PostalAddress { get; set; }
        public DateTime DateOfBirth { get; set; }

         

         

        public static ServiceResponse SearchStudent(JObject data)
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