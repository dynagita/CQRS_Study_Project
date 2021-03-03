using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WritableRESTAPI.Entity;
using WritableRESTAPI.Repository.Interface;

namespace WritableRESTAPI.Repository.Impl
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        protected DbContext _context { get; private set;}
        protected DbSet<T> _dbSet { get; private set; }
        public RepositoryBase(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual async Task<T> Get(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<T> Delete(int id)
        {

            var entityDb = _dbSet.Find(id);
            entityDb.Active = false;

            _context.Entry(entityDb).Property(x => x.Id).IsModified = false;
            _context.Entry(entityDb).CurrentValues.SetValues(entityDb);
            
            await _context.SaveChangesAsync();

            return entityDb;

        }

        public virtual async Task<T> Insert(T entity)
        {
            entity.Active = true;

            NormalizeForeignKeys(entity);

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> Update(int id, T entity)
        {
            var entityDb = await _dbSet.FindAsync(id);
            entity.Id = entityDb.Id;
            entity.Active = entityDb.Active;

            NormalizeForeignKeys(entity);

            _context.Entry(entityDb).Property(x => x.Id).IsModified = false;
            _context.Entry(entityDb).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        protected virtual T NormalizeForeignKeys(T entity)
        {
            return entity;
        }
    }
}
