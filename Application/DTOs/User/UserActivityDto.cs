using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.User
{
    public class UserActivityDto
    {
        public string Type { get; set; }
        public string Activity {  get; set; }
        public DateTime Date { get; set; }
    }
}
