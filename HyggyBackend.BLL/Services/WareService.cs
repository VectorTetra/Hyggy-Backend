using HyggyBackend.BLL.Interfaces;
using AutoMapper;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Entities;
using HyggyBackend.BLL.DTO;

namespace HyggyBackend.BLL.Services
{
    public class WareService : IWareService
    {
        IUnitOfWork Database;

        public WareService(IUnitOfWork uow)
        {
            Database = uow;
        }

        MapperConfiguration Ware_WareDTOMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Ware, WareDTO>()
        .ForMember("Id", opt => opt.MapFrom(c => c.Id))
        .ForMember("Article", opt => opt.MapFrom(c => c.Article))
        .ForMember("Name", opt => opt.MapFrom(c => c.Name))
        .ForMember("Description", opt => opt.MapFrom(c => c.Description))
        .ForMember("Price", opt => opt.MapFrom(c => c.Price))
        .ForMember("Discount", opt => opt.MapFrom(c => c.Discount))
        .ForMember("IsDeliveryAvailable", opt => opt.MapFrom(c => c.IsDeliveryAvailable))
        //.ForPath(d => d.Category3, opt => opt.MapFrom(c => new WareCategory3DTO { Id }))
        
        );
    }
}
