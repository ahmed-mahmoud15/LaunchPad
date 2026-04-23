using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Drive
{
    public enum DriveFolder
    {
        Cvs,
        Audios,
        Videos
    }

    public record DriveUploadResult(
        string FileId,
        string FileName,
        string MimeType,
        long? Size,
        string WebViewLink,
        string DownloadLink
    );

    public record DriveFileInfo(
        string FileId,
        string FileName,
        string MimeType,
        long? Size,
        string WebViewLink,
        DateTime? CreatedAt
    );
}
