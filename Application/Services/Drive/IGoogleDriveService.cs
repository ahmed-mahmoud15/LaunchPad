using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Drive
{
    public interface IGoogleDriveService
    {
        public Task<DriveUploadResult> UploadAsync(Stream stream, string fileName, string mimeType, DriveFolder folder);
        public Task<Stream> DownloadAsync(string fileId);
        public Task DeleteAsync(string fileId);
        public Task<DriveFileInfo> GetFileInfoAsync(string fileId);
        public Task<List<DriveFileInfo>> ListAsync(DriveFolder folder);
    }
}
