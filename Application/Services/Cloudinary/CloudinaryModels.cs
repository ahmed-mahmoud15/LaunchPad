using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Cloudinary
{
    public record StorageUploadResult(string PublicId, string Url, string FileName, long Size);

    public enum StorageFolder { Cvs, Audios, Videos }
}
