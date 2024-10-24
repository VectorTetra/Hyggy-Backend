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
             .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
             .ForMember(dest => dest.ShopId, opts => opts.MapFrom(src => src.Shop != null ? src.Shop.Id : (long?)null))
             .ForMember(dest => dest.StorageId, opts => opts.MapFrom(src => src.Storage != null ? src.Storage.Id : (long?)null))
             .ForMember(dest => dest.Street, opts => opts.MapFrom(src => src.Street != null ? src.Street : null))
             .ForMember(dest => dest.HouseNumber, opts => opts.MapFrom(src => src.HouseNumber != null ? src.HouseNumber : null))
             .ForMember(dest => dest.City, opts => opts.MapFrom(src => src.City != null ? src.City : null))
             .ForMember(dest => dest.State, opts => opts.MapFrom(src => src.State != null ? src.State : null))
             .ForMember(dest => dest.PostalCode, opts => opts.MapFrom(src => src.PostalCode != null ? src.PostalCode : null))
             .ForMember(dest => dest.Latitude, opts => opts.MapFrom(src => src.Latitude != null ? src.Latitude : null))
             .ForMember(dest => dest.Longitude, opts => opts.MapFrom(src => src.Longitude != null ? src.Longitude : null))
             .ForMember(dest => dest.OrderIds, opts => opts.MapFrom(src => src.Orders != null ? src.Orders.Select(o => o.Id).ToList() : new List<long>()));

            CreateMap<AddressQueryBLL, AddressQueryDAL>();
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
            .ForPath(dst => dst.Phone, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForPath(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
            .ForPath(dst => dst.FavoriteWareIds, opt => opt.MapFrom(src => src.FavoriteWares.Select(w => w.Id)))
            .ForPath(dst => dst.ExecutedOrdersSum, opt => opt.MapFrom(src =>
                src.Orders
                    .Where(o => o.Status.Id == 3)
                    .SelectMany(o => o.OrderItems)
                    .Sum(oi => oi.Count * (oi.Ware.Price - (oi.Ware.Price * oi.Ware.Discount / 100)))
            ))
            .ForPath(dst => dst.ExecutedOrdersAvg, opt => opt.MapFrom(src =>
                src.Orders.Where(o => o.Status.Id == 3)
                           .SelectMany(o => o.OrderItems)
                           .Select(oi => oi.Count * (oi.Ware.Price - (oi.Ware.Price * oi.Ware.Discount / 100)))
                           .DefaultIfEmpty(0) // Якщо немає замовлень, повертаємо 0
                           .Average() // Обчислюємо середнє значення
            ));




            //CreateMap<CustomerDTO, Customer>()
            //    .ForPath(dst => dst.Orders, opt => opt.MapFrom(src => src.OrderIds.Select(id => new Order { Id = id })))
            //    .ForPath(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
            //    .ForPath(dst => dst.Surname, opt => opt.MapFrom(src => src.Surname))
            //    .ForPath(dst => dst.Email, opt => opt.MapFrom(src => src.Email));
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
                .ForPath(dst => dst.Count, opt => opt.MapFrom(src => src.Count))
                .ForPath(dst => dst.Ware, opt => opt.MapFrom(src => new WareDTO
                {
                    Id = src.Ware.Id,
                    Article = src.Ware.Article,
                    Name = src.Ware.Name,
                    Description = src.Ware.Description,
                    StructureFilePath = src.Ware.StructureFilePath,
                    Price = src.Ware.Price,
                    Discount = src.Ware.Discount,
                    FinalPrice = src.Ware.Price - (src.Ware.Price * src.Ware.Discount / 100),
                    IsDeliveryAvailable = src.Ware.IsDeliveryAvailable,
                    WareCategory3Id = src.Ware.WareCategory3.Id,
                    StatusIds = src.Ware.Statuses.Select(st => st.Id).ToList(),
                    ImageIds = src.Ware.Images.Select(image => image.Id).ToList(),
                    TrademarkId = src.Ware.WareTrademark != null ? src.Ware.WareTrademark.Id : 0,
                    AverageRating = src.Ware.Reviews.Any() ? src.Ware.Reviews.Average(r => (float)r.Rating) : 0,
                    PreviewImagePath = src.Ware.Images != null && src.Ware.Images.Any() ? src.Ware.Images.FirstOrDefault().Path : null,
                    CustomerFavoriteIds = src.Ware.CustomerFavorites.Select(customer => customer.Id).ToList(),
                    TrademarkName = src.Ware.WareTrademark != null ? src.Ware.WareTrademark.Name : null,
                    StatusNames = src.Ware.Statuses.Select(st => st.Name).ToList(),
                    ImagePaths = src.Ware.Images.Select(image => image.Path).ToList(),
                    WareCategory3Name = src.Ware.WareCategory3.Name
                }))
                .ForPath(dst => dst.PriceHistory, opt => opt.MapFrom(src => new WarePriceHistoryDTO
                {
                    Id = src.PriceHistory.Id,
                    Price = src.PriceHistory.Price,
                    EffectiveDate = src.PriceHistory.EffectiveDate
                }));
            CreateMap<OrderItemQueryBLL, OrderItemQueryDAL>();
            #endregion

            #region Order Mappings
            CreateMap<Order, OrderDTO>()
             .ForPath(dto => dto.Id, opt => opt.MapFrom(src => src.Id))
             .ForPath(dto => dto.DeliveryAddressId, opt => opt.MapFrom(src => src.DeliveryAddress.Id))
             .ForPath(dto => dto.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
             .ForPath(dto => dto.Phone, opt => opt.MapFrom(src => src.Phone))
             .ForPath(dto => dto.Comment, opt => opt.MapFrom(src => src.Comment))
             .ForPath(dto => dto.StatusId, opt => opt.MapFrom(src => src.Status.Id))
             .ForPath(dto => dto.ShopId, opt => opt.MapFrom(src => src.ShopId))
             .ForPath(dto => dto.Shop, opt => opt.MapFrom(src => new ShopDTO
             {
                 Id = src.Shop.Id,
                 Name = src.Shop.Name,
                 PhotoUrl = src.Shop.PhotoUrl,
                 WorkHours = src.Shop.WorkHours,
                 AddressId = src.Shop.Address.Id,
                 StorageId = src.Shop.Storage.Id,
                 Street = src.Shop.Address.Street,
                 HouseNumber = src.Shop.Address.HouseNumber,
                 City = src.Shop.Address.City,
                 State = src.Shop.Address.State,
                 PostalCode = src.Shop.Address.PostalCode,
                 Latitude = src.Shop.Address.Latitude,
                 Longitude = src.Shop.Address.Longitude

             }))
             .ForPath(dto => dto.Status, opt => opt.MapFrom(src => new OrderStatusDTO
             {
                 Id = src.Status.Id,
                 Name = src.Status.Name,
                 Description = src.Status.Description
             }))
             .ForPath(dto => dto.DeliveryAddress, opt => opt.MapFrom(src => new AddressDTO
             {
                 Id = src.DeliveryAddress.Id,
                 ShopId = src.DeliveryAddress.Shop != null ? src.DeliveryAddress.Shop.Id : null,
                 StorageId = src.DeliveryAddress.Storage != null ? src.DeliveryAddress.Storage.Id : null,
                 Street = src.DeliveryAddress.Street,
                 HouseNumber = src.DeliveryAddress.HouseNumber,
                 City = src.DeliveryAddress.City,
                 State = src.DeliveryAddress.State,
                 PostalCode = src.DeliveryAddress.PostalCode,
                 Latitude = src.DeliveryAddress.Latitude,
                 Longitude = src.DeliveryAddress.Longitude
             }))
             .ForPath(dto => dto.CustomerId, opt => opt.MapFrom(src => src.Customer.Id))
             .ForPath(dto => dto.OrderItemIds, opt => opt.MapFrom(src => src.OrderItems.Select(oi => oi.Id)))
             .ForPath(dto => dto.Customer, opt => opt.MapFrom(src => new CustomerDTO
             {
                 Id = src.Customer.Id,
                 Name = src.Customer.Name,
                 Surname = src.Customer.Surname,
                 Email = src.Customer.Email,
                 Phone = src.Customer.PhoneNumber
             }))
             .ForPath(dto => dto.OrderItems, opt => opt.MapFrom(src => src.OrderItems.Select(oi => new OrderItemDTO
             {
                 Ware = new WareDTO
                 {
                     Id = oi.Ware.Id,
                     Name = oi.Ware.Name,
                     Price = oi.Ware.Price,
                     Discount = oi.Ware.Discount,
                     FinalPrice = oi.Ware.Price - (oi.Ware.Price * oi.Ware.Discount / 100),
                     WareCategory3Id = oi.Ware.WareCategory3.Id,
                     PreviewImagePath = oi.Ware.Images.FirstOrDefault() != null ? oi.Ware.Images.FirstOrDefault().Path : null
                 },
                 Count = oi.Count,
                 PriceHistory = new WarePriceHistoryDTO
                 {
                     Id = oi.PriceHistory.Id,
                     Price = oi.PriceHistory.Price,
                     EffectiveDate = oi.PriceHistory.EffectiveDate
                 },
                 Id = oi.Id,
                 OrderId = oi.Order.Id,
                 WareId = oi.Ware.Id,
                 PriceHistoryId = oi.PriceHistory.Id
             }))) ;
            CreateMap<OrderQueryBLL, OrderQueryDAL>();
            #endregion

            #region OrderStatus Mappings
            CreateMap<OrderStatus, OrderStatusDTO>()
                .ForPath(dst => dst.OrderIds, opt => opt.MapFrom(src => src.Orders.Select(o => o.Id)))
                .ForPath(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForPath(dst => dst.Description, opt => opt.MapFrom(src => src.Description))
                .ForPath(dst => dst.Id, opt => opt.MapFrom(src => src.Id));
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
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.PhotoUrl, opts => opts.MapFrom(src => src.PhotoUrl))
                .ForMember(dest => dest.WorkHours, opts => opts.MapFrom(src => src.WorkHours))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Street, opts => opts.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.HouseNumber, opts => opts.MapFrom(src => src.Address.HouseNumber))
                .ForMember(dest => dest.City, opts => opts.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.State, opts => opts.MapFrom(src => src.Address.State))
                .ForMember(dest => dest.PostalCode, opts => opts.MapFrom(src => src.Address.PostalCode))
                .ForMember(dest => dest.Latitude, opts => opts.MapFrom(src => src.Address.Latitude))
                .ForMember(dest => dest.Longitude, opts => opts.MapFrom(src => src.Address.Longitude))
                .ForMember(dest => dest.AddressId, opts => opts.MapFrom(src => src.Address.Id))
                .ForMember(dest => dest.StorageId, opts => opts.MapFrom(src => src.Storage.Id))
                .ForMember(dest => dest.ExecutedOrdersSum, opts => opts.MapFrom(src =>
                    src.Orders
                        .Where(o => o.Status.Id == 3) // Фільтруємо замовлення за статусом
                        .SelectMany(o => o.OrderItems)
                        .Sum(oi => oi.Count * (oi.Ware.Price - (oi.Ware.Price * oi.Ware.Discount / 100)))
                ))
                .ForMember(dest => dest.OrderIds, opts => opts.MapFrom(src => src.Orders.Select(ord => ord.Id)));
            CreateMap<ShopQueryBLL, ShopQueryDAL>();
            #endregion

            #region Storage Mappings
            CreateMap<Storage, StorageDTO>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Street, opts => opts.MapFrom(src => src.Address != null ? src.Address.Street : null))
                .ForMember(dest => dest.HouseNumber, opts => opts.MapFrom(src => src.Address != null ? src.Address.HouseNumber : null))
                .ForMember(dest => dest.City, opts => opts.MapFrom(src => src.Address != null ? src.Address.City : null))
                .ForMember(dest => dest.State, opts => opts.MapFrom(src => src.Address != null ? src.Address.State : null))
                .ForMember(dest => dest.PostalCode, opts => opts.MapFrom(src => src.Address != null ? src.Address.PostalCode : null))
                .ForMember(dest => dest.Latitude, opts => opts.MapFrom(src => src.Address != null ? src.Address.Latitude : (long?)null))
                .ForMember(dest => dest.Longitude, opts => opts.MapFrom(src => src.Address != null ? src.Address.Longitude : (long?)null))
                .ForMember(dst => dst.ShopId, opts => opts.MapFrom(src => src.Shop != null ? src.Shop.Id : (long?)null))
                .ForMember(dst => dst.ShopName, opts => opts.MapFrom(src => src.Shop != null ? src.Shop.Name : "-> (Загальний склад)"))
                .ForMember(dst => dst.AddressId, opt => opt.MapFrom(src => src.Address != null ? src.Address.Id : (long?)null))
                .ForMember(dst => dst.StoredWaresSum, opt => opt.MapFrom(src => src.WareItems != null ? src.WareItems.Sum(wi => wi.Quantity * (wi.Ware.Price * wi.Ware.Discount / 100)) : 0));
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
             .ForMember(dst => dst.WaresCategory2Ids, opt => opt.MapFrom(c => c.WaresCategory2 != null
                 ? c.WaresCategory2.Select(wc => wc.Id).ToList()
                 : new List<long>()))
             .ForMember(dst => dst.Name, opt => opt.MapFrom(c => c.Name))
             .ForMember(dst => dst.WaresCategories2, opt => opt.MapFrom(c => c.WaresCategory2 != null
                 ? c.WaresCategory2.Select(wc => new WareCategory2DTO
                 {
                     Id = wc.Id,
                     Name = wc.Name,
                     WareCategory1Id = wc.WareCategory1 != null ? wc.WareCategory1.Id : (long?)null,
                     WaresCategories3 = wc.WaresCategory3 != null
                         ? wc.WaresCategory3.Select(wc3 => new WareCategory3DTO
                         {
                             Id = wc3.Id,
                             Name = wc3.Name,
                             WareCategory2Id = wc3.WareCategory2 != null ? wc3.WareCategory2.Id : (long?)null,
                             WareIds = new List<long>()
                         }).ToList()
                         : new List<WareCategory3DTO>()
                 }).ToList()
                 : new List<WareCategory2DTO>()));




            CreateMap<WareCategory1QueryBLL, WareCategory1QueryDAL>();
            #endregion

            #region WareCategory2 Mappings
            CreateMap<WareCategory2, WareCategory2DTO>()
            .ForMember(dst => dst.Id, opt => opt.MapFrom(c => c.Id))
            .ForMember(dst => dst.Name, opt => opt.MapFrom(c => c.Name))
            .ForMember(dst => dst.WareCategory1Id, opt => opt.MapFrom(c => c.WareCategory1 != null ? c.WareCategory1.Id : (long?)null))
            .ForMember(dst => dst.WaresCategories3, opt => opt.MapFrom(c => c.WaresCategory3 != null
                ? c.WaresCategory3.Select(wc3 => new WareCategory3DTO
                {
                    Id = wc3.Id,
                    Name = wc3.Name,
                    WareCategory2Id = wc3.WareCategory2 != null ? wc3.WareCategory2.Id : (long?)null,
                    WareIds = new List<long>()
                }).ToList()
                : new List<WareCategory3DTO>()))
            .ForMember(dst => dst.WaresCategory3Ids, opt => opt.MapFrom(c => c.WaresCategory3 != null
                ? c.WaresCategory3.Select(wc => wc.Id).ToList()
                : new List<long>()));



            CreateMap<WareCategory2QueryBLL, WareCategory2QueryDAL>();
            #endregion

            #region WareCategory3 Mappings
            CreateMap<WareCategory3, WareCategory3DTO>()
           .ForMember("Id", opt => opt.MapFrom(c => c.Id))
           .ForMember("Name", opt => opt.MapFrom(c => c.Name))
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
            CreateMap<WarePriceHistory, WarePriceHistoryDTO>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.WareId, opts => opts.MapFrom(src => src.Ware.Id))
                .ForMember(dest => dest.Price, opts => opts.MapFrom(src => src.Price))
                .ForMember(dest => dest.EffectiveDate, opts => opts.MapFrom(src => src.EffectiveDate));
            CreateMap<WarePriceHistoryQueryBLL, WarePriceHistoryQueryDAL>();

            #endregion

            #region Ware Mappings
            CreateMap<Ware, WareDTO>()
             .ForMember(d => d.Id, opt => opt.MapFrom(c => c.Id))
             .ForMember(d => d.Article, opt => opt.MapFrom(c => c.Article))
             .ForMember(d => d.Name, opt => opt.MapFrom(c => c.Name))
             .ForMember(d => d.Description, opt => opt.MapFrom(c => c.Description))
             .ForMember(d => d.StructureFilePath, opt => opt.MapFrom(c => c.StructureFilePath))
             .ForMember(d => d.Price, opt => opt.MapFrom(c => c.Price))
             .ForMember(d => d.Discount, opt => opt.MapFrom(c => c.Discount))
             .ForMember(d => d.FinalPrice, opt => opt.MapFrom(c => c.Price - (c.Price * c.Discount / 100)))
             .ForMember(d => d.IsDeliveryAvailable, opt => opt.MapFrom(c => c.IsDeliveryAvailable))
             .ForMember(d => d.WareCategory3Id, opt => opt.MapFrom(c => c.WareCategory3.Id))
             .ForMember(d => d.StatusIds, opt => opt.MapFrom(c => c.Statuses.Select(st => st.Id)))
             .ForMember(d => d.ImageIds, opt => opt.MapFrom(c => c.Images.Select(image => image.Id)))
             .ForMember(d => d.PriceHistoryIds, opt => opt.MapFrom(c => c.PriceHistories.Select(price => price.Id)))
             .ForMember(d => d.WareItemIds, opt => opt.MapFrom(c => c.WareItems.Select(wareItem => wareItem.Id)))
             .ForMember(d => d.OrderItemIds, opt => opt.MapFrom(c => c.OrderItems.Select(orderItem => orderItem.Id)))
             .ForMember(d => d.ReviewIds, opt => opt.MapFrom(c => c.Reviews.Select(review => review.Id)))
             .ForMember(d => d.TrademarkId, opt => opt.MapFrom(c => c.WareTrademark != null ? c.WareTrademark.Id : 0))
             .ForMember(d => d.AverageRating, opt => opt.MapFrom(c => c.Reviews.Any() ? c.Reviews.Average(r => (float)r.Rating) : 0))
             .ForMember(d => d.PreviewImagePath, opt => opt.MapFrom(c => c.Images != null && c.Images.Any() ? c.Images.FirstOrDefault().Path : null))
             .ForMember(d => d.CustomerFavoriteIds, opt => opt.MapFrom(c => c.CustomerFavorites.Select(customer => customer.Id)))
             .ForMember(d => d.TrademarkName, opt => opt.MapFrom(c => c.WareTrademark != null ? c.WareTrademark.Name : null))
             .ForMember(d => d.StatusNames, opt => opt.MapFrom(c => c.Statuses.Select(st => st.Name)))
             .ForMember(d => d.ImagePaths, opt => opt.MapFrom(c => c.Images.Select(image => image.Path)))
             .ForMember(d => d.WareCategory3Name, opt => opt.MapFrom(c => c.WareCategory3.Name));

            CreateMap<WareQueryBLL, WareQueryDAL>();
            #endregion

            #region WareReview Mappings
            CreateMap<WareReview, WareReviewDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(d => d.WareId, opt => opt.MapFrom(c => c.Ware.Id))
                .ForMember(d => d.CustomerName, opt => opt.MapFrom(c => c.CustomerName))
                .ForMember(d => d.AuthorizedCustomerId, opt => opt.MapFrom(c => c.AuthorizedCustomerId))
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
                .ForMember(d => d.Description, opt => opt.MapFrom(c => c.Description))
                .ForMember(d => d.WareIds, opt => opt.MapFrom(c => c.Wares.Select(w => w.Id)));
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
