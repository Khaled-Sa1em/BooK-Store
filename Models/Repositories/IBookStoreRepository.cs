using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Repositories
{
    public interface IBookStoreRepository<TEntity>
    {
        //public IEnumerable<TEntity> List();
        public IList<TEntity> List();
        TEntity Find(int id);
        void Add(TEntity entity);
        void Update(int id, TEntity entity);
        void Delete(int id);

        //for searching 
        public IList<TEntity> SearchResult(string searchPhase);

    }
}
