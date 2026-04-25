using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Application.Services.Cloudinary
{
    public class CloudinaryStorageService : IStorageService
    {
        private readonly CloudinaryDotNet.Cloudinary cloudinary;
        private readonly IConfiguration config;

        public CloudinaryStorageService(IConfiguration config)
        {
            this.config = config;
            var account = new Account(
                config["Cloudinary:CloudName"],
                config["Cloudinary:ApiKey"],
                config["Cloudinary:ApiSecret"]
            );
            cloudinary = new CloudinaryDotNet.Cloudinary(account);
            //cloudinary.Api.Secure = true;
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

        public string GetUrl(string publicId)
        {
            var cloudName = cloudinary.Api.Account.Cloud;
            return $"https://res.cloudinary.com/{cloudName}/raw/upload/{publicId}";
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
