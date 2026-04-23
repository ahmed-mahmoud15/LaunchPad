using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Cloudinary
{
    public interface IStorageService
    {
        public Task<StorageUploadResult> UploadAsync(Stream stream, string fileName, StorageFolder folder);
        public Task DeleteAsync(string publicId);
        public string GetUrl(string publicId);
    }
}
