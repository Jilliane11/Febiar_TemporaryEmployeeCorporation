using Microsoft.EntityFrameworkCore;
using TemporaryEmployeeCorporation_1.Configurations;
using TemporaryEmployeeCorporation_1.Core;

namespace TemporaryEmployeeCorporation_1
{
    public class TECContext:DbContext
    {
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Requirement> Requirements { get; set; }
        public DbSet<JobHistory> JobHistories { get; set; }
        public DbSet<Opening> Openings { get; set; }
        public DbSet<Placement> Placements { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }
        public DbSet<Session> Sessions { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder op)
        {
            if (!op.IsConfigured)
            {
                op.UseSqlServer(
                    @"Server=LAPTOP-DADDFU3M\SQLSOFTWARE;Initial Catalog=TEC;TrustServerCertificate=True;Trusted_Connection=True");
            }
        }
        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.ApplyConfiguration(new AttendanceConfiguration());
            mb.ApplyConfiguration(new CandidateConfiguration());
            mb.ApplyConfiguration(new CertificateConfiguration());
            mb.ApplyConfiguration(new CompanyConfiguration());
            mb.ApplyConfiguration(new CourseConfiguration());
            mb.ApplyConfiguration(new RequirementConfiguration());
            mb.ApplyConfiguration(new JobHistoryConfiguration());
            mb.ApplyConfiguration(new OpeningConfiguration());
            mb.ApplyConfiguration(new QualificationConfiguration());
            mb.ApplyConfiguration(new SessionConfiguration());
            mb.ApplyConfiguration(new PlacementConfiguration());
        }
    }
}