using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemporaryEmployeeCorporation_1.Core;

namespace TemporaryEmployeeCorporation_1.Configurations
{
    public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> b)
        {
            b.ToTable("Attendance");

            b.HasData(new Attendance { AttendanceId = 1, CandidateId = 1, SessionId = 1, DateAttended = new DateTime(2020,09,11,13,0,0), SessionDescription = "1st Session: Data Types" });
            b.HasData(new Attendance { AttendanceId = 2, CandidateId = 2, SessionId = 1 ,DateAttended = new DateTime(2020,09,11,15,0,0), SessionDescription = "1st Session: Data Types" });
            b.HasData(new Attendance { AttendanceId = 3, CandidateId = 1, SessionId = 2 ,DateAttended = new DateTime(2020,09,11,15,0,0), SessionDescription = "2nd Session: Classes and Objects" });
            b.HasData(new Attendance { AttendanceId = 4, CandidateId = 2, SessionId = 3 ,DateAttended = new DateTime(2020,09,11,15,0,0), SessionDescription = "1st Session: Proper Naming Convention" });
        }
    }
}
