using Sahara.Common;
using Sahra.DataLayer.Models.DTO;
using Sahra.DataLayer.Models.Entities;
using System.Threading.Tasks;

namespace Sahra.DataLayer.IRepository
{
    public interface ISliderImageRepository
    {
        Task<Result<SliderImage>> GetSliderByIdImg(int id);
        Task<Result<SliderImage>> UpdateSlider(UpdateImageDTO updateImageDTO);
        Task<Result<SliderImage>> DeleteSliderImage(int id);
        Task<Result<SliderImage>> AddImageToSlider(SliderImage sliderImage);
    }
}
