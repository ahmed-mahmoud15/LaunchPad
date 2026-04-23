using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Microsoft.Extensions.Configuration;

namespace Application.Services.Drive
{
    public class GoogleDriveService : IGoogleDriveService
    {
        private readonly DriveService drive;
        private readonly IConfiguration config;

        private static readonly string[] Fields =
        { "id", "name", "mimeType", "size", "webViewLink", "webContentLink", "createdTime" };

        public GoogleDriveService(IConfiguration config)
        {
            this.config = config;

            var credentialPath = config["GoogleDrive:CredentialsPath"];
            var credential = GoogleCredential.FromFile(credentialPath).CreateScoped(DriveService.ScopeConstants.Drive);

            drive = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "App_Files"
            });
        }

        public async Task DeleteAsync(string fileId)
        {
            await drive.Files.Delete(fileId).ExecuteAsync();
        }

        public async Task<Stream> DownloadAsync(string fileId)
        {
            var stream = new MemoryStream();
            await drive.Files.Get(fileId).DownloadAsync(stream);
            stream.Position = 0;
            return stream;
        }

        public async Task<DriveFileInfo> GetFileInfoAsync(string fileId)
        {
            var request = drive.Files.Get(fileId);
            request.Fields = string.Join(", ", Fields);
            var file = await request.ExecuteAsync();
            return MapToFileInfo(file);
        }

        public async Task<List<DriveFileInfo>> ListAsync(DriveFolder folder)
        {
            var folderId = ResolveFolderId(folder);
            var request = drive.Files.List();
            request.Q = $"'{folderId}' in parents and trashed=false";
            request.Fields = $"files({string.Join(", ", Fields)})";

            var result = await request.ExecuteAsync();
            return result.Files.Select(MapToFileInfo).ToList();
        }

        public async Task<DriveUploadResult> UploadAsync(Stream stream, string fileName, string mimeType, DriveFolder folder)
        {
            var folderId = ResolveFolderId(folder);
            var fileMetaData = new Google.Apis.Drive.v3.Data.File
            {
                Name = $"{Guid.NewGuid()}_{fileName}",
                Parents = new[] { folderId }
            };

            var request = drive.Files.Create(fileMetaData, stream, mimeType);
            request.Fields = string.Join(", ", Fields);

            var progress = await request.UploadAsync();

            if (progress.Status != Google.Apis.Upload.UploadStatus.Completed)
            {
                throw new Exception($"Drive upload failed: {progress.Exception?.Message}");
            }

            var file = request.ResponseBody;
            return new DriveUploadResult(
                file.Id,
                file.Name,
                file.MimeType,
                file.Size,
                file.WebViewLink,
                file.WebContentLink
            );
        }

        private string ResolveFolderId(DriveFolder folder)
        {
            var key = folder.ToString();
            return config[$"GoogleDrive:Folders:{key}"]
                ?? throw new InvalidOperationException($"No Drive Folder configured for '{key}'");
        }

        private static DriveFileInfo MapToFileInfo(Google.Apis.Drive.v3.Data.File file)
        {
            return new(file.Id, file.Name, file.MimeType, file.Size, file.WebViewLink, file.CreatedTimeDateTimeOffset?.UtcDateTime);
        }
    }
}
