using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.DTO.AccountDtos;
using HyggyBackend.BLL.DTO.EmployeesDTO;
using HyggyBackend.BLL.Queries;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Entities.Employes;
using HyggyBackend.DAL.Queries;

namespace HyggyBackend.BLL.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region Address Mappings
            CreateMap<Address, AddressDTO>()
                .ForMember(dest => dest.OrderIds, opts => opts.MapFrom(src => src.Orders.Select(o => o.Id)));
            CreateMap<AddressDTO, Address>();
            #endregion

            #region Blog Mappings
            CreateMap<Blog, BlogDTO>()
                .ForMember(dest => dest.BlogCategory2Id, opts => opts.MapFrom(src => src.BlogCategory2.Id))
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.BlogTitle, opts => opts.MapFrom(src => src.BlogTitle))
                .ForMember(dest => dest.Keywords, opts => opts.MapFrom(src => src.Keywords))
                .ForMember(dest => dest.FilePath, opts => opts.MapFrom(src => src.FilePath))
                .ForMember(dest => dest.PreviewImagePath, opts => opts.MapFrom(src => src.PreviewImagePath));
            CreateMap<BlogQueryBLL, BlogQueryDAL>();
            #endregion

            #region BlogCategory1 Mappings
            CreateMap<BlogCategory1, BlogCategory1DTO>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.BlogCategory2Ids, opts => opts.MapFrom(src => src.BlogCategories2.Select(bc2 => bc2.Id)));
            CreateMap<BlogCategory1QueryBLL, BlogCategory1QueryDAL>();
            #endregion

            #region BlogCategory2 Mappings
            CreateMap<BlogCategory2, BlogCategory2DTO>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.PreviewImagePath, opts => opts.MapFrom(src => src.PreviewImagePath))
                .ForMember(dest => dest.BlogCategory1Id, opts => opts.MapFrom(src => src.BlogCategory1.Id));
            CreateMap<BlogCategory2QueryBLL, BlogCategory2QueryDAL>();
            #endregion

            #region Customer Mappings
            CreateMap<Customer, CustomerDTO>()
                .ForPath(dst => dst.OrderIds, opt => opt.MapFrom(src => src.Orders.Select(o => o.Id)))
                .ForPath(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForPath(dst => dst.Surname, opt => opt.MapFrom(src => src.Surname))
                .ForPath(dst => dst.Email, opt => opt.MapFrom(src => src.Email));
            CreateMap<CustomerDTO, Customer>()
                .ForPath(dst => dst.Orders, opt => opt.MapFrom(src => src.OrderIds.Select(id => new Order { Id = id })))
                .ForPath(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForPath(dst => dst.Surname, opt => opt.MapFrom(src => src.Surname))
                .ForPath(dst => dst.Email, opt => opt.MapFrom(src => src.Email));
            CreateMap<CustomerQueryBLL, CustomerQueryDAL>();
            #endregion

            #region Employee Mappings
            CreateMap<EmployeeForRegistrationDto, ShopEmployee>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
            CreateMap<EmployeeForRegistrationDto, StorageEmployee>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
            CreateMap<ShopEmployee, ShopEmployeeDTO>();
            CreateMap<ShopEmployeeDTO, ShopEmployee>();
            CreateMap<StorageEmployee, StorageEmployeeDTO>();
            CreateMap<StorageEmployeeDTO, StorageEmployee>();
            #endregion

            #region OrderItem Mappings
            CreateMap<OrderItem, OrderItemDTO>()
                .ForPath(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForPath(dst => dst.OrderId, opt => opt.MapFrom(src => src.Order.Id))
                .ForPath(dst => dst.WareId, opt => opt.MapFrom(src => src.Ware.Id))
                .ForPath(dst => dst.PriceHistoryId, opt => opt.MapFrom(src => src.PriceHistory.Id))
                .ForPath(dst => dst.Count, opt => opt.MapFrom(src => src.Count));
            CreateMap<OrderItemQueryBLL, OrderItemQueryDAL>();
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


            #region User Mappings
            CreateMap<UserForEditDto, Customer>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
            CreateMap<UserForRegistrationDto, Customer>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
            #endregion

            #region WareCategory1 Mappings
            CreateMap<WareCategory1, WareCategory1DTO>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(c => c.Name))
                .ForMember(dst => dst.JSONStructureFilePath, opt => opt.MapFrom(c => c.JSONStructureFilePath))
                .ForMember(dst => dst.WaresCategory2Ids, opt => opt.MapFrom(c => c.WaresCategory2.Select(wc => wc.Id)));

            CreateMap<WareCategory1QueryBLL, WareCategory1QueryDAL>();
            #endregion

            #region WareCategory2 Mappings
            CreateMap<WareCategory2, WareCategory2DTO>()
            .ForMember("Id", opt => opt.MapFrom(c => c.Id))
            .ForMember("Name", opt => opt.MapFrom(c => c.Name))
            .ForMember("JSONStructureFilePath", opt => opt.MapFrom(c => c.JSONStructureFilePath))
            .ForPath(dst => dst.WareCategory1Id, opt => opt.MapFrom(c => c.WareCategory1.Id))
            .ForMember(dst => dst.WaresCategory3Ids, opt => opt.MapFrom(c => c.WaresCategory3.Select(wc => wc.Id)));

            CreateMap<WareCategory2QueryBLL, WareCategory2QueryDAL>();
            #endregion

            #region WareCategory3 Mappings
            CreateMap<WareCategory3, WareCategory3DTO>()
           .ForMember("Id", opt => opt.MapFrom(c => c.Id))
           .ForMember("Name", opt => opt.MapFrom(c => c.Name))
           .ForMember("JSONStructureFilePath", opt => opt.MapFrom(c => c.JSONStructureFilePath))
           .ForPath(dst => dst.WareCategory2Id, opt => opt.MapFrom(c => c.WareCategory2.Id));

            CreateMap<WareCategory3QueryBLL, WareCategory3QueryDAL>();
            #endregion

            #region WareImage Mappings
            CreateMap<WareImage, WareImageDTO>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Path, opts => opts.MapFrom(src => src.Path))
                .ForMember(dest => dest.WareId, opts => opts.MapFrom(src => src.Ware.Id));
            CreateMap<WareImageQueryBLL, WareImageQueryDAL>();
            #endregion

            #region WarePriceHistory Mappings
            CreateMap<WarePriceHistory, WarePriceHistoryDTO>();
            CreateMap<WarePriceHistoryDTO, WarePriceHistory>();
            #endregion

            #region Ware Mappings
            CreateMap<Ware, WareDTO>()
             .ForMember(d => d.Id, opt => opt.MapFrom(c => c.Id))
             .ForMember(d => d.Article, opt => opt.MapFrom(c => c.Article))
             .ForMember(d => d.Name, opt => opt.MapFrom(c => c.Name))
             .ForMember(d => d.Description, opt => opt.MapFrom(c => c.Description))
             .ForMember(d => d.Price, opt => opt.MapFrom(c => c.Price))
             .ForMember(d => d.Discount, opt => opt.MapFrom(c => c.Discount))
             .ForMember(d => d.IsDeliveryAvailable, opt => opt.MapFrom(c => c.IsDeliveryAvailable))
             .ForMember(d => d.WareCategory3Id, opt => opt.MapFrom(c => c.WareCategory3.Id))
             .ForMember(d => d.StatusId, opt => opt.MapFrom(c => c.Status.Id))
             .ForMember(d => d.ImageIds, opt => opt.MapFrom(c => c.Images.Select(image => image.Id)))
             .ForMember(d => d.PriceHistoryIds, opt => opt.MapFrom(c => c.PriceHistories.Select(price => price.Id)))
             .ForMember(d => d.WareItemIds, opt => opt.MapFrom(c => c.WareItems.Select(wareItem => wareItem.Id)))
             .ForMember(d => d.OrderItemIds, opt => opt.MapFrom(c => c.OrderItems.Select(orderItem => orderItem.Id)))
             .ForMember(d => d.ReviewIds, opt => opt.MapFrom(c => c.Reviews.Select(review => review.Id)))
             .ForMember(d => d.TrademarkId, opt => opt.MapFrom(c => c.WareTrademark != null ? c.WareTrademark.Id : 0))
             .ForMember(d => d.AverageRating, opt => opt.MapFrom(c => c.Reviews.Any() ? c.Reviews.Average(r => (float)r.Rating) : 0))
             .ForMember(d => d.PreviewImagePath, opt => opt.MapFrom(c => c.Images != null && c.Images.Any() ? c.Images.FirstOrDefault().Path : null))
             .ForMember(d => d.CustomerFavoriteIds, opt => opt.MapFrom(c => c.CustomerFavorites.Select(customer => customer.Id)));

            CreateMap<WareQueryBLL, WareQueryDAL>();
            #endregion

            #region WareReview Mappings
            CreateMap<WareReview, WareReviewDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(d => d.WareId, opt => opt.MapFrom(c => c.Ware.Id))
                .ForMember(d => d.CustomerName, opt => opt.MapFrom(c => c.CustomerName))
                .ForMember(d => d.Rating, opt => opt.MapFrom(c => c.Rating))
                .ForMember(d => d.Text, opt => opt.MapFrom(c => c.Text))
                .ForMember(d => d.Theme, opt => opt.MapFrom(c => c.Theme))
                .ForMember(d => d.Email, opt => opt.MapFrom(c => c.Email))
                .ForMember(d => d.Date, opt => opt.MapFrom(c => c.Date));
            CreateMap<WareReviewQueryBLL, WareReviewQueryDAL>();
            #endregion

            #region WareTrademark Mappings
            CreateMap<WareTrademark, WareTrademarkDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(c => c.Name))
                .ForMember(d => d.WareIds, opt => opt.MapFrom(c => c.Wares.Select(w => w.Id)));
            CreateMap<WareTrademarkQueryBLL, WareTrademarkQueryDAL>();
            #endregion

            #region WareStatus Mappings
            CreateMap<WareStatus, WareStatusDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(c => c.Name))
                .ForMember(d => d.Description, opt => opt.MapFrom(c => c.Description));
            CreateMap<WareStatusQueryBLL, WareStatusQueryDAL>();
            #endregion

            #region WareItem Mappings
            CreateMap<WareItem, WareItemDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.WareId, opt => opt.MapFrom(s => s.Ware.Id))
                .ForMember(d => d.StorageId, opt => opt.MapFrom(s => s.Storage.Id))
                .ForMember(d => d.Quantity, opt => opt.MapFrom(s => s.Quantity));

            CreateMap<WareItemQueryBLL, WareItemQueryDAL>();
            #endregion
        }
    }
}
