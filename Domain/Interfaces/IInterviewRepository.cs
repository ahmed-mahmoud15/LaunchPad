using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.RawData;

namespace Domain.Interfaces
{
    public interface IInterviewRepository : IRepository<Interview>
    {
        Task<CardInfoRaw> GetInterviewCountAsync();
    }
}
