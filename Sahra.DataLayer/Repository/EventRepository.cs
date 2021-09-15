using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sahara.Common;
using Sahra.DataLayer.IRepository;
using Sahra.DataLayer.Models.DbContext;
using Sahra.DataLayer.Models.DTO;
using Sahra.DataLayer.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sahra.DataLayer.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger<EventRepository> _logger;
        public EventRepository(ApplicationDbContext applicationDbContext, ILogger<EventRepository> logger)
        {
            _applicationDbContext = applicationDbContext;
            _logger = logger;
        }

        public async Task<Result<Event>> AddEvent(Event _event)
        {
            _applicationDbContext.Events.Add(_event);
            await _applicationDbContext.SaveChangesAsync();
            return Result.Success(_event);
        }

        public async Task<Result<Event>> DeleteEvent(int id)
        {
            try
            {
                var result = await _applicationDbContext.Events.FirstOrDefaultAsync(c => c.Id == id);
                if (result != null)
                {
                    _applicationDbContext.Events.Remove(result);
                    await _applicationDbContext.SaveChangesAsync();
                    return Result.Success(result);
                }
                return Result.Failed<Event>("رویدادی با این آیدی یافت نشد.");
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "DeleteEvent");
                return Result.Failed<Event>("خطا در حذف رویداد.");
            }

        }

        public async Task<Result<List<Event>>> GetAllEvent(int skip, int take)
        {
            var result = await _applicationDbContext.Events.OrderByDescending(x => x.Id).Skip(skip).Take(take).ToListAsync();
            return Result.Success(result);
        }

        public async Task<Result<Event>> GetEventById(int Id)
        {
            var result = await _applicationDbContext.Events.FirstOrDefaultAsync(c => c.Id == Id);
            return Result.Success(result);
        }

        public async Task<Result<Event>> UpdateEvent(UpdateEventDTO updateEventDto)
        {
            try
            {
                var upEve = await _applicationDbContext.Events.FirstOrDefaultAsync(current => current.Id == updateEventDto.Id);
                if (upEve != null)
                {
                    upEve.Title = updateEventDto.Title;
                    upEve.Image = updateEventDto.Image;
                    upEve.Holder = updateEventDto.Holder;
                    upEve.Start = updateEventDto.Start;
                    upEve.Finish = updateEventDto.Finish;
                    upEve.IsOnline = updateEventDto.IsOnline;
                    upEve.Description = updateEventDto.Description;
                    await _applicationDbContext.SaveChangesAsync();
                    return Result.Success(upEve);
                }
                return Result.Failed<Event>("تغییری اعمال نشد.");
            }
            catch (System.Exception ex)
            {

                _logger.LogError(ex, "UpdateEvent");
                return Result.Failed<Event>("خطا در بروز رسانی رویداد!!");
            }
        }
    }
}
