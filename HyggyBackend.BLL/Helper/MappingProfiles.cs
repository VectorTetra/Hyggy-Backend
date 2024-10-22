using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.DTO.AccountDtos;
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
                .ForMember(dest => dest.Address, opts => opts.MapFrom(src => src.Address))
                .ForMember(dest => dest.Orders, opts => opts.MapFrom(src => src.Orders));
            CreateMap<ShopDTO, Shop>();

            CreateMap<MainStorage, MainStorageDto>()
                .ForMember(dest => dest.AddressId, opts => opts.MapFrom(src => src.AddressId))
                .ForMember(dest => dest.ShopIds, opts => opts.MapFrom(src => src.Shops.Select(s => s.Id)))
                .ForMember(dest => dest.StorageEmployeeIds, opts => opts.MapFrom(src => src.Employees.Select(e => e.Id)));
            CreateMap<MainStorageDto, MainStorage>();

			CreateMap<ShopEmployeeDTO, ShopEmployee>();
            CreateMap<ShopEmployee, ShopEmployeeDTO>();

			CreateMap<EmployeeForRegistrationDto, ShopEmployee>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
            CreateMap<EmployeeForRegistrationDto, StorageEmployee>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
            CreateMap<ShopEmployee, ShopEmployeeDTO>();
            CreateMap<ShopEmployeeDTO, ShopEmployee>();
            CreateMap<StorageEmployee, StorageEmployeeDTO>();
            CreateMap<StorageEmployeeDTO, StorageEmployee>();
            #endregion

			CreateMap<UserForRegistrationDto, Customer>()
				.ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
            #region OrderItem Mappings
            CreateMap<OrderItem, OrderItemDTO>()
                .ForPath(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForPath(dst => dst.OrderId, opt => opt.MapFrom(src => src.Order.Id))
                .ForPath(dst => dst.WareId, opt => opt.MapFrom(src => src.Ware.Id))
                .ForPath(dst => dst.PriceHistoryId, opt => opt.MapFrom(src => src.PriceHistory.Id))
                .ForPath(dst => dst.Count, opt => opt.MapFrom(src => src.Count));
            CreateMap<OrderItemQueryBLL, OrderItemQueryDAL>();
            CreateMap<OrderItemDTO, OrderItem>();
            #endregion

            #region OrderStatus Mappings
            CreateMap<OrderStatus, OrderStatusDTO>();
            CreateMap<OrderStatusDTO, OrderStatus>();
            #endregion

            #region Profession Mappings
            CreateMap<Proffession, ProffessionDTO>()
            .ForMember(dst => dst.Id, opt => opt.MapFrom(c => c.Id))
            .ForMember(dst => dst.Name, opt => opt.MapFrom(c => c.Name));
            //.ForMember(dst => dst.EmployeeIds, opt => opt.MapFrom(c => c.Employes.Select(b => b.Id).ToList()));
            CreateMap<ProffessionQueryBLL, ProffessionQueryDAL>();
            #endregion

            #region Shop Mappings
            CreateMap<Shop, ShopDTO>()
                .ForMember(dest => dest.AddressId, opts => opts.MapFrom(src => src.Address.Id))
                .ForMember(dest => dest.OrderIds, opts => opts.MapFrom(src => src.Orders.Select(ord => ord.Id)));
            CreateMap<ShopDTO, Shop>();
            #endregion


            #region Storage Mappings
            CreateMap<Storage, StorageDTO>()
                .ForMember(dst=>dst.Id, opt=>opt.MapFrom(src=>src.Id))
                .ForMember(dst => dst.ShopId, opt => opt.MapFrom(src => src.Shop.Id))
                .ForMember(dst => dst.AddressId, opt => opt.MapFrom(src => src.Address.Id));
            CreateMap<StorageQueryBLL, StorageQueryDAL>();
            #endregion


            CreateMap<UserForEditDto, Customer>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
			

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
