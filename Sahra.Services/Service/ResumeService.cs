using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sahara.Common;
using Sahra.DataLayer.IRepository;
using Sahra.DataLayer.Models.DTO.ResumeDTO;
using Sahra.DataLayer.Models.Entities;
using Sahra.Services.IService;
using Sahra.ViewModel.Resume;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sahra.Services.Service
{
    public class ResumeService : IResumeService
    {

        private readonly IResumeRepository _resumeRepository;
        private readonly ILogger<ResumeService> _logger;

        public ResumeService(IResumeRepository resumeRepository, ILogger<ResumeService> logger)
        {
            _resumeRepository = resumeRepository;
            _logger = logger;
        }
        public async Task<Result<List<GetResumeDTO>>> GetAllResume()
        {
            var res = await _resumeRepository.GetAllResume();
            var list = new List<GetResumeDTO>();
            foreach (var item in res.Value)
            {
                var exi = new GetResumeDTO();
                exi.Id = item.Id;
                exi.CreateDate = item.CreateDate;
                exi.Name = item.Name;
                exi.LastName = item.LastName;
                exi.Mobile = item.Mobile;
                exi.UploadFile = item.Image;

                if (item.MetaData != null)
                {
                    var meta = JsonConvert.DeserializeObject<GetResumeDTO>(item.MetaData);

                    if (meta.JobType != null)
                    {
                        exi.JobType = meta.JobType;
                    }
                }

                list.Add(exi);
            }
            return Result.Success(list);
        }
        public async Task<Result<GetResumeDTO>> GetResumeById(int id)
        {
            var res = await _resumeRepository.GetResumeById(id);
            var exi = new GetResumeDTO();


            exi.Id = res.Value.Id;
            exi.CreateDate = res.Value.CreateDate;
            exi.Name = res.Value.Name;
            exi.LastName = res.Value.LastName;
            exi.Mobile = res.Value.Mobile;
            exi.UploadFile = res.Value.Image;

            if (res.Value.MetaData != null)
            {
                var meta = JsonConvert.DeserializeObject<GetResumeDTO>(res.Value.MetaData);


                if (meta.JobType != null)
                {
                    exi.JobType = meta.JobType;
                }
            }
            return Result.Success(exi);
        }

        public async Task<Result<GetResumeDTO>> AddResume(AddResume addResume)
        {
            try
            {
                if (addResume.UploadFile != null)
                {

                    if (!UploadFileExtension.CheckIfIValidFile(addResume.UploadFile))
                        return Result.Failed<GetResumeDTO>("فایل آپلود شده غیر مجاز است!!");
                    if (addResume.UploadFile.Length / 8 / 1000 / 100 > 10)
                        return Result.Failed<GetResumeDTO>("حجم فایل آپلود شده بیش از حد مجاز است!!");

                }
                var ar = new Resume();

                ar.CreateDate = DateTime.Now;
                var mtd = new JobTypeDTO();

                mtd.JobType = addResume.JobType;
                ar.MetaData = JsonConvert.SerializeObject(mtd);
                ar.Name = addResume.Name;
                ar.LastName = addResume.LastName;
                ar.Mobile = addResume.Mobile;
                if (addResume.UploadFile.Length > 0)
                {
                    ar.Image = await UploadFileExtension.WriteFile("UploadFile", addResume.UploadFile);
                };
                var res = await _resumeRepository.AddResume(ar);
                var sR = new GetResumeDTO();
                sR.Name = res.Value.Name;
                sR.LastName = res.Value.LastName;
                sR.Mobile = res.Value.Mobile;

                if (res.Value.MetaData != null)
                {
                    var meta = JsonConvert.DeserializeObject<JobTypeDTO>(res.Value.MetaData);

                    if (meta.JobType != null)
                    {
                        sR.JobType = meta.JobType;
                    }
                }
                ar.MetaData = JsonConvert.SerializeObject(mtd);
                return Result.Success(sR);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AddResume");
                return Result.Failed<GetResumeDTO>("خطا در ثبت اطلاعات");
            }
        }

        public async Task<Result<UpdateResumeDTO>> UpdateResume(UpdateResumeViewModel update)
        {
            var newar = new UpdateResumeDTO();
            newar.Id = update.Id;
            newar.Name = update.Name;
            newar.LastName = update.LastName;
            newar.Mobile = update.Mobile;
            if (update.UploadFile.Length > 0)
            {
                newar.UploadFile = await UploadFileExtension.WriteFile("UploadFile", update.UploadFile);
            };
            var mtd = new GetResumeDTO();
            mtd.JobType = update.JobType;


            newar.MetaData = JsonConvert.SerializeObject(mtd);

            var res = await _resumeRepository.UpdateResume(newar);


            return Result.Success(newar);
        }
    }
}
