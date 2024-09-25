using HyggyBackend.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Interfaces
{
	public interface IMainStorageRepository
	{
		Task<IEnumerable<MainStorage>> GetAll();
		Task<MainStorage?> GetMainStorage(long id);
		Task<MainStorage?> GetMainStorageByAddress(long addressId);

		Task Create(MainStorage mainStorage);
		void Update(MainStorage mainStorage);
		Task Delete(long id);
	}
}
