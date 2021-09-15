using Microsoft.Extensions.Logging;
using Sahara.Common;
using Sahra.DataLayer.IRepository;
using Sahra.DataLayer.Models.DTO;
using Sahra.DataLayer.Models.Entities;
using Sahra.Services.IService;
using Sahra.ViewModel.Event;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sahra.Services.Service
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly ILogger<EventService> _logger;
        public EventService(IEventRepository eventRepository, ILogger<EventService> logger)
        {
            _eventRepository = eventRepository;
            _logger = logger;
        }
        public async Task<Result<Event>> AddEvent(AddEventViewModel addEventViewModel)
        {
            try
            {
                if (!UploadFileExtension.CheckIfImageFile(addEventViewModel.Image))
                    return Result.Failed<Event>("عکس آپلود شده غیر مجاز است");
                var em = new Event();
                em.Title = addEventViewModel.Title;
                em.Holder = addEventViewModel.Holder;
                em.Start = addEventViewModel.Start;
                em.CreateDate = DateTime.Now;
                em.IsOnline = addEventViewModel.IsOnline;
                em.Description = addEventViewModel.Description;
                em.Finish = addEventViewModel.Finish;
                if (addEventViewModel.Image.Length > 0)
                {
                    em.Image = await UploadFileExtension.WriteFile("Event", addEventViewModel.Image);
                }
                var result = await _eventRepository.AddEvent(em);
                return result;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "AddEvent");
                return Result.Failed<Event>("خطا در ثبت ایونت!!");
            }
        }

        public async Task<Result<Event>> DeleteEvent(int id)
        {
            try
            {
                var delEve = await _eventRepository.GetEventById(id);
                if (delEve.Value?.Image != null)
                    UploadFileExtension.DeleteFile("Event", delEve.Value.Image);
                return await _eventRepository.DeleteEvent(id);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "DeleteEvent");
                return Result.Failed<Event>("خطا در حذف ایونت!!");
            }
        }

        public async Task<Result<List<Event>>> GetAllEvent(int page, int pageSize)
        {
            var skip = pageSize * page - pageSize;
            var take = pageSize;
            if (page < 1 || pageSize < 1)
            {
                skip = 1;
                take = 100;
            }

            return await _eventRepository.GetAllEvent(skip, take);
        }

        public async Task<Result<Event>> GetEventById(int Id)
        {
            return await _eventRepository.GetEventById(Id);
        }

        public async Task<Result<Event>> UpdateEvent(UpdateEventViewModel updateEventViewModel)
        {
            try
            {
                var upEve = await _eventRepository.GetEventById(updateEventViewModel.Id);

                if (upEve == null)
                    return Result.Failed<Event>("یافت نشد");

                if (updateEventViewModel.Image != null)
                {
                    UploadFileExtension.DeleteFile("Event", upEve.Value.Image);
                    var newName = await UploadFileExtension.WriteFile("Event", updateEventViewModel.Image);
                    upEve.Value.Image = newName;
                }

                var dto = new UpdateEventDTO();
                dto.Id = updateEventViewModel.Id;
                dto.IsOnline = updateEventViewModel.IsOnline;
                dto.Title = updateEventViewModel.title;
                dto.Image = upEve.Value.Image;
                dto.Start = updateEventViewModel.Start;
                dto.Finish = updateEventViewModel.Finish;
                dto.Description = updateEventViewModel.Description;
                dto.Holder = updateEventViewModel.Holder;
                return await _eventRepository.UpdateEvent(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateEvent");
                return Result.Failed<Event>("خطا در به روز رسانی!!");


            }

        }
    }
}
