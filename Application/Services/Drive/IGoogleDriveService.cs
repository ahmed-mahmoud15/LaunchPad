using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Drive
{
    public interface IGoogleDriveService
    {
        Task<DriveUploadResult> UploadAsync(Stream stream, string fileName, string mimeType, DriveFolder folder);
        Task<Stream> DownloadAsync(string fileId);
        Task DeleteAsync(string fileId);
        Task<DriveFileInfo> GetFileInfoAsync(string fileId);
        Task<List<DriveFileInfo>> ListAsync(DriveFolder folder);
    }
}
