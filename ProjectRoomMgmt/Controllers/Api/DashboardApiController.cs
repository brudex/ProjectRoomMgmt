using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProjectRoomMgmt.Models;

namespace ProjectRoomMgmt.Controllers.Api
{
    /// <summary>
    /// 1 Admissions(Grouped by :Accepted, Pending, Rejected)
    /// 2 No of Students
    /// 3 No of Available Rooms
    /// 4 No of Allocated Rooms
    /// </summary>


   
    public class DashboardApiController : ApiController
    {

        /// <summary>
        /// 1 Admissions(Grouped by :Accepted, Pending, Rejected)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ServiceResponse GetAdmissionsCount()
        {
            var list = DbHandler.Instance.GetAdmissionApplications();
            var dict = new Dictionary<string,int>();
            dict[Constants.AdmissionStatus.Accepted] = list.Count(x => x.AdmissionStatus == Constants.AdmissionStatus.Accepted);
            dict[Constants.AdmissionStatus.Rejected] = list.Count(x => x.AdmissionStatus == Constants.AdmissionStatus.Rejected);
            dict[Constants.AdmissionStatus.Pending] = list.Count(x => x.AdmissionStatus == Constants.AdmissionStatus.Pending);
            var response = new ServiceResponse();
            response.Status = "00";
            response.data = dict;
            return response;
        }

        /// <summary>
        /// 2 No of Students
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ServiceResponse GetStudentsCount()
        {
            var count = DbHandler.Instance.GetStudentsCount();
            var response = new ServiceResponse();
            response.Status = "00";
            response.data = count;
            return response;
        }

        /// <summary>
        /// 3 No of Available Rooms
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ServiceResponse GetAvailableRoomsCount()
        {
            var count = 23; //Todo implement
            var response = new ServiceResponse();
            response.Status = "00";
            response.data = count;
            return response;
        }


        /// <summary>
        /// 4 No of Allocated Rooms
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ServiceResponse GetAllocatedRoomsCount()
        {
            var count = 23; //Todo implement
            var response = new ServiceResponse();
            response.Status = "00";
            response.data = count;
            return response;
        }




    }
}
