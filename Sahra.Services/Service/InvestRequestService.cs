using Microsoft.Extensions.Logging;
using Sahara.Common;
using Sahra.DataLayer.IRepository;
using Sahra.DataLayer.Models.DTO;
using Sahra.DataLayer.Models.Entities;
using Sahra.Services.IService;
using Sahra.ViewModel.InvestRequest;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sahra.Services.Service
{
    public class InvestRequestService : IInvestRequestService
    {
        private readonly IInvestRequestRepository _investRequestRepository;
        private readonly ILogger<InvestRequestService> _logger;

        public InvestRequestService(IInvestRequestRepository investRequestRepository, ILogger<InvestRequestService> logger)
        {
            _investRequestRepository = investRequestRepository;
            _logger = logger;
        }
        public async Task<Result<IList<ShowInvestRequestViewModel>>> GetAllInvestRequest()
        {
            try
            {
                var ir = new List<ShowInvestRequestViewModel>();
                var res = await _investRequestRepository.GetAllInvestRequest();
                foreach (var item in res.Value)
                {
                    var map = new ShowInvestRequestViewModel();
                    map.Id = item.Id;
                    map.Name = item.Name;
                    map.Mobile = item.Mobile;
                    map.Invest = item.Invest;
                    map.IsRegister = item.IsRegister;
                    map.IsActiveUser = item.IsActiveUser;
                    map.Need = item.Need;
                    map.HasProductIdea = item.HasProductIdea;
                    map.AgentName = item.AgentName;
                    map.CreatDate = item.CreateDate;
                    map.Description = item.Description;
                    map.Vision = item.Vision;
                    map.WebSiteAddress = item.WebSiteAddress;
                    map.TeamPersonCount = item.TeamPersonCount;
                    map.StartUpEmail = item.StartUpEmail;
                    map.RequestStatus = item.RequestStatus;
                    map.TrackingNumber = item.TrackingNumber;
                    map.UploadFile = new List<ShowUploadFileViewModel>();
                    if (item.UploadFile != null)
                    {
                        foreach (var x in item?.UploadFile)
                        {
                            map.UploadFile.Add(new ShowUploadFileViewModel() { UploadFile = x.UploadFileName });
                        }
                    }


                    map.Category = new List<ShowCategoryViewModel>();
                    if (item.Category != null)
                    {
                        foreach (var items in item.Category)
                        {
                            var x = new ShowCategoryViewModel();
                            x.Category = items.EnumCategory;
                            map.Category.Add(x);
                        }
                    }


                    map.Collabration = new List<ShowCollabrationViewModel>();
                    if (item.Collabration != null)
                    {
                        foreach (var x in item.Collabration)
                        {
                            map.Collabration.Add(new ShowCollabrationViewModel() { Collaboration = x.EnumCollaboration });
                        }
                    }


                    map.Platform = new List<ShowPlatformViewModel>();
                    if (item.Platform != null)
                    {
                        foreach (var x in item.Platform)
                        {
                            map.Platform.Add(new ShowPlatformViewModel() { Platform = x.EnumPlatform });
                        }
                    }


                    map.Members = new List<ShowMembersViewModel>();
                    if (item.Members != null)
                    {
                        foreach (var x in item.Members)
                        {
                            map.Members.Add(new ShowMembersViewModel()
                            {
                                Name = x.Name,
                                Email = x.Email,
                                DegreeOfEducation = x.DegreeOfEducation,
                                Position = x.Position,
                                Mobile = x.Mobile,
                            });
                        }

                    }

                    ir.Add(map);

                }
                return Result.Success<IList<ShowInvestRequestViewModel>>(ir);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "GetAllInvestRequest");
                return Result.Failed<IList<ShowInvestRequestViewModel>>("خطا در دریافت لیست سرمایه گذاری!!");
            }
        }

        public async Task<Result<InvestRequest>> GetInvestRequestById(int id)
        {
            return await _investRequestRepository.GetInvestRequestById(id);
        }

        public Task<Result<InvestRequest>> GetInvestRequestByTrackingNumber(int trackingNumber)
        {
            return _investRequestRepository.GetInvestRequestByTrackingNumber(trackingNumber);
        }

        public async Task<Result<ShowAddInvestRequestViewModel>> RegisterInvestRequest(AddInvestRequestViewModel addInvestRequest)
        {
            try
            {
                if (addInvestRequest.UploadFile != null)
                {
                    if (addInvestRequest.UploadFile?.Count > 3)
                        return Result.Failed<ShowAddInvestRequestViewModel>("حداکثر مجاز به آپلود 3 فایل میباشید!!");
                    foreach (var item in addInvestRequest?.UploadFile)
                    {
                        if (!UploadFileExtension.CheckIfIValidFile(item))
                            return Result.Failed<ShowAddInvestRequestViewModel>("فایل آپلود شده غیر مجاز است!!");
                        if (item.Length / 8 / 1000 / 100 > 10)
                            return Result.Failed<ShowAddInvestRequestViewModel>("حجم فایل آپلود شده بیش از حد مجاز است!!");
                    }
                }
                var random = new Random();
                var ir = new InvestRequest();

                ir.CreateDate = DateTime.Now;
                ir.RequestStatus = DataLayer.Models.Enums.EnumRequestStatus.Submited;
                ir.TrackingNumber = random.Next(100000000, 999999999);
                ir.Name = addInvestRequest.Name;
                ir.Invest = addInvestRequest.Invest;
                ir.AgentName = addInvestRequest.AgentName;
                ir.IsActiveUser = addInvestRequest.IsActiveUser;
                ir.IsRegister = addInvestRequest.IsRegister;
                ir.Mobile = addInvestRequest.Mobile;
                ir.Need = addInvestRequest.Need;
                ir.StartUpEmail = addInvestRequest.StartUpEmail;
                ir.Description = addInvestRequest.Description;
                ir.TeamPersonCount = addInvestRequest.TeamPersonCount;
                ir.Vision = addInvestRequest.Vision;
                ir.WebSiteAddress = addInvestRequest.WebSiteAddress;
                ir.HasProductIdea = addInvestRequest.HasProductIdea;

                ir.Members = new List<Members>();
                if (addInvestRequest?.Members is not null)
                {
                    foreach (var item in addInvestRequest.Members)
                    {
                        ir.Members.Add(new Members()
                        {
                            Email = item.Email,
                            DegreeOfEducation = item.DegreeOfEducation,
                            Name = item.Name,
                            Position = item.Position,
                            Mobile = item.Mobile,
                        });
                    }
                }
                ir.Platform = new List<Platform>();

                if (addInvestRequest?.Platform is not null)
                {
                    foreach (var item in addInvestRequest.Platform)
                    {
                        ir.Platform.Add(new Platform()
                        {
                            EnumPlatform = item.Platform
                        });
                    }
                }
                ir.Category = new List<CategoryInvestReq>();

                if (addInvestRequest?.Category is not null)
                {
                    foreach (var item in addInvestRequest.Category)
                    {
                        ir.Category.Add(new CategoryInvestReq()
                        {
                            EnumCategory = item.Category
                        });
                    }
                }
                ir.Collabration = new List<Collabration>();
                if (addInvestRequest?.Collabration is not null)
                {
                    foreach (var item in addInvestRequest.Collabration)
                    {
                        ir.Collabration.Add(new Collabration()
                        {
                            EnumCollaboration = item.Collaboration
                        });
                    }
                }
                ir.UploadFile = new List<UploadFile>();
                if (addInvestRequest?.UploadFile is not null)
                {
                    foreach (var item in addInvestRequest?.UploadFile)
                    {

                        ir.UploadFile.Add(new UploadFile()
                        {
                            UploadFileName = await UploadFileExtension.WriteFile("InvestRequest", item)
                        });
                    }
                }

                var res = await _investRequestRepository.RegisterInvestRequest(ir);
                var newir = new ShowAddInvestRequestViewModel();

                newir.CreateDate = DateTime.Now;
                newir.TrackingNumber = res.Value.TrackingNumber;
                newir.Name = res.Value.Name;
                newir.Invest = res.Value.Invest;
                newir.AgentName = res.Value.AgentName;
                newir.IsActiveUser = res.Value.IsActiveUser;
                newir.IsRegister = res.Value.IsRegister;
                newir.Mobile = res.Value.Mobile;
                newir.Need = res.Value.Need;
                newir.StartUpEmail = res.Value.StartUpEmail;
                newir.Description = res.Value.Description;
                newir.TeamPersonCount = res.Value.TeamPersonCount;
                newir.Vision = res.Value.Vision;
                newir.WebSiteAddress = res.Value.WebSiteAddress;
                newir.HasProductIdea = res.Value.HasProductIdea;
                newir.UploadFile = new List<ShowUploadFileViewModel>();
                foreach (var x in res.Value.UploadFile)
                {
                    newir.UploadFile.Add(new ShowUploadFileViewModel() { UploadFile = x.UploadFileName });
                }

                newir.Members = new List<ShowMembersViewModel>();
                foreach (var item in res.Value.Members)
                {
                    newir.Members.Add(new ShowMembersViewModel()
                    {
                        Name = item.Name,
                        DegreeOfEducation = item.DegreeOfEducation,
                        Email = item.Email,
                        Position = item.Position,
                        Mobile = item.Mobile,
                    });
                }

                newir.Collabration = new List<ShowCollabrationViewModel>();
                foreach (var item in res.Value.Collabration)
                {
                    newir.Collabration.Add(new ShowCollabrationViewModel()
                    {
                        Collaboration = item.EnumCollaboration,
                    });
                }

                newir.Category = new List<ShowCategoryViewModel>();
                foreach (var item in res.Value.Category)
                {
                    newir.Category.Add(new ShowCategoryViewModel()
                    {
                        Category = item.EnumCategory,
                    });
                }

                newir.Platform = new List<ShowPlatformViewModel>();
                foreach (var item in res.Value.Platform)
                {
                    newir.Platform.Add(new ShowPlatformViewModel()
                    {
                        Platform = item.EnumPlatform,
                    });
                }

                return Result.Success(newir);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RegisterInvestRequest");
                return Result.Failed<ShowAddInvestRequestViewModel>("خطا در ثبت اطلاعات");
            }
        }

        public async Task<Result<ShowUpdateInvestRequestViewModel>> UpdateStatus(ShowUpdateInvestRequestViewModel showUpdateInvestRequestViewModel)
        {
            var newir = new ShowInvestRequestViewModel();
            newir.RequestStatus = showUpdateInvestRequestViewModel.EnumRequestStatus;
            var res = await _investRequestRepository.UpdateRequestStatus(showUpdateInvestRequestViewModel);


            return Result.Success(showUpdateInvestRequestViewModel);
        }
    }
}
