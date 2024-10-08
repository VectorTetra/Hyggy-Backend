﻿using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Queries;

namespace HyggyBackend.DAL.Interfaces
{
	public interface IAddressRepository
	{
		Task<IEnumerable<Address>> GetAllAsync();
		Task<Address?> GetByIdAsync(long id);

		Task CreateAsync(Address address);
		void Update(Address address);
		Task DeleteAsync(long id);
	}
}
