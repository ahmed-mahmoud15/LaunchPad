using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Cv;
using Domain.Common;

namespace Application.Interfaces
{
    public interface IUserCvService
    {
        public Task<Result<IEnumerable<UserCvDto>>> GetUserCvsAsync(int userId);
        public Task<Result> UploadCvAsync(int userId, UploadCvDto dto);
    }
}
