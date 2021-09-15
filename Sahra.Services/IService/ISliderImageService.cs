using Sahara.Common;
using Sahra.DataLayer.Models.Entities;
using Sahra.ViewModel.SliderImage;
using System.Threading.Tasks;

namespace Sahra.Services.IService
{
    public interface ISliderImageService
    {
        Task<Result<SliderImage>> GetSliderByIdImg(int id);
        Task<Result<ShowUpdateSliderImageViewModel>> UpdateSlide(UpdateImageSliderViewModel updateImageSliderViewModel);
        Task<Result<SliderImage>> DeleteSliderImage(int id);
        Task<Result<SliderImage>> AddImageToSlider(AddImageSliderViewModel addImageSlider);
    }
}
