using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    public interface ICvRequest
    {
        int? CvId { get; }
        IFormFile? File { get; }
    }
}
