using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using ProjectRoomMgmt.Models;

namespace ProjectRoomMgmt.Controllers.Api
{
    public class StudentApiController : ApiController
    { 

        [HttpGet]
        public ServiceResponse GetTrainingStudents()
        {
            var response = new ServiceResponse();
            var list = DbHandler.Instance.GetTrainingStudents(100);
            response.Status = "00";
            response.data = list;
            return response;
        }


        [HttpGet]
        public ServiceResponse GetAdmission()
        {
            var response = new ServiceResponse();
            var list = DbHandler.Instance.GetTrainingStudents(100);
            response.Status = "00";
            response.data = list;
            return response;
        }

        public static ServiceResponse SearchStudents(JObject data)
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
                        list = DbHandler.Instance.SearchAdmissionsByDate(fromDate, toDate);
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
