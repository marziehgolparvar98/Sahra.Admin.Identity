using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sahara.Common;
using Sahra.DataLayer.IRepository;
using Sahra.DataLayer.Models.DbContext;
using Sahra.DataLayer.Models.DTO;
using Sahra.DataLayer.Models.Entities;
using System.Threading.Tasks;

namespace Sahra.DataLayer.Repository
{
    public class SliderImageRepository : ISliderImageRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger<SliderImageRepository> _logger;
        public SliderImageRepository(ApplicationDbContext applicationDbContext, ILogger<SliderImageRepository> logger)
        {
            _applicationDbContext = applicationDbContext;
            _logger = logger;
        }
        public async Task<Result<SliderImage>> AddImageToSlider(SliderImage sliderImage)
        {
            _applicationDbContext.SliderImages.Add(sliderImage);
            await _applicationDbContext.SaveChangesAsync();
            return Result.Success(sliderImage);
        }

        public async Task<Result<SliderImage>> DeleteSliderImage(int id)
        {
            try
            {
                var DelSlideImg = await _applicationDbContext.SliderImages.FirstOrDefaultAsync(current => current.Id == id);
                if (DelSlideImg != null)
                {
                    _applicationDbContext.SliderImages.Remove(DelSlideImg);
                    await _applicationDbContext.SaveChangesAsync();
                    return Result.Success(DelSlideImg);
                }
                return Result.Failed<SliderImage>("عکسی با این آیدی یافت نشد.");
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "DeleteSliderImage");
                return Result.Failed<SliderImage>("خطا در حذف عکس!!");
            }

        }

        public async Task<Result<SliderImage>> GetSliderByIdImg(int id)
        {
            try
            {
                var result = await _applicationDbContext.SliderImages.Include(x => x.Slider).FirstOrDefaultAsync(current => current.Id == id);
                return Result.Success(result);
            }
            catch (System.Exception ex)
            {

                _logger.LogError(ex, "GetSliderByIdImg");
                return Result.Failed<SliderImage>("عکسی با این آیدی یافت نشد.");
            }

        }

        public async Task<Result<SliderImage>> UpdateSlider(UpdateImageDTO updateImageDTO)
        {
            try
            {
                var slide = await _applicationDbContext.SliderImages.FirstOrDefaultAsync(current => current.Id == updateImageDTO.Id);
                if (slide != null)
                {
                    slide.SlideAlt = updateImageDTO.Alt;
                    slide.Image = updateImageDTO.Image;
                    slide.Title = updateImageDTO.Title;
                    await _applicationDbContext.SaveChangesAsync();
                    return Result.Success(slide);
                }
                return Result.Failed<SliderImage>("تغییری اعمال نشد.");
            }
            catch (System.Exception ex)
            {

                _logger.LogError(ex, "UpdateSlider");
                return Result.Failed<SliderImage>("خطا در بروز رسانی اسلایدر!!");
            }
        }
    }
}
