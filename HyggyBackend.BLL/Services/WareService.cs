using HyggyBackend.BLL.Interfaces;
using AutoMapper;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Entities;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Queries;
using HyggyBackend.DAL.Queries;
using HyggyBackend.BLL.Infrastructure;

namespace HyggyBackend.BLL.Services
{
    public class WareService : IWareService
    {
        IUnitOfWork Database;
        IMapper _mapper;

        public WareService(IUnitOfWork uow, IMapper mapper)
        {
            Database = uow;
            _mapper = mapper;
        }
        public async Task<WareDTO?> GetById(long id)
        {
            var ware = await Database.Wares.GetById(id);
            if (ware == null)
            {
                return null;
            }
            return _mapper.Map<Ware, WareDTO>(ware);
        }
        public async Task<WareDTO?> GetByArticle(long article)
        {
            var ware = await Database.Wares.GetByArticle(article);
            if (ware == null)
            {
                return null;
            }
            return _mapper.Map<Ware, WareDTO>(ware);
        }
        public async Task<IEnumerable<WareDTO>> GetPagedWares(int pageNumber, int pageSize)
        {
            var wares = await Database.Wares.GetPagedWares(pageNumber, pageSize);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }

        public async Task<IEnumerable<WareDTO>> GetByStringIds(string stringIds)
        {
            var wares = await Database.Wares.GetByStringIds(stringIds);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }

        public async Task<IEnumerable<WareDTO>> GetByStringTrademarkIds(string stringTrademarkIds)
        {
            var wares = await Database.Wares.GetByStringTrademarkIds(stringTrademarkIds);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }

        public async Task<IEnumerable<WareDTO>> GetByStringStatusIds(string stringStatusIds)
        {
            var wares = await Database.Wares.GetByStringStatusIds(stringStatusIds);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }

        public async Task<IEnumerable<WareDTO>> GetByStringCategory1Ids(string stringCategory1Ids)
        {
            var wares = await Database.Wares.GetByStringCategory1Ids(stringCategory1Ids);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }

        public async Task<IEnumerable<WareDTO>> GetByStringCategory2Ids(string stringCategory2Ids)
        {
            var wares = await Database.Wares.GetByStringCategory2Ids(stringCategory2Ids);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }

        public async Task<IEnumerable<WareDTO>> GetByStringCategory3Ids(string stringCategory3Ids)
        {
            var wares = await Database.Wares.GetByStringCategory3Ids(stringCategory3Ids);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }

        public async Task<IEnumerable<WareDTO>> GetByCategory1Id(long category1Id)
        {
            var wares = await Database.Wares.GetByCategory1Id(category1Id);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }
        public async Task<IEnumerable<WareDTO>> GetByCategory2Id(long category2Id)
        {
            var wares = await Database.Wares.GetByCategory2Id(category2Id);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }
        public async Task<IEnumerable<WareDTO>> GetByCategory3Id(long category3Id)
        {
            var wares = await Database.Wares.GetByCategory3Id(category3Id);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }
        public async Task<IEnumerable<WareDTO>> GetByNameSubstring(string nameSubstring)
        {
            var wares = await Database.Wares.GetByNameSubstring(nameSubstring);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }

        public async Task<IEnumerable<WareDTO>> GetByStructureFilePathSubstring(string StructureFilePathSubstring)
        {
            var wares = await Database.Wares.GetByStructureFilePathSubstring(StructureFilePathSubstring);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }
        public async Task<IEnumerable<WareDTO>> GetByDescriptionSubstring(string descriptionSubstring)
        {
            var wares = await Database.Wares.GetByDescriptionSubstring(descriptionSubstring);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }
        public async Task<IEnumerable<WareDTO>> GetByCategory1NameSubstring(string category1NameSubstring)
        {
            var wares = await Database.Wares.GetByCategory1NameSubstring(category1NameSubstring);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }
        public async Task<IEnumerable<WareDTO>> GetByCategory2NameSubstring(string category2NameSubstring)
        {
            var wares = await Database.Wares.GetByCategory2NameSubstring(category2NameSubstring);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }
        public async Task<IEnumerable<WareDTO>> GetByCategory3NameSubstring(string category3NameSubstring)
        {
            var wares = await Database.Wares.GetByCategory3NameSubstring(category3NameSubstring);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }
        public async Task<IEnumerable<WareDTO>> GetByTrademarkId(long trademarkId)
        {
            var wares = await Database.Wares.GetByTrademarkId(trademarkId);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }
        public async Task<IEnumerable<WareDTO>> GetByTrademarkNameSubstring(string trademarkNameSubstring)
        {
            var wares = await Database.Wares.GetByTrademarkNameSubstring(trademarkNameSubstring);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }
        public async Task<IEnumerable<WareDTO>> GetByPriceRange(float minPrice, float maxPrice)
        {
            var wares = await Database.Wares.GetByPriceRange(minPrice, maxPrice);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }
        public async Task<IEnumerable<WareDTO>> GetByDiscountRange(float minDiscount, float maxDiscount)
        {
            var wares = await Database.Wares.GetByDiscountRange(minDiscount, maxDiscount);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }
        public async Task<IEnumerable<WareDTO>> GetByIsDeliveryAvailable(bool isDeliveryAvailable)
        {
            var wares = await Database.Wares.GetByIsDeliveryAvailable(isDeliveryAvailable);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }
        public async Task<IEnumerable<WareDTO>> GetByStatusId(long statusId)
        {
            var wares = await Database.Wares.GetByStatusId(statusId);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }
        public async Task<IEnumerable<WareDTO>> GetByStatusNameSubstring(string statusNameSubstring)
        {
            var wares = await Database.Wares.GetByStatusNameSubstring(statusNameSubstring);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }
        public async Task<IEnumerable<WareDTO>> GetByStatusDescriptionSubstring(string statusDescriptionSubstring)
        {
            var wares = await Database.Wares.GetByStatusDescriptionSubstring(statusDescriptionSubstring);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }
        public async Task<IEnumerable<WareDTO>> GetByImagePathSubstring(string imagePathSubstring)
        {
            var wares = await Database.Wares.GetByImagePathSubstring(imagePathSubstring);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }
        public async Task<IEnumerable<WareDTO>> GetFavoritesByCustomerId(string customerId)
        {
            var wares = await Database.Wares.GetFavoritesByCustomerId(customerId);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }
        public async Task<IEnumerable<WareDTO>> GetByQuery(WareQueryBLL queryDAL)
        {
            var query = _mapper.Map<WareQueryBLL, WareQueryDAL>(queryDAL);
            var wares = await Database.Wares.GetByQuery(query);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }
        public async Task<WareDTO> Create(WareDTO wareDTO)
        {
            if (wareDTO.Article == null)
            {
                throw new ValidationException("Артикул не може бути пустим!", wareDTO.Article.ToString());
            }
            //Перевірка на унікальність артикулу та назви
            var existedArticle = await Database.Wares.GetByArticle(wareDTO.Article.Value);
            if (existedArticle != null)
            {
                throw new ValidationException("Товар з таким артикулом вже існує!", wareDTO.Article.ToString());
            }
            //var existedNames = await Database.Wares.GetByNameSubstring(wareDTO.Name);
            //if (existedNames.Any(x => x.Name == wareDTO.Name))
            //{
            //    throw new ValidationException("Товар з таким іменем вже існує!", wareDTO.Name);
            //}
            if (wareDTO.Name == null)
            {
                throw new ValidationException("Назва не може бути пустою!", wareDTO.Name);
            }

            if (wareDTO.Description == null)
            {
                throw new ValidationException("Опис не може бути пустим!", wareDTO.Description);
            }

            if (wareDTO.Price == null)
            {
                throw new ValidationException("Ціна не може бути пустою!", wareDTO.Price.ToString());
            }
            if (wareDTO.Price < 0)
            {
                throw new ValidationException("Ціна не може бути від'ємною!", wareDTO.Price.ToString());
            }
            if (wareDTO.Discount < 0)
            {
                throw new ValidationException("Знижка не може бути від'ємною!", wareDTO.Discount.ToString());
            }
            if (wareDTO.WareCategory3Id == null)
            {
                throw new ValidationException("Категорія не може бути пустою!", wareDTO.WareCategory3Id.ToString());
            }
            var existedCategory = await Database.Categories3.GetById(wareDTO.WareCategory3Id.Value);
            if (existedCategory == null)
            {
                throw new ValidationException("Категорії з таким Id не існує!", wareDTO.WareCategory3Id.ToString());
            }
            if (wareDTO.IsDeliveryAvailable == null)
            {
                throw new ValidationException("Доставка не може бути пустою!", "");
            }
            var statuses = new List<WareStatus>();
            await foreach (var wareImage in Database.WareStatuses.GetByIdsAsync(wareDTO.StatusIds))
            {
                if (wareImage == null)
                {
                    throw new ValidationException($"Одна з WareImage не знайдена!", "");
                }
                statuses.Add(wareImage);
            }
            var existedTrademark = new WareTrademark();
            if (wareDTO.TrademarkId != null)
            {
                existedTrademark = await Database.WareTrademarks.GetById(wareDTO.TrademarkId.Value);
                if (existedTrademark == null)
                {
                    throw new ValidationException("Торгової марки з таким Id не існує!", wareDTO.TrademarkId.ToString());
                }
            }
            var checkStructFilePath = wareDTO.StructureFilePath ?? "";


            var wareDAL = new Ware
            {
                Article = wareDTO.Article.Value,
                WareCategory3 = existedCategory,
                WareTrademark = existedTrademark.Id != 0 ? existedTrademark : null,
                Name = wareDTO.Name,
                Description = wareDTO.Description,
                StructureFilePath = checkStructFilePath,
                Price = wareDTO.Price.Value,
                Discount = wareDTO.Discount != null ? wareDTO.Discount.Value : 0,
                IsDeliveryAvailable = wareDTO.IsDeliveryAvailable.Value,
                Statuses = statuses,
                Images = new List<WareImage>(),
                PriceHistories = new List<WarePriceHistory>(),
                WareItems = new List<WareItem>(),
                OrderItems = new List<OrderItem>(),
                Reviews = new List<WareReview>(),
                CustomerFavorites = new List<Customer>()
            };


            await Database.Wares.Create(wareDAL);
            await Database.Save();

            return _mapper.Map<Ware, WareDTO>(wareDAL);

        }
        public async Task<WareDTO> Update(WareDTO wareDTO)
        {
            var existedWare = await Database.Wares.GetById(wareDTO.Id);
            if (existedWare == null)
            {
                throw new ValidationException("Товару з таким Id не існує!", wareDTO.Id.ToString());
            }
            if (wareDTO.Article == null)
            {
                throw new ValidationException("Артикул не може бути пустим!", wareDTO.Article.ToString());
            }
            //Перевірка на унікальність артикулу та назви
            var existedArticle = await Database.Wares.GetByArticle(wareDTO.Article.Value);
            if (existedArticle != null && existedArticle.Id != wareDTO.Id)
            {
                throw new ValidationException("Товар з таким артикулом вже існує!", wareDTO.Article.ToString());
            }
            if (wareDTO.Name == null)
            {
                throw new ValidationException("Назва не може бути пустою!", wareDTO.Name);
            }
            if (wareDTO.Description == null)
            {
                throw new ValidationException("Опис не може бути пустим!", wareDTO.Description);
            }
            if (wareDTO.Price == null)
            {
                throw new ValidationException("Ціна не може бути пустою!", wareDTO.Price.ToString());
            }
            if (wareDTO.Price < 0)
            {
                throw new ValidationException("Ціна не може бути від'ємною!", wareDTO.Price.ToString());
            }
            if (wareDTO.Discount < 0)
            {
                throw new ValidationException("Знижка не може бути від'ємною!", wareDTO.Discount.ToString());
            }
            if (wareDTO.WareCategory3Id == null)
            {
                throw new ValidationException("Категорія не може бути пустою!", wareDTO.WareCategory3Id.ToString());
            }
            var existedCategory = await Database.Categories3.GetById(wareDTO.WareCategory3Id.Value);
            if (existedCategory == null)
            {
                throw new ValidationException("Категорії з таким Id не існує!", wareDTO.WareCategory3Id.ToString());
            }
            if (wareDTO.IsDeliveryAvailable == null)
            {
                throw new ValidationException("Доставка не може бути пустою!", "");
            }
            //if (wareDTO.StatusIds == null)
            //{
            //    throw new ValidationException("Статус не може бути пустим!", wareDTO.StatusIds.ToString());
            //}

            var existedTrademark = new WareTrademark();
            if (wareDTO.TrademarkId != null)
            {
                existedTrademark = await Database.WareTrademarks.GetById(wareDTO.TrademarkId.Value);
                if (existedTrademark == null)
                {
                    throw new ValidationException("Торгової марки з таким Id не існує!", wareDTO.TrademarkId.ToString());
                }
            }

            existedWare.Article = wareDTO.Article.Value;
            existedWare.WareCategory3 = existedCategory;
            existedWare.WareTrademark = existedTrademark.Id != 0 ? existedTrademark : null;
            existedWare.Name = wareDTO.Name;
            existedWare.Description = wareDTO.Description;
            existedWare.Price = wareDTO.Price.Value;
            existedWare.Discount = wareDTO.Discount != null ? wareDTO.Discount.Value : 0;
            existedWare.StructureFilePath = wareDTO.StructureFilePath ?? "";
            existedWare.IsDeliveryAvailable = wareDTO.IsDeliveryAvailable.Value;

            if (wareDTO.ImageIds != null)
            {
                existedWare.Images.Clear();
                if (wareDTO.ImageIds.Any())
                {
                    await foreach (var wareImage in Database.WareImages.GetByIdsAsync(wareDTO.ImageIds))
                    {
                        if (wareImage == null)
                        {
                            throw new ValidationException($"Одна з WareImage не знайдена!", "");
                        }
                        existedWare.Images.Add(wareImage);
                    }
                }
            }
            if (wareDTO.StatusIds != null)
            {
                existedWare.Statuses.Clear();
                if (wareDTO.StatusIds.Any())
                {
                    await foreach (var status in Database.WareStatuses.GetByIdsAsync(wareDTO.StatusIds))
                    {
                        if (status == null)
                        {
                            throw new ValidationException($"Один з WareStatus не знайдений!", "");
                        }
                        existedWare.Statuses.Add(status);
                    }
                }
            }
            if (wareDTO.PriceHistoryIds != null)
            {
                existedWare.PriceHistories.Clear();
                if (wareDTO.PriceHistoryIds.Any())
                {
                    await foreach (var priceHistory in Database.WarePriceHistories.GetByIdsAsync(wareDTO.PriceHistoryIds))
                    {
                        if (priceHistory == null)
                        {
                            throw new ValidationException($"Один з WarePriceHistory не знайдений!", "");
                        }
                        existedWare.PriceHistories.Add(priceHistory);
                    }
                }
            }
            if (wareDTO.WareItemIds != null)
            {
                existedWare.WareItems.Clear();
                if (wareDTO.WareItemIds.Any())
                {
                    await foreach (var wareItem in Database.WareItems.GetByIdsAsync(wareDTO.WareItemIds))
                    {
                        if (wareItem == null)
                        {
                            throw new ValidationException($"Один з WareItem не знайдений!", "");
                        }
                        existedWare.WareItems.Add(wareItem);
                    }
                }

            }
            if (wareDTO.OrderItemIds != null)
            {
                existedWare.OrderItems.Clear();
                if (wareDTO.OrderItemIds.Any())
                {
                    await foreach (var orderItem in Database.OrderItems.GetByIdsAsync(wareDTO.OrderItemIds))
                    {
                        if (orderItem == null)
                        {
                            throw new ValidationException($"Один з OrderItem не знайдений!", "");
                        }
                        existedWare.OrderItems.Add(orderItem);
                    }
                }
            }
            if (wareDTO.ReviewIds != null)
            {
                existedWare.Reviews.Clear();
                if (wareDTO.ReviewIds.Any())
                {
                    await foreach (var review in Database.WareReviews.GetByIdsAsync(wareDTO.ReviewIds))
                    {
                        if (review == null)
                        {
                            throw new ValidationException($"Один з WareReview не знайдений!", "");
                        }
                        existedWare.Reviews.Add(review);
                    }
                }
            }
            if (wareDTO.CustomerFavoriteIds != null)
            {
                existedWare.CustomerFavorites.Clear();
                if (wareDTO.CustomerFavoriteIds.Any())
                {
                    await foreach (var customer in Database.Customers.GetByIdsAsync(wareDTO.CustomerFavoriteIds))
                    {
                        if (customer == null)
                        {
                            throw new ValidationException($"Один з Customer не знайдений!", "");
                        }
                        existedWare.CustomerFavorites.Add(customer);
                    }

                }
            }

            Database.Wares.Update(existedWare);
            await Database.Save();

            return _mapper.Map<Ware, WareDTO>(existedWare);
        }
        public async Task<WareDTO> Delete(long id)
        {
            var existedId = await Database.Wares.GetById(id);
            if (existedId == null)
            {
                throw new ValidationException("Товару з таким Id не існує!", id.ToString());
            }
            await Database.Wares.Delete(id);
            await Database.Save();

            return _mapper.Map<Ware, WareDTO>(existedId);
        }
    }
}
