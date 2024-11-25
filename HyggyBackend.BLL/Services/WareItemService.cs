using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Queries;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;

namespace HyggyBackend.BLL.Services
{
    public class WareItemService : IWareItemService
    {
        IUnitOfWork Database;
        IMapper _mapper;

        public WareItemService(IUnitOfWork uow, IMapper mapper)
        {
            Database = uow;
            _mapper = mapper;
        }
        public async Task<WareItemDTO?> GetById(long id)
        {
            var wareItem = await Database.WareItems.GetById(id);
            if (wareItem == null)
            {
                return null;
            }
            return _mapper.Map<WareItem, WareItemDTO>(wareItem);
        }
        public async Task<IEnumerable<WareItemDTO>> GetByStringIds(string stringIds)
        {
            var wareItems = await Database.WareItems.GetByStringIds(stringIds);
            return _mapper.Map<IEnumerable<WareItem>, IEnumerable<WareItemDTO>>(wareItems);
        }
        public async Task<IEnumerable<WareItemDTO>> GetByArticle(long article)
        {
            var wareItems = await Database.WareItems.GetByArticle(article);
            return _mapper.Map<IEnumerable<WareItem>, IEnumerable<WareItemDTO>>(wareItems);
        }
        public async Task<IEnumerable<WareItemDTO>> GetByWareId(long wareId)
        {
            var wareItems = await Database.WareItems.GetByWareId(wareId);
            return _mapper.Map<IEnumerable<WareItem>, IEnumerable<WareItemDTO>>(wareItems);
        }
        public async Task<IEnumerable<WareItemDTO>> GetByWareName(string wareName)
        {
            var wareItems = await Database.WareItems.GetByWareName(wareName);
            return _mapper.Map<IEnumerable<WareItem>, IEnumerable<WareItemDTO>>(wareItems);
        }
        public async Task<IEnumerable<WareItemDTO>> GetByWareDescription(string wareDescription)
        {
            var wareItems = await Database.WareItems.GetByWareDescription(wareDescription);
            return _mapper.Map<IEnumerable<WareItem>, IEnumerable<WareItemDTO>>(wareItems);
        }
        public async Task<IEnumerable<WareItemDTO>> GetByWarePriceRange(float minPrice, float maxPrice)
        {
            var wareItems = await Database.WareItems.GetByWarePriceRange(minPrice, maxPrice);
            return _mapper.Map<IEnumerable<WareItem>, IEnumerable<WareItemDTO>>(wareItems);
        }
        public async Task<IEnumerable<WareItemDTO>> GetByWareDiscountRange(float minDiscount, float maxDiscount)
        {
            var wareItems = await Database.WareItems.GetByWareDiscountRange(minDiscount, maxDiscount);
            return _mapper.Map<IEnumerable<WareItem>, IEnumerable<WareItemDTO>>(wareItems);
        }
        public async Task<IEnumerable<WareItemDTO>> GetByWareStatusId(long statusId)
        {
            var wareItems = await Database.WareItems.GetByWareStatusId(statusId);
            return _mapper.Map<IEnumerable<WareItem>, IEnumerable<WareItemDTO>>(wareItems);
        }
        public async Task<IEnumerable<WareItemDTO>> GetByWareCategory3Id(long wareCategory3Id) 
        {
            var wareItems = await Database.WareItems.GetByWareCategory3Id(wareCategory3Id);
            return _mapper.Map<IEnumerable<WareItem>, IEnumerable<WareItemDTO>>(wareItems);
        }
        public async Task<IEnumerable<WareItemDTO>> GetByWareCategory2Id(long wareCategory2Id) 
        {
            var wareItems = await Database.WareItems.GetByWareCategory2Id(wareCategory2Id);
            return _mapper.Map<IEnumerable<WareItem>, IEnumerable<WareItemDTO>>(wareItems);
        }
        public async Task<IEnumerable<WareItemDTO>> GetByWareCategory1Id(long wareCategory1Id) 
        {
            var wareItems = await Database.WareItems.GetByWareCategory1Id(wareCategory1Id);
            return _mapper.Map<IEnumerable<WareItem>, IEnumerable<WareItemDTO>>(wareItems);
        }
        public async Task<IEnumerable<WareItemDTO>> GetByWareImageId(long wareImageId) 
        {
            var wareItems = await Database.WareItems.GetByWareImageId(wareImageId);
            return _mapper.Map<IEnumerable<WareItem>, IEnumerable<WareItemDTO>>(wareItems);
        }
        public async Task<IEnumerable<WareItemDTO>> GetByPriceHistoryId(long priceHistoryId) 
        {
            var wareItems = await Database.WareItems.GetByPriceHistoryId(priceHistoryId);
            return _mapper.Map<IEnumerable<WareItem>, IEnumerable<WareItemDTO>>(wareItems);
        }
        public async Task<IEnumerable<WareItemDTO>> GetByOrderItemId(long orderItemId)
        {
            var wareItems = await Database.WareItems.GetByOrderItemId(orderItemId);
            return _mapper.Map<IEnumerable<WareItem>, IEnumerable<WareItemDTO>>(wareItems);
        }
        public async Task<IEnumerable<WareItemDTO>> GetByIsDeliveryAvailable(bool isDeliveryAvailable)
        {
            var wareItems = await Database.WareItems.GetByIsDeliveryAvailable(isDeliveryAvailable);
            return _mapper.Map<IEnumerable<WareItem>, IEnumerable<WareItemDTO>>(wareItems);
        }
        public async Task<IEnumerable<WareItemDTO>> GetByStorageId(long storageId) 
        {
            var wareItems = await Database.WareItems.GetByStorageId(storageId);
            return _mapper.Map<IEnumerable<WareItem>, IEnumerable<WareItemDTO>>(wareItems);
        }
        public async Task<IEnumerable<WareItemDTO>> GetByShopId(long shopId) 
        {
            var wareItems = await Database.WareItems.GetByShopId(shopId);
            return _mapper.Map<IEnumerable<WareItem>, IEnumerable<WareItemDTO>>(wareItems);
        }
        public async Task<IEnumerable<WareItemDTO>> GetByQuantityRange(long minQuantity, long maxQuantity) 
        {
            var wareItems = await Database.WareItems.GetByQuantityRange(minQuantity, maxQuantity);
            return _mapper.Map<IEnumerable<WareItem>, IEnumerable<WareItemDTO>>(wareItems);
        }
        public async Task<IEnumerable<WareItemDTO>> GetPagedWareItems(int pageNumber, int pageSize) 
        {
            var wareItems = await Database.WareItems.GetPagedWareItems(pageNumber, pageSize);
            return _mapper.Map<IEnumerable<WareItem>, IEnumerable<WareItemDTO>>(wareItems);
        }
        public async Task<IEnumerable<WareItemDTO>> GetByQuery(WareItemQueryBLL query) 
        {
            var wareItems = await Database.WareItems.GetByQuery(_mapper.Map<WareItemQueryBLL, WareItemQueryDAL>(query));
            return _mapper.Map<IEnumerable<WareItem>, IEnumerable<WareItemDTO>>(wareItems);
        }
        public async Task<WareItemDTO> Create(WareItemDTO WareItemDTO) 
        {
            var ExistedWare = await Database.Wares.GetById(WareItemDTO.WareId);
            if (ExistedWare == null)
            {
                throw new ValidationException($"Товару з вказаним Id не існує! Id: {WareItemDTO.WareId}","") ;
            }

            var ExistedStorage = await Database.Storages.GetById(WareItemDTO.StorageId);
            if (ExistedStorage == null)
            {
                throw new ValidationException($"Складу з вказаним Id не існує! Id: {WareItemDTO.StorageId}", "");
            }

            var ExistedWareItem = await Database.WareItems.ExistsAsync(WareItemDTO.WareId, WareItemDTO.StorageId);
            if (ExistedWareItem != null)
            {
                throw new ValidationException($"Запис для товару з Id {WareItemDTO.WareId} на складі з Id {WareItemDTO.StorageId} вже існує.", "");
            }

            var wareItem = new WareItem
            {
                Ware = ExistedWare,
                Storage = ExistedStorage,
                Quantity = WareItemDTO.Quantity
            };

            await Database.WareItems.Create(wareItem);
            await Database.Save();

            return _mapper.Map<WareItem, WareItemDTO>(wareItem);
        }
        public async Task<WareItemDTO> Update(WareItemDTO WareItemDTO) 
        {

            var isExistedId = await Database.WareItems.GetById(WareItemDTO.Id);
            if (isExistedId == null)
            {
                throw new ValidationException($"Запис для товару з вказаним Id не існує! Id: {WareItemDTO.Id}", "");
            }
            var ExistedWare = await Database.Wares.GetById(WareItemDTO.WareId);
            if (ExistedWare == null)
            {
                throw new ValidationException($"Товару з вказаним Id не існує! Id: {WareItemDTO.WareId}", "");
            }

            var ExistedStorage = await Database.Storages.GetById(WareItemDTO.StorageId);
            if (ExistedStorage == null)
            {
                throw new ValidationException($"Складу з вказаним Id не існує! Id: {WareItemDTO.StorageId}", "");
            }
            var ExistedWareItem = await Database.WareItems.ExistsAsync(WareItemDTO.WareId, WareItemDTO.StorageId);
            if (ExistedWareItem != null && ExistedWareItem.Id != WareItemDTO.Id)
            {
                throw new ValidationException($"Запис для товару з Id {WareItemDTO.WareId} на складі з Id {WareItemDTO.StorageId} вже існує.", "");
            }

            isExistedId.Id = WareItemDTO.Id;
            isExistedId.Ware = ExistedWare;
            isExistedId.Storage = ExistedStorage;
            isExistedId.Quantity = WareItemDTO.Quantity;
           

            Database.WareItems.Update(isExistedId);
            await Database.Save();

            return _mapper.Map<WareItem, WareItemDTO>(isExistedId);
        }
        public async Task<WareItemDTO> Delete(long id) 
        {
            var wareItem = await Database.WareItems.GetById(id);
            if (wareItem == null)
            {
                throw new ValidationException($"Товару з вказаним Id не існує! Id: {id}", "");
            }

            await Database.WareItems.Delete(id);
            await Database.Save();

            return _mapper.Map<WareItem, WareItemDTO>(wareItem);
        }
    }
}
