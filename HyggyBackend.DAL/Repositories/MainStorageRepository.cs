//using HyggyBackend.DAL.EF;
//using HyggyBackend.DAL.Entities;
//using HyggyBackend.DAL.Interfaces;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace HyggyBackend.DAL.Repositories
//{
//	public class MainStorageRepository : IMainStorageRepository
//	{
//		private readonly HyggyContext _context;

//		public MainStorageRepository(HyggyContext context)
//		{
//			_context = context;
//		}
//		public async Task<IEnumerable<MainStorage>> GetAll()
//		{
//			return await _context.MainStorages.ToListAsync();
//		}
//		public async Task<MainStorage?> GetMainStorage(long id)
//		{
//			return await _context.MainStorages.Where(s => s.Id == id).FirstOrDefaultAsync(); ;
//		}
//		public async Task<MainStorage?> GetMainStorageByAddress(long addressId)
//		{
//			return await _context.MainStorages.Where(s => s.AddressId == addressId).FirstOrDefaultAsync(); ;

//		}
//		public async Task Create(MainStorage mainStorage)
//		{
//			await _context.MainStorages.AddAsync(mainStorage);
//		}
//		public void Update(MainStorage mainStorage)
//		{
//			MainStorage? storage = _context.MainStorages.Where(s => s.Id == mainStorage.Id).FirstOrDefault();
//			storage.AddressId = mainStorage.AddressId;
//		}
//		public async Task Delete(long id)
//		{
//			var storage = await GetMainStorage(id);
//			if(storage != null) 
//				_context.MainStorages.Remove(storage);
//		}

		
//	}
//}
