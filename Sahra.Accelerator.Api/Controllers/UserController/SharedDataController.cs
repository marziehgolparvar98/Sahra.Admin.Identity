using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sahara.Common;
using Sahra.DataLayer.Models.Enums;
using System.Collections.Generic;

namespace Sahra.Accelerator.api.Controllers.UserController
{
    [Route("api/[controller]")]
    [ApiController]
    public class SharedDataController : ControllerBase
    {



        [HttpGet("GetPlatform")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EnumDescription>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result.ErrorResult))]
        public IActionResult GetPlatform()
        {
            return Result.Success(EnumExtensions.GetList<EnumPlatform>()).ToHttpCodeResult();
        }

        [HttpGet("GetCategory")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EnumDescription>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result.ErrorResult))]
        public IActionResult GetCategory()
        {
            return Result.Success(EnumExtensions.GetList<EnumCategory>()).ToHttpCodeResult();
        }

        [HttpGet("GetRequestStatus")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EnumDescription>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result.ErrorResult))]
        public IActionResult GetRequestStatus()
        {
            return Result.Success(EnumExtensions.GetList<EnumRequestStatus>()).ToHttpCodeResult();
        }

        [HttpGet("GetCollabrationm")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EnumDescription>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result.ErrorResult))]
        public IActionResult GetCollabration()
        {
            return Result.Success(EnumExtensions.GetList<EnumCollaboration>()).ToHttpCodeResult();
        }
    }
}
