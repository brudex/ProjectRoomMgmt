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
        private ServiceResponse _response;

        public AdmissionApiController() { }

        [HttpPost]
        [Route("api/AdmissionApi/Search")]
        public ServiceResponse Search([FromBody]JObject data)
        {
            return AdmissionViewModel.SearchAdmissions(data);
        }

        [HttpPost]
        [Route("api/AdmissionApi/Capture")]
        public ServiceResponse Capture(AdmissionViewModel model)
        {
            try
            {
//                if (!ModelState.IsValid)
//                {
                    var result = model.SaveAdmission();
                    _response = new ServiceResponse(result);

//                }
            }
            catch (Exception e)
            {
                _response = new ServiceResponse("False", e.Message);
            }

            return _response;

        }

        [HttpPost]
       public ServiceResponse SetAdmissionStatus(AdmissionStatusViewModel model)
        {
            try
            {
                _response = model.UpdateAdmissonStatus();
            }
            catch (Exception e)
            {
                _response = new ServiceResponse("False", e.Message);
            }

            return _response;

        }

    }
}
