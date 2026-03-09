using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class HrQuestion
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string? ModelAnswer { get; set; }
    }
}
