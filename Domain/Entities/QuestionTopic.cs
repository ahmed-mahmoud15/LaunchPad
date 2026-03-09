using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class QuestionTopic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameNormalized { get
            {
                return Name.ToUpper();
            }
        }

        public ICollection<CodingQuestion> Questions { get; set; } = new List<CodingQuestion>();
    }
}
