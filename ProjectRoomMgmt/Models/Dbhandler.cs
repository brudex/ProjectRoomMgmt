﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using DapperExtensions;
using ProjectRoomMgmt.Models.DbModels;


namespace ProjectRoomMgmt.Models
{
    public class DbHandler
    {
        static DbHandler instance = null;
        static readonly object padlock = new object();
        private readonly string DefaultConnectionString;

        DbHandler()
        {
            DefaultConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].
                ConnectionString;
        }



        public static DbHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new DbHandler();
                        }
                    }
                }
                return instance;
            }
        }



        public DbConnection GetOpenDefaultDbConnection()
        {
            var connection = new SqlConnection(DefaultConnectionString);
            connection.Open();
            return connection;
        }



        public int Save<T>(T data) where T : class
        {
            using (var conn = GetOpenDefaultDbConnection())
            {
                return conn.Insert(data);
            }
        }

        public bool Update<T>(T data) where T : class
        {
            using (var conn = GetOpenDefaultDbConnection())
            {
                return conn.Update(data);
            }
        }

        public T GetById<T>(int Id) where T : class
        {
            using (var conn = GetOpenDefaultDbConnection())
            {
                return conn.Get<T>(Id);
            }
        }
        public List<AdmissionViewModel> GetAdmissions(int limit = 100)
        {
            using (var conn = GetOpenDefaultDbConnection())
            {
                string sql =
                    "select top " + limit + " a.*,b.* ,a.Id 'AdmissionId' from AdmissionApplication a inner join ApplicantBioData b on a.BioDataId=b.Id order by a.Id desc";
                return conn.Query<AdmissionViewModel>(sql).ToList();
            }
        }

        public List<AdmissionApplication> GetAdmissionApplications()
        {
            using (var conn = GetOpenDefaultDbConnection())
            {
                string sql =
                    "select * from AdmissionApplication order by Id desc";
                return conn.Query<AdmissionApplication>(sql).ToList();
            }
        }

        public List<AdmissionViewModel> SearchAdmissionsByDate(string fromDate, string toDate)
        {
            using (var conn = GetOpenDefaultDbConnection())
            {
                string sql =
                    "select a.*,b.*,a.Id 'AdmissionId'  from AdmissionApplication a inner join ApplicantBioData b on a.BioDataId=b.Id where a.Created >= @fromDate and a.CreatedAt <= @toDate  order by a.Id desc";
                return conn.Query<AdmissionViewModel>(sql, new { fromDate, toDate }).ToList();
            }
        }


        public List<StudentViewModel> SearchStudentsByDate(string fromDate, string toDate)
        {
            using (var conn = GetOpenDefaultDbConnection())
            {
                string sql =
                    "select a.*,b.* from TrainingStudent a inner join ApplicantBioData b on a.BioDataId=b.Id where a.CreatedAt >= @fromDate and a.CreatedAt <= @toDate  order by a.Id desc";
                return conn.Query<StudentViewModel>(sql, new { fromDate, toDate }).ToList();
            }
        }

        public List<AdmissionViewModel> SearchAdmissionsByDescription(string text)
        {
            using (var conn = GetOpenDefaultDbConnection())
            {
                string sql =
                    string.Format("select a.*,b.*,a.Id 'AdmissionId'  from AdmissionApplication a inner join ApplicantBioData b on a.BioDataId=b.Id where b.FullName like '%{0}%'  order by a.Id desc", text);
                return conn.Query<AdmissionViewModel>(sql).ToList();
            }
        }


        public List<StudentViewModel> SearchStudentByDescription(string text)
        {
            using (var conn = GetOpenDefaultDbConnection())
            {
                string sql =
                    string.Format("select a.*,b.* from TrainingStudent a inner join ApplicantBioData b on a.BioDataId=b.Id where b.FullName like '%@searchText%' or a.StudentNo=@searchText  order by a.Id desc", text);
                return conn.Query<StudentViewModel>(sql, new { searchText = text }).ToList();
            }
        }

        public List<StudentViewModel> SearchStudentByNo(string studentNo)
        {
            using (var conn = GetOpenDefaultDbConnection())
            {
                string sql =
                    "select a.*,b.* from TrainingStudent a inner join ApplicantBioData b on a.BioDataId=b.Id where a.StudentNo=@studentNo  order by a.Id desc";
                return conn.Query<StudentViewModel>(sql, new { studentNo }).ToList();
            }
        }



        public List<AdmissionViewModel> SearchAdmissionsByStatus(string status)
        {
            using (var conn = GetOpenDefaultDbConnection())
            {
                string sql =
                    "select a.*,b.* from AdmissionApplication a inner join ApplicantBioData b on a.BioDataId=b.Id where a.AdmissionStatus=@status  order by a.Id desc";
                return conn.Query<AdmissionViewModel>(sql, new { status }).ToList();
            }
        }


        public List<StudentViewModel> GetTrainingStudents(int limit)
        {
            using (var conn = GetOpenDefaultDbConnection())
            {
                string sql =
                    "select top " + limit + " * from  TrainingStudent a inner join ApplicantBioData b on a.BioDataId=b.Id order by a.Id desc ";
                return conn.Query<StudentViewModel>(sql).ToList();
            }
        }

        public int GetMaxStudentNo()
        {
            using (var conn = GetOpenDefaultDbConnection())
            {
                string sql = "select max(StudentNo) 'int' from TrainingStudent";
                return conn.Query<int>(sql).FirstOrDefault();
            }
        }

        public StudentViewModel GetTrainingStudentByNo(string studentNo)
        {
            using (var conn = GetOpenDefaultDbConnection())
            {
                string sql =
                    "select top " + 1 + " a.*,a.Id 'StudentId',b.* from  TrainingStudent a inner join ApplicantBioData b on a.BioDataId=b.Id where a.StudentNo=@studentNo order by a.Id desc";
                return conn.Query<StudentViewModel>(sql, new { studentNo }).FirstOrDefault();
            }
        }

        public int GetStudentsCount()
        {
            using (var conn = GetOpenDefaultDbConnection())
            {
                string sql =
                    "select count(*) 'int' from TrainingStudent";
                return conn.Query<int>(sql).FirstOrDefault();
            }
        }

        public List<Room> GetAllRooms()
        {
            using (var conn = GetOpenDefaultDbConnection())
            {
                string sql = "select * from Room";
                return conn.Query<Room>(sql).ToList();
            }
        }

        public List<Room> GetAvailableRooms()
        {
            using (var conn = GetOpenDefaultDbConnection())
            {
                string sql = string.Format("select * from Room where status='{0}'", Constants.RoomStatus.Available);
                return conn.Query<Room>(sql).ToList();
            }
        }

        public List<RoomBooking> GetRoomBookingsByRoomId(int id)
        {
            using (var conn = GetOpenDefaultDbConnection())
            {
                string sql = "select * from RoomBooking where RoomId=@id and Active=1";
                return conn.Query<RoomBooking>(sql, new { id }).ToList();
            }
        }
    }
}
