using Sahara.Common;
using Sahra.DataLayer.Models.DTO;
using Sahra.DataLayer.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sahra.DataLayer.IRepository
{
    public interface IEventRepository
    {
        Task<Result<List<Event>>> GetAllEvent(int skip, int take);
        Task<Result<Event>> GetEventById(int Id);
        Task<Result<Event>> AddEvent(Event _event);
        Task<Result<Event>> UpdateEvent(UpdateEventDTO updateEventDto);
        Task<Result<Event>> DeleteEvent(int id);
    }
}
