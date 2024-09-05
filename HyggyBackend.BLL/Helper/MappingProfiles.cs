using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.DTO.EmployeesDTO;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Entities.Employes;

namespace HyggyBackend.BLL.Helper
{
	public class MappingProfiles : Profile
	{
        public MappingProfiles()
        {
            CreateMap<Shop, ShopDTO>()
                //.ForMember(dest => dest.StorageId, opts => opts.MapFrom(src => src.StorageId))
                .ForMember(dest => dest.AddressId, opts => opts.MapFrom(src => src.AddressId))
                .ForMember(dest => dest.OrderIds, opts => opts.MapFrom(src => src.Orders.Select(o => o.Id)));
            CreateMap<ShopDTO, Shop>();
				

            CreateMap<ShopEmployeeDTO, ShopEmployee>();
            CreateMap<ShopEmployee, ShopEmployeeDTO>();

			CreateMap<StorageEmployee, StorageEmployeeDTO>();
			CreateMap<StorageEmployeeDTO, StorageEmployee>();

            CreateMap<OrderStatusDTO, OrderStatus>();
            CreateMap<OrderStatus, OrderStatusDTO>();

            CreateMap<ProffessionDTO, Proffession>();
            CreateMap<Proffession, ProffessionDTO>();

            CreateMap<WarePriceHistoryDTO, WarePriceHistory>();
            CreateMap<WarePriceHistory, WarePriceHistoryDTO>();

            CreateMap<Address, AddressDTO>()
                .ForMember(dest => dest.OrderIds, opts => opts.MapFrom(src => src.Orders.Select(o => o.Id)));
            CreateMap<AddressDTO, Address>();
		}
    }
}
