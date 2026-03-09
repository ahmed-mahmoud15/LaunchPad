using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CvJobAnalysis
    {
        public int Id { get; set; }
        public int CvId { get; set; }
        public string JobDescription { get; set; }

        public string Feedback { get; set; }
        public int Score { get; set; }

        public UserCv Cv { get; set; }
    }
}
