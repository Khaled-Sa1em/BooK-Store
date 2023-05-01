using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Repositories
{
    public class AutherRepository : IBookStoreRepository<Auther>
    {
        private readonly List<Auther> authers;
        public AutherRepository()
        {
            authers = new List<Auther>()
            {
                new Auther
                {
                    Id=1,FullName="khaled"
                }
                ,
                new Auther
                {
                    Id=2,FullName="ali"
                },
                new Auther
                {
                    Id=3,FullName="sameh"
                }
            };
        }
        public void Add(Auther entity)
        {
            authers.Add(entity);
        }

        public void Delete(int id)
        {
            var book = Find(id);
            authers.Remove(book);
        }

        public Auther Find(int id)
        {

            var auther = authers.SingleOrDefault(a => a.Id == id);
            return auther;
        }

        public IList<Auther> List()
        {
            return authers;
        }

        public IList<Auther> SearchResult(string searchPhase)
        {
            //List<Auther> _authers = new List<Auther>();
            List<Auther> _authers = new ();

            foreach (var item in authers)
            {
                if (item.FullName.Contains(searchPhase))
                {
                    _authers.Add(item);
                }
            }

            return _authers;
        }
        public void Update(int id, Auther entity)
        {
            // DRY
            var auther = Find(id);

            auther.FullName = entity.FullName;
            //auther.Id = entity.Id;
        }
    }
}
