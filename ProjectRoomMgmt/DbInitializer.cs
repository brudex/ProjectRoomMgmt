using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Brudex.CodeFirst;
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
            BrudexCodeFirst.RunMigrations("DefaultConnection",true);
        }
    }
}