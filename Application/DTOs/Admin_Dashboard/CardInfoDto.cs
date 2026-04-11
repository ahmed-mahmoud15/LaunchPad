using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Admin_Dashboard
{
    public class CardInfoDto
    {
        public int TotalCount;
        public Dictionary<int, int> CountPerMonth = new Dictionary<int, int>();
    }
}
