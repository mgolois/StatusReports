using Microsoft.Data.Entity;
using StatusReports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatusReports.Data
{
    public interface IRepository<T>
    {
        void Add(T obj);
        IQueryable<T> GetAll();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAndSaveAsync(T obj);
        Task<bool> SaveAsync();
        void Update(T obj);
        Task<T> UpdateAndSaveAsync(T obj);

        Task<LookupData> GetLookupData();

    }
  
}
