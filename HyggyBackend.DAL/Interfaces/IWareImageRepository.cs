﻿using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Queries;

namespace HyggyBackend.DAL.Interfaces
{
    public interface IWareImageRepository
    {
        Task<WareImage?> GetById(long id);
        Task<IEnumerable<WareImage>> GetByWareId(long wareId);
        Task<IEnumerable<WareImage>> GetByStringIds(string stringIds);
        Task<IEnumerable<WareImage>> GetByWareArticle(long wareArticle);
        Task<IEnumerable<WareImage>> GetByPathSubstring(string path);
        Task<IEnumerable<WareImage>> GetByQuery(WareImageQueryDAL queryDAL);
        IAsyncEnumerable<WareImage> GetByIdsAsync(IEnumerable<long> ids);
        Task Create(WareImage wareImage);
        void Update(WareImage wareImage);
        Task Delete(long id);
    }
}
