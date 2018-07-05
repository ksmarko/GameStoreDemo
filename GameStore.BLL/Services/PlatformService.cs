using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System;
using System.Collections.Generic;

namespace GameStore.BLL.Services
{
    public class PlatformService : IPlatformService
    {
        private IUnitOfWork Database { get; }

        public PlatformService(IUnitOfWork uow)
        {
            Database = uow ?? throw new ArgumentNullException();
        }

        public IEnumerable<PlatformTypeDTO> GetAll()
        {
            return Mapper.Map<IEnumerable<PlatformType>, IEnumerable<PlatformTypeDTO>>(Database.PlatformTypes.GetAll());
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
