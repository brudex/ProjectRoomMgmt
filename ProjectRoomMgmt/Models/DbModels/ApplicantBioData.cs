using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectRoomMgmt.Models.DbModels
{
    public class ApplicantBioData
    {
        public ApplicantBioData() { }

        public ApplicantBioData(AdmissionViewModel vm)
        {
            FirstName = vm.FirstName;
            LastName = vm.LastName;
            OtherNames = vm.OtherNames;
            FullName = string.Format("{0} {1} {2}", vm.FirstName, vm.OtherNames, vm.LastName);
            CountryOfOrigin = vm.CountryOfOrigin;
            Nationality = vm.Nationality;
            CityOfOrigin = vm.CityOfOrigin;
            Occupation = vm.Occupation;
            OriginatingInstitute = vm.OriginatingInstitute;
            CreatedAt= DateTime.Now;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OtherNames { get; set; }
        public string FullName { get; set; }
        public string CountryOfOrigin { get; set; }
        public string Nationality { get; set; }
        public string CityOfOrigin { get; set; }
        public string Occupation { get; set; }
        public string OriginatingInstitute { get; set; } //e.g., Police, Immigration etc.
        public DateTime CreatedAt { get; set; }
        public void Save()
        {
            if (Id == 0)
            {
                Id = DbHandler.Instance.Save(this);
            }
            else
            {
                DbHandler.Instance.Update(this);
            }
          
        }
    }
}