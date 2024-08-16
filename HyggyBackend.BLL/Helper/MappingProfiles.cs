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
                .ForMember(dest => dest.Storage, opts => opts.MapFrom(src => src.Storage))
                .ForMember(dest => dest.Address, opts => opts.MapFrom(src => src.Address))
                .ForMember(dest => dest.Orders, opts => opts.MapFrom(src => src.Orders));

            CreateMap<ShopDTO, Shop>()
				.ForMember(dest => dest.Storage, opts => opts.MapFrom(src => src.Storage))
				.ForMember(dest => dest.Address, opts => opts.MapFrom(src => src.Address))
				.ForMember(dest => dest.Orders, opts => opts.MapFrom(src => src.Orders)); ;

            CreateMap<ShopEmployeeDTO, ShopEmployee>();
            CreateMap<ShopEmployee, ShopEmployeeDTO>();

			CreateMap<StorageEmployee, StorageEmployeeDTO>();
			CreateMap<StorageEmployeeDTO, StorageEmployee>();
		}
    }
}
