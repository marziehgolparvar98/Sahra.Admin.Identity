using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sahara.Common;
using Sahra.DataLayer.IRepository;
using Sahra.DataLayer.Models.DTO.SolutionDTO;
using Sahra.DataLayer.Models.Entities;
using Sahra.Services.IService;
using Sahra.ViewModel.Solution;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sahra.Services.Service
{
    public class SolutionService : ISolutionService
    {
        private readonly ILogger<SolutionService> _logger;
        private readonly ISolutionRepository _solutionRepository;
        public SolutionService(ISolutionRepository solutionRepository, ILogger<SolutionService> logger)
        {
            _solutionRepository = solutionRepository;
            _logger = logger;

        }
        public async Task<Result<GetSolutionDTO>> AddSolution(AddSolution addSolution)
        {
            try
            {

                var aS = new Solution();
                aS.Title = addSolution.Title;
                aS.Description = addSolution.Description;
                aS.Image = addSolution.Image;
                aS.CreateDate = DateTime.Now;
                aS.SolutionName = addSolution.SolutionName;
                var mtd = new SolutionMetaDataDTO();
                mtd.SolutionDetails = addSolution.SolutionDetails;
                mtd.SolutionCustomer = addSolution.SolutionCustomer;
                mtd.SolutionSubTitles = addSolution.SolutionSubTitles;
                aS.MetaData = JsonConvert.SerializeObject(mtd);

                var result = await _solutionRepository.AddSolution(aS);

                var map = new GetSolutionDTO();
                map.Id = result.Value.Id;
                map.CreateDate = result.Value.CreateDate;
                map.Title = result.Value.Title;
                map.Description = result.Value.Description;
                map.Image = result.Value.Image;

                if (result.Value.MetaData != null)
                {
                    var meta = JsonConvert.DeserializeObject<SolutionMetaDataDTO>(result.Value.MetaData);

                    if (meta.SolutionSubTitles != null)
                    {
                        map.SolutionSubTitles = meta.SolutionSubTitles;
                    }
                    if (meta.SolutionDetails != null)
                    {
                        map.SolutionDetails = meta.SolutionDetails;
                    }
                    if (meta.SolutionCustomer != null)
                    {
                        map.SolutionCustomer = meta.SolutionCustomer;
                    }
                }
                aS.MetaData = JsonConvert.SerializeObject(mtd);

                return Result.Success(map);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "AddSolution");
                return Result.Failed<GetSolutionDTO>("خطا در ثبت راهکار!!");
            }
        }

        public async Task<Result<Solution>> DeleteSolution(int id)
        {
            try
            {

                var delSol = await _solutionRepository.GetSolutionById(id);
                return await _solutionRepository.DeleteSolution(id);
            }
            catch (Exception)
            {

                return Result.Failed<Solution>("خطا در حذف راهکار!!");
            }
        }

        public async Task<Result<List<GetSolutionDTO>>> GetAllSolution(int page, int pageSize)
        {
            try
            {
                var eSP = new List<GetSolutionDTO>();
                var skip = pageSize * page - pageSize;
                var take = pageSize;
                if (page < 1 || pageSize < 1)
                {
                    skip = 1;
                    take = 100;
                }

                var res = await _solutionRepository.GetAllSolution(skip, take);
                var result = new List<Solution>();
                foreach (var item in res.Value)
                {
                    var exi = new GetSolutionDTO();
                    exi.Id = item.Id;
                    exi.CreateDate = item.CreateDate;
                    exi.Description = item.Description;
                    exi.SolutionName = item.SolutionName;
                    exi.Title = item.Title;
                    exi.Image = item.Image;

                    if (item.MetaData != null)
                    {
                        var meta = JsonConvert.DeserializeObject<GetSolutionDTO>(item.MetaData);

                        if (meta.SolutionSubTitles != null)
                        {
                            exi.SolutionSubTitles = meta.SolutionSubTitles;
                        }

                        if (meta.SolutionCustomer != null)
                        {
                            exi.SolutionCustomer = meta.SolutionCustomer;
                        }

                        if (meta.SolutionDetails != null)
                        {
                            exi.SolutionDetails = meta.SolutionDetails;
                        }
                    }
                    eSP.Add(exi);
                }
                _logger.LogInformation("GetAllSolution");
                return Result.Success(eSP);
            }

            catch (Exception ex)
            {

                _logger.LogInformation(ex, "GetAllSolution");

                return Result.Failed<List<GetSolutionDTO>>("خطا در دریافت راهکار!!");
            }
        }

        public async Task<Result<GetSolutionDTO>> GetSolutionById(int Id)
        {
            var res = await _solutionRepository.GetSolutionById(Id);
            var exi = new GetSolutionDTO();

            exi.Description = res.Value.Description;
            exi.Id = res.Value.Id;
            exi.CreateDate = res.Value.CreateDate;
            exi.Title = res.Value.Title;
            exi.Image = res.Value.Image;
            exi.SolutionName = res.Value.SolutionName;
            if (res.Value.MetaData != null)
            {
                var meta = JsonConvert.DeserializeObject<SolutionMetaDataDTO>(res.Value.MetaData);


                if (meta.SolutionDetails != null)
                {
                    exi.SolutionDetails = meta.SolutionDetails;
                }

                if (meta.SolutionSubTitles != null)
                {
                    exi.SolutionSubTitles = meta.SolutionSubTitles;
                }

                if (meta.SolutionCustomer != null)
                {
                    exi.SolutionCustomer = meta.SolutionCustomer;
                }
            }
            return Result.Success(exi);

        }

        public async Task<Result<Solution>> UpdateSoluton(UpdateSolutionViewModel updateSolutionViewModel)
        {
            try
            {
                var upSol = await _solutionRepository.GetSolutionById(updateSolutionViewModel.Id);

                if (upSol == null)
                    return Result.Failed<Solution>("یافت نشد");

                var dto = new UpdateSolutionDTO();

                var mtd = new SolutionMetaDataDTO();
                mtd.SolutionCustomer = updateSolutionViewModel.SolutionCustomer;
                mtd.SolutionDetails = updateSolutionViewModel.SolutionDetails;
                mtd.SolutionSubTitles = updateSolutionViewModel.SolutionSubTitles;

                dto.MetaData = JsonConvert.SerializeObject(mtd);
                dto.Id = updateSolutionViewModel.Id;
                dto.Title = updateSolutionViewModel.Title;
                dto.Description = updateSolutionViewModel.Description;
                dto.Image = upSol.Value.Image;
                dto.SolutionName = updateSolutionViewModel.SolutionName;

                return await _solutionRepository.UpdateSolution(dto);
            }
            catch (Exception)
            {
                return Result.Failed<Solution>("خطا در به روز رسانی!!");
            }
        }

    }
}

