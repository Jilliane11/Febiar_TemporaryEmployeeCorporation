using System.ComponentModel.DataAnnotations;

namespace TemporaryEmployeeCorporation_1.Core;

public class Candidate
{
    [Key]
    public int CandidateId { get; set; }
    public string CandidateName { get; set; }
    public string HomeAddress { get; set; }
    public string TelephoneNumber { get; set; }
    public string EmailAddress { get; set; }
    public string WorkAddress { get; set; }

    //relationships
    public ICollection<Placement>? Placements { get; set; }
    public ICollection<Certificate> Certificates { get; set; }
    public ICollection<JobHistory>? JobHistories { get; set; }
    public ICollection<Attendance>? Attendances { get; set; }


}