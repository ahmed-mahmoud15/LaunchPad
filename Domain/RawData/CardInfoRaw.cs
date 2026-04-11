using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RawData
{
    public class CardInfoRaw
    {
        public int TotalCount;
        public Dictionary<int, int> CountPerMonth = new Dictionary<int, int>();
    }
}
