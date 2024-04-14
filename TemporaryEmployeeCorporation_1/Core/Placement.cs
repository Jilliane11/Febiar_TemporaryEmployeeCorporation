namespace TemporaryEmployeeCorporation_1.Core;

public class Placement
{
    public int PlacementId { get; set; }
    public int TotalHoursWork { get; set; }
    public DateTime DateAssigned { get; set; }
    public string PlacementDescription { get; set; }

    //relationships
    public int CandidateId { get; set; }
    public Candidate CandidateLink { get; set; }
    public ICollection<JobHistory>? JobHistories { get; set; }
    public int OpeningId { get; set; }
    public Opening OpeningLink { get; set; }
}