using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PetSuppliesPlus.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// The data set of the repository
        /// </summary>
        private IDbSet<T> _dbSet;

        public Repository(DbSet<T> dbSet)
        {
            _dbSet = dbSet;
        }

        /// <summary>
        /// Gets all entities
        /// </summary>        
        /// <returns>All entities</returns>
        public IEnumerable<T> GetAll()
        {
            return this._dbSet;
        }

        /// <summary>
        /// Gets all entities matching the predicate
        /// </summary>
        /// <param name="predicate">The filter clause</param>
        /// <returns>All entities matching the predicate</returns>
        public IEnumerable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return this._dbSet.Where(predicate);
        }

        /// <summary>
        /// Set based on where condition
        /// </summary>
        /// <param name="predicate">The predicate</param>
        /// <returns>The records matching the given condition</returns>
        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return this._dbSet.Where(predicate);
        }

        /// <summary>
        /// Finds an entity matching the predicate
        /// </summary>
        /// <param name="predicate">The filter clause</param>
        /// <returns>An entity matching the predicate</returns>
        public IEnumerable<T> Find(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return this._dbSet.Where(predicate);
        }

        /// <summary>
        /// Determines if there are any entities matching the predicate
        /// </summary>
        /// <param name="predicate">The filter clause</param>
        /// <returns>True if a match was found</returns>
        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return this._dbSet.Any(predicate);
        }

        /// <summary>
        /// Returns the first entity that matches the predicate
        /// </summary>
        /// <param name="predicate">The filter clause</param>
        /// <returns>An entity matching the predicate</returns>
        public T First(Expression<Func<T, bool>> predicate)
        {
            return this._dbSet.First(predicate);
        }

        /// <summary>
        /// Returns the first entity that matches the predicate else null
        /// </summary>
        /// <param name="predicate">The filter clause</param>
        /// <returns>An entity matching the predicate else null</returns>
        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return this._dbSet.FirstOrDefault(predicate);
        }

        /// <summary>
        /// Returns the entity that matches the predicate
        /// </summary>
        /// <param name="predicate">The filter clause</param>
        /// <returns>An entity matching the predicate</returns>
        public T Single(Expression<Func<T, bool>> predicate)
        {
            return this._dbSet.Single(predicate);
        }

        /// <summary>
        /// Returns the entity that matches the predicate else null
        /// </summary>
        /// <param name="predicate">The filter clause</param>
        /// <returns>An entity matching the predicate else null</returns>
        public T SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            return this._dbSet.SingleOrDefault(predicate);
        }

        /// <summary>
        /// Adds a given entity to the context
        /// </summary>
        /// <param name="entity">The entity to add to the context</param>
        public void Add(T entity)
        {
            this._dbSet.Add(entity);
        }

        /// <summary>
        /// Deletes a given entity from the context
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        public void Delete(T entity)
        {
            this._dbSet.Remove(entity);
        }

        /// <summary>
        /// Attaches a given entity to the context
        /// </summary>
        /// <param name="entity">The entity to attach</param>
        public void Attach(T entity)
        {
            this._dbSet.Attach(entity);
        }
    }
}
