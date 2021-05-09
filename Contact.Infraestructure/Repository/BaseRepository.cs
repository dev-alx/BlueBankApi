using Contact.Core.Interfaces;
using Contact.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Infraestructure.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly ContactDbContext context;
        protected readonly DbSet<T> entities;

        public BaseRepository(ContactDbContext context)
        {
            this.context = context;
            this.entities = context.Set<T>();
        }       

        public async Task<IEnumerable<T>> GetAll()
        {
            return await entities.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await entities.FindAsync(id);
        }

        public async Task Add(T entity)
        {
            await entities.AddAsync(entity);
        }

        public async Task Delete(int id)
        {
            T entity = await GetById(id);
            entities.Remove(entity);
        }

        public void Update(T entity)
        {
            entities.Update(entity);
        }
    }
}
