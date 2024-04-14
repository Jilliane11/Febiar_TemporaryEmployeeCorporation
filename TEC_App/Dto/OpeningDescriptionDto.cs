using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using TemporaryEmployeeCorporation_1.Core;

namespace TEC_App.Dto
{
    public class OpeningDescriptionDto
    {
        public int OpeningId { get; set; }
        public string OpeningDescription { get; set; }

        public OpeningDescriptionDto(int openingId, string openingDescription)
        {
            OpeningId = openingId;
            OpeningDescription = openingDescription;
        }
    }

    public class OpeningDetails
    {
        public int OpeningId { get; set; }
        public string OpeningDescription { get; set;}
        public double HourlyPay { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }

        public List<OpeningPlacement> PlacementList { get; set; } = new();
        public int CompanyId { get; set; }
        public string CompanyName { get; set;}

        public int QualificationId { get; set; }
        public string QualificationDescription { get; set; }

        public OpeningDetails(Opening opening)
        {
            if (opening.QualificationLink is null)
            {
                throw new InvalidOperationException("An opening should have one qualification");

            }

            if (opening.CompanyLink is null)
            {
                throw new InvalidOperationException("An opening should have a company");

            }

            OpeningId=opening.OpeningId;
            OpeningDescription=opening.OpeningDescription;
            HourlyPay = opening.HourlyPay;
            DateStart = opening.DateStart;
            DateEnd = opening.DateEnd;

            //placements
            var pb = new List<string>();
            PlacementList.Clear();
            foreach (var i in opening.Placements)
            {
                pb.Add(i.PlacementDescription);
                pb.Add($"{i.DateAssigned:yyyy MMMM dd}");
                pb.Add(i.TotalHoursWork.ToString());

                PlacementList.Add(new OpeningPlacement(i));

            }
            
            
            CompanyId = opening.CompanyId;
            CompanyName = opening.CompanyLink.CompanyName;

            QualificationId=opening.QualificationId;
            QualificationDescription=opening.QualificationLink.Description;



        }
        


    }

    public class OpeningPlacement
    {
        public int PlacementId { get; set; }
        public int TotalHoursWork { get; set; }
        public DateTime DateAssigned { get; set; }
        public string PlacementDescription { get; set; }

        public OpeningPlacement(Placement placement)
        {
            PlacementId = placement.PlacementId;
            PlacementDescription = placement.PlacementDescription;
            TotalHoursWork=placement.TotalHoursWork;
            DateAssigned = placement.DateAssigned;
        }
    }
}
