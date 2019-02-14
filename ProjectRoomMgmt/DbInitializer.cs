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

        public static void InitializeRooms()
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
    }
}