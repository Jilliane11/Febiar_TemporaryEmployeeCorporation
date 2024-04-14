using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace TemporaryEmployeeCorporation_1.Core
{
    public class Attendance
    {
        public int AttendanceId { get; set; }
        public DateTime DateAttended { get; set; }
        public string? SessionDescription { get; set; }

        //relationships
        public int SessionId { get; set; }
        public Session SessionLink { get; set; }
        public int CandidateId { get; set; }
        public Candidate CandidateLink { get; set; }
    }
}
