using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEC_App.Dto
{
    public class QualificationDescriptionDto
    {

        public int QualificationId { get; set; }
        public string Description { get; set; }

        public QualificationDescriptionDto(int qualificationId, string description)
        {
            QualificationId = qualificationId;
            Description = description;
        }
    }
}
