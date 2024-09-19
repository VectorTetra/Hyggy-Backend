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
                .ForMember(dest => dest.AddressId, opts => opts.MapFrom(src => src.AddressId))
                .ForMember(dest => dest.OrderIds, opts => opts.MapFrom(src => src.Orders.Select(o => o.Id)));
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

			CreateMap<UserForRegistrationDto, Customer>()
				.ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));

            CreateMap<UserForEditDto, Customer>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
			

			CreateMap<StorageEmployee, StorageEmployeeDTO>();
			CreateMap<StorageEmployeeDTO, StorageEmployee>();

            CreateMap<Address, AddressDTO>()
                .ForMember(dest => dest.OrderIds, opts => opts.MapFrom(src => src.Orders.Select(o => o.Id)));
            CreateMap<AddressDTO, Address>();
		}
    }
}
