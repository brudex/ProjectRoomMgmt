using System;


namespace ProjectRoomMgmt.Models.DbModels
{
    public class TrainingStudent
    {
        public TrainingStudent(){}

        public TrainingStudent(AdmissionApplication applicant)
        { 
            BioDataId = applicant.BioDataId;
            StudentNo = Constants.GetUniqueStudentNo();
            CourseName = applicant.CourseName;
            CreatedAt = DateTime.Now; 
        }

        public int Id { get; set; }
        public int BioDataId { get; set; }
        public int StudentNo { get; set; }
        public string CourseName { get; set; }
        public DateTime CreatedAt { get; set; }

        public void Save()
        {
            if (Id == 0)
            { 
                DbHandler.Instance.Save(this);
            }
            else
            { 
                DbHandler.Instance.Update(this);
            }
        }
    }
}