using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using ProjectRoomMgmt.Models;

namespace ProjectRoomMgmt.Controllers.Api
{
    public class StudentApiController : ApiController
    {  

        [HttpGet]
        public ServiceResponse GetAdmission()
        {
            var response = new ServiceResponse();
            var list = DbHandler.Instance.GetTrainingStudents(100);
            response.Status = "00";
            response.data = list;
            return response;
        }




        [HttpPost]
        public ServiceResponse StudentInfo(JObject data)
        {
            string studentNo = data["studentNo"].ToStringOrEmpty();
            var student = DbHandler.Instance.GetTrainingStudentByNo(studentNo);
            var response = new ServiceResponse();
            if (student != null)
            {
                response.Message = student.FullName;
                response.Status = "00";
                response.data = student;
            }
            else
            {
                response.Status = "03";
            }
             return response;
        }


        [HttpPost]
        public ServiceResponse SearchStudents(JObject data)
        {
            var response = new ServiceResponse();
            response.Status = "00";
            var searchMode = data["searchMode"].ToStringOrEmpty();
            int limit = data["limit"].ToInteger();
            if(limit == 0)
            {
                limit = 100;
            }
            var list = new List<StudentViewModel>();
            switch (searchMode)
            {
                case "list":
                    {
                        list = DbHandler.Instance.GetTrainingStudents(limit);
                        break;
                    }
                case "bydate":
                    {
                        string fromDate = data["fromDate"].ToStringOrEmpty();
                        string toDate = data["toDate"].ToStringOrEmpty();
                        list = DbHandler.Instance.SearchStudentsByDate(fromDate, toDate);
                        break; 
                    }
                case "byname":
                    {
                        string text = data["text"].ToStringOrEmpty();
                        list = DbHandler.Instance.SearchStudentByDescription(text);
                        break;
                    }
                case "bystudentNo":
                    {
                        string text = data["status"].ToStringOrEmpty();
                        list = DbHandler.Instance.SearchStudentByNo(text);
                        break;
                    }
            }
            response.data = list;
            return response; 
        }


    }
}
