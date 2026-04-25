using Microsoft.Extensions.Configuration;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Google.Apis.Http;
using HttpMethod = System.Net.Http.HttpMethod;

namespace Application.Services.Cloudinary
{
    public class CloudinaryStorageService : IStorageService
    {
        private readonly CloudinaryDotNet.Cloudinary cloudinary;
        private readonly HttpClient httpClient = new HttpClient();

        public CloudinaryStorageService(IConfiguration config)
        {
            var account = new Account(
                config["Cloudinary:CloudName"],
                config["Cloudinary:ApiKey"],
                config["Cloudinary:ApiSecret"]
            );
            cloudinary = new CloudinaryDotNet.Cloudinary(account);
            cloudinary.Api.Secure = true;
        }


        public async Task DeleteAsync(string publicId)
        {
            var result = await cloudinary.DestroyAsync(new DeletionParams(publicId)
            {
                ResourceType = ResourceType.Raw
            });
            if (result.Result != "ok")
            {
                throw new Exception($"Cloudinary delete failed: {result.Error?.Message}");
            }
        }

        public async Task<Stream> DownloadAsync(string fileUrl)
        {
            var cloudName = cloudinary.Api.Account.Cloud;
            var apiKey = cloudinary.Api.Account.ApiKey;
            var apiSecret = cloudinary.Api.Account.ApiSecret;

            // Extract PublicId from URL
            var uploadSegment = "/upload/";
            var uploadIndex = fileUrl.IndexOf(uploadSegment);
            var afterUpload = fileUrl.Substring(uploadIndex + uploadSegment.Length);

            if (afterUpload.StartsWith("v") && afterUpload.Contains("/"))
            {
                var versionPart = afterUpload.Substring(1, afterUpload.IndexOf("/") - 1);
                if (long.TryParse(versionPart, out _))
                    afterUpload = afterUpload.Substring(afterUpload.IndexOf("/") + 1);
            }

            var publicId = afterUpload;

            // Generate a signed archive download URL — this is authenticated and bypasses CDN restrictions
            var archiveParams = new ArchiveParams()
                .PublicIds(new List<string> { publicId })
                .ResourceType("raw")
                .Mode(ArchiveCallMode.Download);

            var signedUrl = cloudinary.DownloadArchiveUrl(archiveParams);

            Console.WriteLine($"[Cloudinary] Signed archive URL: {signedUrl}");

            // This signed URL is authenticated — fetch it server side
            var response = await httpClient.SendAsync(
                new HttpRequestMessage(HttpMethod.Get, signedUrl),
                HttpCompletionOption.ResponseHeadersRead
            );

            Console.WriteLine($"[Cloudinary] Archive download status: {response.StatusCode}");

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"[Cloudinary] Archive error: {body}");
                throw new Exception($"Failed to download: {response.StatusCode}");
            }

            // The archive returns a ZIP — extract the PDF from it
            var zipStream = await response.Content.ReadAsStreamAsync();
            var memoryStream = new MemoryStream();

            using (var archive = new System.IO.Compression.ZipArchive(zipStream, System.IO.Compression.ZipArchiveMode.Read))
            {
                var entry = archive.Entries.FirstOrDefault();
                if (entry == null)
                    throw new Exception("Archive is empty");

                Console.WriteLine($"[Cloudinary] Archive entry: {entry.Name}");

                using var entryStream = entry.Open();
                await entryStream.CopyToAsync(memoryStream);
            }

            memoryStream.Position = 0;
            return memoryStream;
        }

        public async Task<StorageUploadResult> UploadAsync(Stream stream, string fileName, StorageFolder folder)
        {
            var uploadParams = new RawUploadParams
            {
                File = new FileDescription(fileName, stream),
                Folder = folder.ToString(),
                UseFilename = false,
                UniqueFilename = true,
                Overwrite = false
            };

            var result = await cloudinary.UploadAsync(uploadParams);

            if (result.Error != null)
            {
                throw new Exception($"Cloudinary upload failed: {result.Error.Message}");
            }

            Console.WriteLine($"[Cloudinary] PublicId: {result.PublicId}");
            Console.WriteLine($"[Cloudinary] SecureUrl: {result.SecureUrl}");
            Console.WriteLine($"[Cloudinary] ResourceType: {result.ResourceType}");

            return new StorageUploadResult(result.PublicId, result.SecureUrl.ToString(), fileName, result.Bytes);
        }
    }
}
