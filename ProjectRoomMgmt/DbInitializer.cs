using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Brudex.CodeFirst;
using ProjectRoomMgmt.Models;
using ProjectRoomMgmt.Models.DbModels;

namespace ProjectRoomMgmt
{
    public class DbInitializer
    {

        public static void InitDb()
        {
            Bcf<AdmissionApplication>.Migrate();
            Bcf<ApplicantBioData>.Migrate();
            Bcf<Room>.Migrate();
            Bcf<RoomBooking>.Migrate();
            Bcf<TrainingStudent>.Migrate();
            BrudexCodeFirst.RunMigrations("DefaultConnection",true,ConnectionType.SqlServer,MigrationOptions.RecreateOnlyChangedModels);
        }

        public static void InitializeTestRooms()
        {
           var room = new Room();
            room.BlockLocation = "Block one";
            room.Capacity = 1;
            room.RoomName = "Room One";
            room.RoomNo = "101";
            room.RoomType = "Type One";
            DbHandler.Instance.Save(room);

             room = new Room();
            room.BlockLocation = "Block two";
            room.Capacity = 2;
            room.RoomName = "Room two";
            room.RoomNo = "201";
            room.RoomType = "Type Two";
            DbHandler.Instance.Save(room);

        }


        public static void InitializeTestStudents()
        {
            var biodata = new ApplicantBioData();
            biodata.CreatedAt= DateTime.Now;
            biodata.CityOfOrigin = "Accra";
            biodata.FirstName = "Nana";
            biodata.LastName = "Safo";
            biodata.OtherNames = "Yaw";
            biodata.FullName = "Nana Safo";
            biodata.CountryOfOrigin = "Ghana";
            biodata.Nationality = "Ghanaian";
            biodata.Occupation = "Developer";
            biodata.OriginatingInstitute =  "Police";
            biodata.Gender = "Male";
            biodata.Mobile = "233";
            biodata.PrimaryContactMobile = "233";
            biodata.PrimaryContactName = "Philip";
            biodata.EmailAddress = "test@gmail.com";
            biodata.HomeAddress = "No 1 Accra";
            biodata.PostalAddress = "Box 1";
            biodata. DateOfBirth =DateTime.Now.AddYears(-25);
         
            
            var applicant = new AdmissionApplication();
            applicant.BioDataId = DbHandler.Instance.Save(biodata);
            applicant.CourseName = "Public Relations";
            applicant.AdmissionStatus = Constants.AdmissionStatus.Pending;
            applicant.CreatedAt= DateTime.Now;
            DbHandler.Instance.Save(applicant);


            biodata.CreatedAt = DateTime.Now;
            biodata.CityOfOrigin = "Kumasi";
            biodata.FirstName = "Robert";
            biodata.LastName = "Adwini";
            biodata.OtherNames = "";
            biodata.FullName = "Robert Adwini";
            biodata.CountryOfOrigin = "Ghana";
            biodata.Nationality = "Ghanaian";
             biodata.Occupation = "IT";
            biodata.OriginatingInstitute = "Immigration";
            biodata.Gender = "Male";
            biodata.Mobile = "233";
            biodata.PrimaryContactMobile = "233";
            biodata.PrimaryContactName = "Sabie";
            biodata.EmailAddress = "adwini@gmail.com";
            biodata.HomeAddress = "Oyarifa";
            biodata.PostalAddress = "Box 1";
            biodata.DateOfBirth = DateTime.Now.AddYears(-25);

            var s = new TrainingStudent();
            s.BioDataId = DbHandler.Instance.Save(biodata);
            s.CourseName = "Peace Making";
            s.StudentNo = Constants.GetUniqueStudentNo();
            s.CreatedAt=DateTime.Now;
            s.Save();
            

        }
    }
}