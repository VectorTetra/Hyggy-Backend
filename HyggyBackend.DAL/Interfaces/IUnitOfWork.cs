using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Repositories;

namespace HyggyBackend.DAL.Interfaces
{
    public interface IUnitOfWork
    {
       
        //IBookingRepository Bookings { get; }
        //IHotelRepository Hotels { get; }
        //IRoomRepository Rooms { get; }
        //IRoomTypeRepository RoomTypes { get; }
        //IBedConfigurationRepository BedConfigurations { get; }
        //IRoomConfigurationRepository RoomConfigurations { get; }
        //IHotelRoomRepository HotelRooms { get; }
        Task Save();
    }
}
