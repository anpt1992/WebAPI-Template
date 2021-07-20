using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebAPI_Template.Data;
using WebAPI_Template.Domain;

namespace WebAPI_Template.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DataContext _context;
        public Repository(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add new entity
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        /// <summary>
        /// Delete an entity
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(T entity)
        {
            T existing = _context.Set<T>().Find(entity);
            if (existing != null)
                _context.Set<T>().Remove(existing);
        }

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities"></param>
        public void DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(int id, bool allowTracking = true)
        {
            return _context.Set<T>().FirstOrDefault(c =>
            ((int)c.GetType().GetProperty("Id").GetValue(c) == id));
        }

        /// <summary>
        /// Get entity by lambda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T Get(Expression<Func<T, bool>> predicate, bool allowTracking = true)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        /// <summary>
        /// Get list of entities
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll(bool allowTracking = true)
        {
            return _context.Set<T>().AsEnumerable();
        }

        /// <summary>
        /// Get entites by lambda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<T> GetMany(Expression<Func<T, bool>> predicate, bool allowTracking = true)
        {
            return _context.Set<T>().Where(predicate).AsEnumerable();
        }

        /// <summary>
        /// Update an entity
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<T>().Attach(entity);
        }

        /// <summary>
        /// Get entities from sql string
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public IEnumerable<T> FromSqlQuery(string sql, bool allowTracking = true)
        {
            if (allowTracking)
            {
                return _context.Set<T>().FromSqlRaw(sql).AsEnumerable();
            }

            return _context.Set<T>().FromSqlRaw(sql).AsNoTracking().AsEnumerable();
        }

        /// <summary>
        /// Get all entities async
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllAsync(bool allowTracking = true)
        {
            var data = await _context.Set<T>().ToListAsync();
            return data;
        }

        /// <summary>
        /// Get entities by lambda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> predicate,
            bool allowTracking = true)
        {
            var data = await _context.Set<T>().Where(predicate).ToListAsync();
            return data;
        }

        /// <summary>
        /// Get entities with pagination and order by
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetManyPaginationAsync<O>(Expression<Func<T, bool>> predicate, Expression<Func<T, O>> orderBy, int pageNumber, int pageSize, bool allowTracking = true)
        {
            var skip = (pageNumber - 1) * pageSize;
            var data = await _context.Set<T>().Where(predicate).OrderBy(orderBy).Skip(skip).Take(pageSize).ToListAsync();
            return data;
        }

        /// <summary>
        /// Get entity by id async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetByIdAsync(int id, bool allowTracking = true)
        {
            var data = await _context.Set<T>().FirstOrDefaultAsync(c =>
            ((int)c.GetType().GetProperty("Id").GetValue(c) == id));

            return data;
        }

        /// <summary>
        /// Get entities by lambda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, bool allowTracking = true)
        {
            T data;

            if (allowTracking)
            {
                data = await _context.Set<T>().FirstOrDefaultAsync(predicate);
            }
            else
            {
                data = await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);
            }
            return data;
        }

        /// <summary>
        /// Get entities from sql string async
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> FromSqlQueryAsync(string sql, bool allowTracking = true)
        {
            IEnumerable<T> data;

            if (allowTracking)
            {
                data = await _context.Set<T>().FromSqlRaw(sql).ToListAsync();
            }
            else
            {
                data = await _context.Set<T>().FromSqlRaw(sql).AsNoTracking().ToListAsync();
            }
            return data;
        }
    }
}
