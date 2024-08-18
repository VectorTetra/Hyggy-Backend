using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Repositories;
using HyggyBackend.DAL.Entities.Employes;

namespace HyggyBackend.DAL.Interfaces
{
    public interface IUnitOfWork
    {
       
        IWareRepository Wares { get; }
        IOrderRepository Orders { get; }
        //IHotelRepository Hotels { get; }
        //IRoomRepository Rooms { get; }
        //IRoomTypeRepository RoomTypes { get; }
        //IBedConfigurationRepository BedConfigurations { get; }
        //IRoomConfigurationRepository RoomConfigurations { get; }
        //IHotelRoomRepository HotelRooms { get; }
        Task Save();
    }
}
