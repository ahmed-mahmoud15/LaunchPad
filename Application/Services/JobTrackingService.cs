using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Job_Tracker;
using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;

namespace Application.Services
{
    public class JobTrackingService : IJobTrackingService
    {
        private readonly IUnitOfWork unit;
        private readonly IUserCvService cvService;

        public JobTrackingService(IUnitOfWork unit, IUserCvService cv)
        {
            this.unit = unit;
            this.cvService = cv;
        }

        public async Task<Result> ChangeJobStatus(int userId, ChangeJobTrackStatusDto dto)
        {
            var user = await unit.Users.GetByIdAsync(userId);
            if(user is null)
            {
                return Result.NotFound("User not found");
            }

            if(dto is null)
            {
                return Result.BadRequest("DTO is null");
            }

            var jobTrack = await unit.JobTracks.GetByIdAsync(dto.JobTrackId);

            if(jobTrack is null)
            {
                return Result.NotFound("Job not found");
            }

            if (jobTrack.CurrentStatus != dto.OldStatus)
            {
                return Result.BadRequest("Previous status didn't match");
            }

            if(dto.OldStatus == dto.NewStatus)
            {
                return Result.BadRequest("Status didn't change");
            }

            jobTrack.CurrentStatus = dto.NewStatus;

            var application = new ApplicationHistory { 
                From = dto.OldStatus,
                To = dto.NewStatus,
                Notes = dto.Notes,
                UpdatedAt = DateTime.UtcNow,
                JobTrackId = dto.JobTrackId
            };

            await unit.JobTracks.UpdateAsync(jobTrack);
            await unit.ApplicationHistory.AddAsync(application);
            await unit.SaveChangesAsync();
            return Result.Ok();
        }

        public async Task<Result> CreateTrackedJob(int userId, CreateJobTrackDto dto)
        {
            var user = await unit.Users.GetByIdAsync(userId);
            if (user is null)
            {
                return Result.NotFound("User not found");
            }

            if (dto is null)
            {
                return Result.BadRequest("DTO can't be null");
            }
            int cvId;
            if (dto.CvId is not null)
            {
                var cv = await unit.UserCvs.FindAsync(c => c.Id == dto.CvId);
                cvId = cv.Id;
            }
            else if (dto.NewCvDto is not null)
            {
                var cvResult = await cvService.UploadCvAsync(userId, dto.NewCvDto);
                if (cvResult is not null && cvResult.IsSuccess)
                {
                    cvId = cvResult.Value;
                }
                else
                {
                    return Result.BadRequest(cvResult.ErrorMessage);
                }
            }
            else
            {
                return Result.BadRequest("You must upload cv or select one of yours");
            }

            var newJob = new Job
            {
                UserId = userId,
                Title = dto.JobTitle,
                Info = dto.JobDescription,
                Type = dto.JobType.ToLower() switch
                {
                    "full-time" or "fulltime" => JobType.FullTime,
                    "part-time" or "parttime" => JobType.PartTime,
                    "freelance" => JobType.Freelance,
                    "remote" => JobType.Remote,
                    "internship" => JobType.Internship,
                    _ => JobType.FullTime
                },
                CvId = cvId
            };

            var newJobTrack = new JobTrack
            {
                CompanyName = dto.CompanyName,
                Location = dto.Location,
                Salary = dto.Salary ?? 0,
                JobUrl = dto.JobUrl,
                AppliedAt = DateTime.UtcNow,
                CurrentStatus = dto.Status.ToLower() switch
                {
                    "applied" => ApplicationStatus.Applied,
                    "assessment" => ApplicationStatus.Assessment,
                    "interview" => ApplicationStatus.Interview,
                    "shortlisted" or "short-listed" => ApplicationStatus.Shortlisted,
                    "rejected" => ApplicationStatus.Rejected,
                    "accecpted" => ApplicationStatus.Accepted,
                    _ => ApplicationStatus.NoAction
                },
                Job = newJob
            };

            foreach (var jobSkill in dto.Skills)
            {
                var skill = await unit.Skills.FindAsync(s => jobSkill.Name.ToUpper().Equals(s.Name.ToUpper()));

                skill ??= new Skill { Name = jobSkill.Name }; // if skill is null then create new skill with this name

                newJobTrack.SkillsRequired.Add(new JobSkill
                {
                    Skill = skill,
                    JobTrack = newJobTrack,
                    RequiredLevel = jobSkill.Level.ToLower() switch
                    {
                        "begineer" => SkillLevel.Beginner,
                        "intermediate" => SkillLevel.Intermediate,
                        "expert" => SkillLevel.Expert,
                        _ => SkillLevel.Beginner
                    }
                });
            }

            var application = new ApplicationHistory
            {
                JobTrack = newJobTrack,
                UpdatedAt = DateTime.UtcNow,
                Notes = dto.Notes,
                From = ApplicationStatus.NoAction,
                To = newJobTrack.CurrentStatus
            };

            await unit.JobTracks.AddAsync(newJobTrack);
            await unit.ApplicationHistory.AddAsync(application);
            await unit.SaveChangesAsync();


            return Result.NoContent();
        }

        public async Task<Result> DeleteTrackedJob(int userId, int jobTrackId)
        {
            var user = await unit.Users.GetByIdAsync(userId);
            if(user is null)
            {
                return Result.NotFound("User not found");
            }

            var job = await unit.JobTracks.GetByIdAsync(jobTrackId);
            if (job is null)
            {
                return Result.NotFound("Job not found");
            }

            await unit.ApplicationHistory.DeleteAllByJobTrackIdAsync(jobTrackId);
            await unit.Skills.DeleteJobSkillsByJobIdAsync(jobTrackId);
            await unit.JobTracks.DeleteAsync(jobTrackId);

            await unit.SaveChangesAsync();

            return Result.NoContent();
        }

        public async Task<Result<ViewJobTrackDetailsDto>> DisplayJobHistory(int userId, int jobTrackId)
        {
            var user = await unit.Users.GetByIdAsync(userId);
            if (user is null)
            {
                return Result<ViewJobTrackDetailsDto>.NotFound("User not found");
            }

            var jobTrack = await unit.JobTracks.GetJobTracksWithIncludes(jobTrackId);

            if (jobTrack is null)
            {
                return Result<ViewJobTrackDetailsDto>.NotFound("Job not found");
            }

            var result = new ViewJobTrackDetailsDto
            {
                Id = jobTrack.Id,
                AppliedDate = jobTrack.AppliedAt,
                CompanyName = jobTrack.CompanyName,
                JobDescription = jobTrack.Job.Info,
                JobTitle = jobTrack.Job.Title,
                JobType = jobTrack.Job.Type.ToString(),
                Status = jobTrack.CurrentStatus.ToString(),
                Location = jobTrack.Location ?? " - ",
                History = new List<JobTrackHistoryDetailsDto>(),
                Cv = jobTrack.Job.Cv.FileName // temp
            };

            foreach(var application in jobTrack.History)
            {
                result.History.Add(new JobTrackHistoryDetailsDto {
                    OldStatus = application.From,
                    NewStatus = application.To,
                    Date = application.UpdatedAt,
                    Notes = application.Notes
                });
            }

            return Result<ViewJobTrackDetailsDto>.Ok(result);
        }
    }
}
