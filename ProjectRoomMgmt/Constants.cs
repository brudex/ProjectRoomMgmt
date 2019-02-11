using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using ProjectRoomMgmt.Models;

namespace ProjectRoomMgmt
{
    public class Constants
    {
        public static int StudentIdStartIndex = 200100;
        public class AdmissionStatus
        {
            public static string Pending = "Pending";
            public static string Rejected = "Rejected";
            public static string Accepted = "Accepted";
        }

        public static int GetUniqueStudentNo()
        {
            return Interlocked.Increment(ref StudentIdStartIndex);
        }

        public static void SetTradeRefCounterStart()
        {
            int max = DbHandler.Instance.GetMaxStudentNo();
            if (max > 0)
            {
                StudentIdStartIndex = max;
            }
        }

    }
}