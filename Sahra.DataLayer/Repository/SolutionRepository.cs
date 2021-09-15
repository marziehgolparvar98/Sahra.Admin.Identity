using Microsoft.EntityFrameworkCore;
using Sahara.Common;
using Sahra.DataLayer.IRepository;
using Sahra.DataLayer.Models.DbContext;
using Sahra.DataLayer.Models.DTO.SolutionDTO;
using Sahra.DataLayer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sahra.DataLayer.Repository
{
    public class SolutionRepository : ISolutionRepository
    {
        private readonly ESahraApplicationDbContext _eSahraApplicationDbContext;
        public SolutionRepository(ESahraApplicationDbContext eSahraApplicationDbContext)
        {
            _eSahraApplicationDbContext = eSahraApplicationDbContext;

        }

        public async Task<Result<Solution>> AddSolution(Solution solution)
        {

            await _eSahraApplicationDbContext.Solutions.AddAsync(solution);
            await _eSahraApplicationDbContext.SaveChangesAsync();
            return Result.Success(solution);
        }

        public async Task<Result<Solution>> DeleteSolution(int id)
        {
            try
            {
                var result = await _eSahraApplicationDbContext.Solutions.FirstOrDefaultAsync(c => c.Id == id);
                if (result != null)
                {
                    _eSahraApplicationDbContext.Solutions.Remove(result);
                    await _eSahraApplicationDbContext.SaveChangesAsync();
                    return Result.Success(result);
                }
                return Result.Failed<Solution>("راهکاری با این آیدی یافت نشد.");
            }
            catch (Exception)
            {
                return Result.Failed<Solution>("خطا در حذف راهکار.");
            }

        }

        public async Task<Result<List<Solution>>> GetAllSolution(int skip, int take)
        {
            var result = await _eSahraApplicationDbContext.Solutions.OrderByDescending(x => x.Id).Skip(skip).Take(take).ToListAsync();
            return Result.Success(result);
        }

        public async Task<Result<Solution>> GetSolutionById(int Id)
        {
            var result = await _eSahraApplicationDbContext.Solutions.FirstOrDefaultAsync(c => c.Id == Id);
            return Result.Success(result);
        }

        public async Task<Result<Solution>> UpdateSolution(UpdateSolutionDTO updateSolutionDTO)
        {
            try
            {
                var upS = await _eSahraApplicationDbContext.Solutions.FirstOrDefaultAsync(current => current.Id == updateSolutionDTO.Id);
                if (upS != null)
                {
                    upS.Title = updateSolutionDTO.Title;
                    upS.SolutionName = updateSolutionDTO.SolutionName;
                    upS.Image = updateSolutionDTO.Image;
                    upS.Description = updateSolutionDTO.Description;
                    upS.CreateDate = DateTime.Now;
                    upS.Image = updateSolutionDTO.Image;
                    upS.MetaData = updateSolutionDTO.MetaData;

                    await _eSahraApplicationDbContext.SaveChangesAsync();
                    return Result.Success(upS);
                }
                return Result.Failed<Solution>("تغییری اعمال نشد.");
            }
            catch (Exception)
            {

                return Result.Failed<Solution>("خطا در بروز رسانی محصول!!");
            }
        }
    }
}
