using Microsoft.Extensions.Logging;
using Sahara.Common;
using Sahra.DataLayer.IRepository;
using Sahra.DataLayer.Models.DTO;
using Sahra.DataLayer.Models.Entities;
using Sahra.Services.IService;
using Sahra.ViewModel.SliderImage;
using System;
using System.Threading.Tasks;

namespace Sahra.Services.Service
{
    public class SliderImageService : ISliderImageService
    {
        private readonly ISliderImageRepository _sliderImageRepository;
        private readonly ILogger<SliderImageService> _logger;
        public SliderImageService(ISliderImageRepository sliderImageRepository, ILogger<SliderImageService> logger)
        {
            _sliderImageRepository = sliderImageRepository;
            _logger = logger;
        }

        public async Task<Result<SliderImage>> AddImageToSlider(AddImageSliderViewModel addImageSliderViewModel)
        {
            try
            {
                if (addImageSliderViewModel.SlideImage == null)
                    return Result.Failed<SliderImage>("عکس وجود ندارد");

                if (!UploadFileExtension.CheckIfImageFile(addImageSliderViewModel.SlideImage))
                    return Result.Failed<SliderImage>("عکس معتبر نمی باشد");

                var si = new SliderImage();
                si.SliderId = addImageSliderViewModel.SliderId;
                si.SlideAlt = addImageSliderViewModel.SlideAlt;
                si.Title = addImageSliderViewModel.SlideTitle;
                si.CreateDate = DateTime.Now;

                var UploadImg = await UploadFileExtension.WriteFile("slider", addImageSliderViewModel.SlideImage);
                si.Image = UploadImg;
                var add = await _sliderImageRepository.AddImageToSlider(si);
                return add;
            }


            catch (Exception ex)
            {
                _logger.LogError(ex, "AddImageToSlider");
                return Result.Failed<SliderImage>("خطا در اضافه کردن عکس!!");
            }
        }
        public async Task<Result<SliderImage>> DeleteSliderImage(int id)
        {
            try
            {
                var delete = await _sliderImageRepository.GetSliderByIdImg(id);

                if (delete.Succeeded && delete.Value?.Image != null)
                    UploadFileExtension.DeleteFile("slider", delete.Value.Image);

                return await _sliderImageRepository.DeleteSliderImage(id);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "DeleteSliderImage");
                return Result.Failed<SliderImage>("خطا در حذف اسلاید عکس!!");
            }
        }

        public async Task<Result<SliderImage>> GetSliderByIdImg(int id)
        {
            return await _sliderImageRepository.GetSliderByIdImg(id);
        }

        public async Task<Result<ShowUpdateSliderImageViewModel>> UpdateSlide(UpdateImageSliderViewModel updateImageSliderViewModel)
        {
            try
            {
                var up = await _sliderImageRepository.GetSliderByIdImg(updateImageSliderViewModel.SlideId);

                if (up.NotSucceeded)
                    return Result.Failed<ShowUpdateSliderImageViewModel>("یافت نشد");

                if (up.Value == null)
                    return Result.Failed<ShowUpdateSliderImageViewModel>("یافت نشد");

                if (updateImageSliderViewModel.Image != null)
                {
                    UploadFileExtension.DeleteFile("Slider", up.Value.Image);
                    var newName = await UploadFileExtension.WriteFile("Slider", updateImageSliderViewModel.Image);
                    up.Value.Image = newName;
                }

                var dto = new UpdateImageDTO();
                dto.Title = updateImageSliderViewModel.SliderTitle;
                dto.Alt = updateImageSliderViewModel.Alt;
                dto.Image = up.Value.Image;
                dto.Id = updateImageSliderViewModel.SlideId;
                var result = await _sliderImageRepository.UpdateSlider(dto);

                var map = new ShowUpdateSliderImageViewModel();
                map.SliderId = result.Value.SliderId;
                map.SlideAlt = result.Value.SlideAlt;
                map.Title = result.Value.Title;
                map.Image = result.Value.Image;
                return Result.Success(map);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "UpdateSlide");
                return Result.Failed<ShowUpdateSliderImageViewModel>("خطا در به روز رسانی اسلایدر!!");
            }

        }
    }
}
