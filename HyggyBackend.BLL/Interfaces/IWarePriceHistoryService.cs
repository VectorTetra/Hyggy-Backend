﻿using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Queries;
using HyggyBackend.DAL.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.Interfaces
{
    public interface IWarePriceHistoryService
    {
        Task<WarePriceHistoryDTO?> GetById(long id);
        Task<IEnumerable<WarePriceHistoryDTO>> GetByWareId(long wareId);
        Task<IEnumerable<WarePriceHistoryDTO>> GetByPriceRange(float minPrice, float maxPrice);
        Task<IEnumerable<WarePriceHistoryDTO>> GetByDateRange(DateTime startDate, DateTime endDate);
        Task<IEnumerable<WarePriceHistoryDTO>> GetByQuery(WarePriceHistoryQueryBLL query);
        Task<WarePriceHistoryDTO> Create(WarePriceHistoryDTO warePriceHistoryDTO);
        Task<WarePriceHistoryDTO> Update(WarePriceHistoryDTO warePriceHistoryDTO);
        Task<WarePriceHistoryDTO> Delete(long id);
    }
}
