using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RawData
{
    public class CvEvaluationRaw
    {
        public Dictionary<int, int> BelowAverage = new Dictionary<int, int>();
        public Dictionary<int, int> AboveAverage = new Dictionary<int, int>();
    }
}
