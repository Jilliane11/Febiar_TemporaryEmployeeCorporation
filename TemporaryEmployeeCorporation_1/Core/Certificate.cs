using System.ComponentModel.DataAnnotations;

namespace TemporaryEmployeeCorporation_1.Core;

public class Certificate
{
    public int CertificateId { get; set; }
    public DateTime DateEarned { get; set; }
    public string Description { get; set; }

    //relationships
    public int QualificationId { get; set; }
    public Qualification QualificationLink { get; set; }
    public int CandidateId { get; set; }
    public Candidate CandidateLink { get; set; }

}