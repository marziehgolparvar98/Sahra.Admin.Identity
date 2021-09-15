using Sahara.Common;
using Sahra.DataLayer.Models.Entities;
using Sahra.ViewModel.Event;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sahra.Services.IService
{
    public interface IEventService
    {
        Task<Result<List<Event>>> GetAllEvent(int page, int pageSize);
        Task<Result<Event>> GetEventById(int Id);
        Task<Result<Event>> AddEvent(AddEventViewModel addEventViewModel);
        Task<Result<Event>> UpdateEvent(UpdateEventViewModel updateEventViewModel);
        Task<Result<Event>> DeleteEvent(int id);
    }
}
