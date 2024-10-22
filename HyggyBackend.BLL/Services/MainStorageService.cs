//using AutoMapper;
//using HyggyBackend.BLL.DTO;
//using HyggyBackend.BLL.Interfaces;
//using HyggyBackend.DAL.Entities;
//using HyggyBackend.DAL.Interfaces;
//using Microsoft.Identity.Client.Extensions.Msal;

//namespace HyggyBackend.BLL.Services
//{
//	public class MainStorageService : IMainStorageService
//	{
//		IUnitOfWork Database { get; set; }
//		private readonly IMapper _mapper;

//		public MainStorageService(IUnitOfWork database, IMapper mapper)
//		{
//			Database = database;
//			_mapper = mapper;
//		}

//		public async Task<IEnumerable<MainStorageDto>> GetAll()
//		{
//			var storages = await Database.MainStorages.GetAll();

//			return _mapper.Map<IEnumerable<MainStorage>, IEnumerable<MainStorageDto>>(storages);
//		}
//		public async Task<MainStorageDto?> GetByAddress(long addressId)
//		{
//			var storage = await Database.MainStorages.GetMainStorageByAddress(addressId);

//			return _mapper.Map<MainStorageDto>(storage);
//		}
//		public async Task<MainStorageDto?> GetById(int id)
//		{
//			var storage = await Database.MainStorages.GetMainStorage(id);

//			return _mapper.Map<MainStorageDto>(storage);
//		}
//		public async Task<bool> IsStorageExist(long id)
//		{
//			var storages = await Database.MainStorages.GetAll();

//			return storages.Any(storage => storage.Id == id);
//		}
//		public async Task<bool> IsStorageExistByAddress(long? addressId)
//		{
//			var storages = await Database.MainStorages.GetAll();
//			var storage = storages.Select(s => s.AddressId);

//			return storage.Any(s => s == addressId);
//		}
//		public async Task<MainStorageDto> Create(MainStorageDto mainStorageDto)
//		{
//			var storage = _mapper.Map<MainStorage>(mainStorageDto);

//			await Database.MainStorages.Create(storage);
//			await Database.Save();

//			return mainStorageDto;
//		}
//		public void Update(MainStorageDto mainStorageDto)
//		{
//			var storage = _mapper.Map<MainStorage>(mainStorageDto);

//			Database.MainStorages.Update(storage);
//			Database.Save();
//		}
//		public async Task Delete(long id)
//		{
//			await Database.MainStorages.Delete(id);
//			await Database.Save();
//		}
//	}
//}
