using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using ProjectRoomMgmt.Models;

namespace ProjectRoomMgmt.Controllers.Api
{
    public class AdmissionApiController : ApiController
    {

        public ServiceResponse Search([FromBody]JObject data)
        {
            return AdmissionViewModel.SearchAdmissions(data);
        }


    }
}
