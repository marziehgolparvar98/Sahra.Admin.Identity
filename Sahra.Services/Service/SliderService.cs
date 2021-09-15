using Sahara.Common;
using Sahra.DataLayer.IRepository;
using Sahra.DataLayer.Models.Entities;
using Sahra.Services.IService;
using Sahra.ViewModel.Slider;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sahra.Services.Service
{
    public class SliderService : ISliderService
    {
        private readonly ISliderRepository _sliderRepository;

        public SliderService(ISliderRepository sliderRepository)
        {
            _sliderRepository = sliderRepository;

        }
        public async Task<Result<Slider>> AddSlider(AddSliderViewModel addSliderViewModel)
        {
            var slide = new Slider();
            slide.SliderDescription = addSliderViewModel.SliderDescription;
            return await _sliderRepository.AddSlider(slide);
        }

        public async Task<Result<Slider>> DeleteSlider(int id)
        {
            await _sliderRepository.GetSliderById(id);
            return await _sliderRepository.DeleteSlider(id);
        }

        public async Task<Result<List<Slider>>> GetAllSlider()
        {
            return await _sliderRepository.GetAllSlider();
        }

        public async Task<Result<Slider>> GetSliderById(int id)
        {
            return await _sliderRepository.GetSliderById(id);
        }
    }
}
