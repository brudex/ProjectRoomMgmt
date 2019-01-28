using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using DapperExtensions;
 

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


        public List<AdmissionViewModel> GetAdmissions(int limit=100)
        {
            using (var conn = GetOpenDefaultDbConnection())
            {
                string sql =
                    "select top "+limit+" a.*,b.* from AdmissionApplication a inner join ApplicantBioData b on a.BioDataId=b.Id order by a.Id desc";
                return conn.Query<AdmissionViewModel>(sql).ToList();
            }
        }

        public List<AdmissionViewModel> SearchAdmissionsByDate(string fromDate, string toDate)
        {
            using (var conn = GetOpenDefaultDbConnection())
            {
                string sql =
                    "select a.*,b.* from AdmissionApplication a inner join ApplicantBioData b on a.BioDataId=b.Id where a.Created >= @fromDate and a.CreatedAt <= @toDate  order by a.Id desc";
                return conn.Query<AdmissionViewModel>(sql,new {fromDate,toDate}).ToList();
            }
        }

        public List<AdmissionViewModel> SearchAdmissionsByDescription(string text)
        {
            using (var conn = GetOpenDefaultDbConnection())
            {
                string sql =
                    string.Format("select a.*,b.* from AdmissionApplication a inner join ApplicantBioData b on a.BioDataId=b.Id where b.FullName like '%{0}%'  order by a.Id desc",text);
                return conn.Query<AdmissionViewModel>(sql).ToList();
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
    }
}
