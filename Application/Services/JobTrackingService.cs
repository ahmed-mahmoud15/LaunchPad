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

        public JobTrackingService(IUnitOfWork unit)
        {
            this.unit = unit;
        }

        public Task<Result> ChangeJobStatus(int userId, ChangeJobTrackStatusDto dto)
        {
            throw new NotImplementedException();
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

            var cv = dto.IsCvUploaded ? await unit.UserCvs.GetByIdAsync(dto.CvId.Value) : null;

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
                Cv = cv
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

                newJobTrack.SkillsRequired.Add(new JobSkill {
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
            await unit.SaveChangesAsync();


            return Result.NoContent();
        }

        public Task<Result> DeleteTrackedJob(int userId, int jobTrackId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<ViewJobTrackDetailsDto>> DisplayJobHistory(int userId, int jobTrackId)
        {
            throw new NotImplementedException();
        }
    }
}
