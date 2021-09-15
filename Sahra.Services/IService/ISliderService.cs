using Sahara.Common;
using Sahra.DataLayer.Models.Entities;
using Sahra.ViewModel.Slider;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sahra.Services.IService
{
    public interface ISliderService
    {
        Task<Result<List<Slider>>> GetAllSlider();
        Task<Result<Slider>> GetSliderById(int id);
        Task<Result<Slider>> AddSlider(AddSliderViewModel addSliderViewModel);
        Task<Result<Slider>> DeleteSlider(int id);
    }
}
