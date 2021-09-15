using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sahara.Common;
using Sahra.DataLayer.IRepository;
using Sahra.DataLayer.Models.DbContext;
using Sahra.DataLayer.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sahra.DataLayer.Repository
{
    public class SliderRepository : ISliderRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger<SliderRepository> _logger;
        public SliderRepository(ApplicationDbContext applicationDbContext, ILogger<SliderRepository> logger)
        {

            _applicationDbContext = applicationDbContext;
            _logger = logger;
        }

        public async Task<Result<Slider>> AddSlider(Slider slider)
        {
            _applicationDbContext.Sliders.Add(slider);
            await _applicationDbContext.SaveChangesAsync();
            return Result.Success(slider);
        }

        public async Task<Result<Slider>> DeleteSlider(int id)
        {
            try
            {
                var DelSlide = await _applicationDbContext.Sliders.FirstOrDefaultAsync(c => c.Id == id);
                if (DelSlide != null)
                {
                    _applicationDbContext.Sliders.Remove(DelSlide);
                    await _applicationDbContext.SaveChangesAsync();
                    return Result.Success(DelSlide);
                }
                return Result.Failed<Slider>("اسلایدری با این آیدی یافت نشد.");
            }
            catch (System.Exception ex)
            {

                _logger.LogError(ex, "DeleteSlider");
                return Result.Failed<Slider>("خطا در حذف اسلایدر!!");
            }
        }

        public async Task<Result<List<Slider>>> GetAllSlider()
        {
            var result =
                await _applicationDbContext.Sliders.ToListAsync();
            return Result.Success(result);
        }

        public async Task<Result<Slider>> GetSliderById(int id)
        {
            var result = await _applicationDbContext.Sliders.Include(x => x.SliderImage).FirstOrDefaultAsync(current => current.Id == id);
            if (result == null)
                return Result.Failed<Slider>("اسلایدی یافت نشد!!");
            return Result.Success(result);
        }
    }
}
