using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Cv;
using Application.Interfaces;
using Application.Services.Cloudinary;
using Application.Services.Drive;
using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class UserCvService : IUserCvService
    {
        private readonly IUnitOfWork unit;
        private readonly IStorageService drive;

        public UserCvService(IUnitOfWork unit, IStorageService drive)
        {
            this.unit = unit;
            this.drive = drive;
        }

        public async Task<Result<UserCvDto>> GetCvByIdAsync(int userId, int cvId)
        {
            var user = await unit.Users.GetByIdAsync(userId);
            if (user == null) {
                return Result<UserCvDto>.NotFound("User Not found");
            }
            var cv = await unit.UserCvs.GetByIdAsync(cvId);
            if(cv is null)
            {
                return Result<UserCvDto>.NotFound("Cv Not found");
            }

            if(cv.UserId != userId)
            {
                return Result<UserCvDto>.Unauthorized("You are not allowed to access this resouce");
            }

            var result = new UserCvDto
            {
                Id = cv.Id,
                UserId = userId,
                FileName = cv.FileName,
                FilePath = cv.FilePath,
                IsDefault = cv.IsDefault,
                Score = cv.Score,
                UploadedAt = cv.UploadedAt
            };


            return Result<UserCvDto>.Ok(result);
        }

        public async Task<Result<CvFileResult>> GetCvFileAsync(int userId, int cvId)
        {
            var cv = await unit.UserCvs.FindAsync(c => c.Id == cvId && c.UserId == userId);

            if(cv is null)
            {
                return Result<CvFileResult>.NotFound("CV not found");
            }

            try
            {
                var stream = await drive.DownloadAsync(cv.FilePath);

                return Result<CvFileResult>.Ok(new CvFileResult
                {
                    FileStream = stream,
                    ContentType = "application/pdf",
                    FileName = cv.FileName
                });
            }
            catch (Exception ex) {
                return Result<CvFileResult>.ServerError($"Could not retrieve file: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<UserCvDto>>> GetUserCvsAsync(int userId)
        {
            var cvs = await unit.UserCvs.FindAllAsync(c => c.UserId == userId);
            var result = new List<UserCvDto>();
            foreach (var c in cvs) {
                result.Add(new UserCvDto
                {
                    Id = c.Id,
                    UserId = userId,
                    FileName = c.FileName,
                    FilePath = c.FilePath,
                    IsDefault = c.IsDefault,
                    Score = c.Score,
                    UploadedAt = c.UploadedAt
                });
            }
            return Result<IEnumerable<UserCvDto>>.Ok(result);
        }

        public async Task<Result<int>> UploadCvAsync(int userId, UploadCvDto dto)
        {
            if(dto is null)
            {
                return Result<int>.BadRequest("DTO is null");
            }

            using var stream = dto.File.OpenReadStream();
            var result = await drive.UploadAsync(stream, dto.File.FileName, StorageFolder.Cvs);

            var cv = new UserCv
            {
                UserId = userId,
                FileName = dto.File.FileName,
                FilePath = result.Url,
                UploadedAt = DateTime.UtcNow,
                Score = 0, // till adding scoring functionality
                IsDefault = false
            };

            await unit.UserCvs.AddAsync(cv);
            await unit.SaveChangesAsync();
            return Result<int>.Ok(cv.Id);
        }
    }
}
