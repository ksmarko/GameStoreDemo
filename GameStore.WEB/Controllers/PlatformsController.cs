using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.WEB.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace GameStore.WEB.Controllers
{
    public class PlatformsController : ApiController
    {
        private readonly IPlatformService _platformService;

        public PlatformsController(IPlatformService platformService)
        {
            _platformService = platformService ?? throw new ArgumentNullException();
        }

        public IEnumerable<PlatformModel> GetPlatforms()
        {
            return Mapper.Map<IEnumerable<PlatformModel>>(_platformService.GetAll());
        }
    }
}
