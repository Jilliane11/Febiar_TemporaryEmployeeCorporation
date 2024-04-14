using System.ComponentModel.DataAnnotations;

namespace TemporaryEmployeeCorporation_1.Core;

public class JobHistory
{
    public int JobHistoryId { get; set; }
    public DateTime DateAssigned { get; set; }
    public DateTime? DateEnded { get; set; }
    public string JobTitle { get; set; }
    public string Description { get; set; }
    public string CompanyName { get; set; }
    //relationships
    public int? PlacementId { get; set; }
    public Placement? PlacementLink { get; set; }
    public int CandidateId { get; set; }
    public Candidate CandidateLink { get; set; }

}