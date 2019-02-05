using ProjectRoomMgmt.Models.DbModels;

namespace ProjectRoomMgmt.Models
{
    public class AdmissionStatusViewModel
    {
        public string Status { get; set; }
        public int Id { get; set; }

        public ServiceResponse UpdateAdmissonStatus()
        {
            var response = new ServiceResponse("False","");
            var applicant = DbHandler.Instance.GetById<AdmissionApplication>(Id);
            if (applicant != null)
            {
                applicant.AdmissionStatus = Status;
                DbHandler.Instance.Update(applicant);
                if (applicant.AdmissionStatus == Constants.AdmissionStatus.Accepted)
                {
                    var trainingStudent = new TrainingStudent(applicant);
                    trainingStudent.Save();
                }
                response.Status = "00";
                response.Message = "Status updated";
            }
            return response;
        }
    }
}