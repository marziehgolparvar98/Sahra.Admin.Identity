using Sahara.Common;
using Sahra.DataLayer.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sahra.DataLayer.IRepository
{
    public interface ISliderRepository
    {
        Task<Result<List<Slider>>> GetAllSlider();
        Task<Result<Slider>> GetSliderById(int id);
        Task<Result<Slider>> AddSlider(Slider slider);
        Task<Result<Slider>> DeleteSlider(int id);
    }
}
