using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Repositories
{
    public class AutherDbRepository : IBookStoreRepository<Auther>
    {
        private readonly BookStoreDbContext db;

        public AutherDbRepository(BookStoreDbContext db)
        {
            this.db = db;
        }
        public void Add(Auther entity)
        {
            db.Authers.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var book = Find(id);
            db.Authers.Remove(book);
            db.SaveChanges();
        }

        public Auther Find(int id)
        {

            var auther = db.Authers.SingleOrDefault(a => a.Id == id);
            return auther;
        }


        public IList<Auther> SearchResult(string searchPhase)
        {
            List<Auther> _authers = db.Authers.Where(a => a.FullName.ToLower().Contains(searchPhase.ToLower())).ToList();
            return _authers;
        }
        public void Update(int id, Auther entity)
        {
            // DRY
            //var auther = Find(id);
            //auther.FullName = entity.FullName;
            db.Update(entity);
            db.SaveChanges();
            //auther.Id = entity.Id;
        }

        public IList<Auther> List()
        {
            return db.Authers.ToList();
        }
    }
}
