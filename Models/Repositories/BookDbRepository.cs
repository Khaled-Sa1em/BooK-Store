using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Repositories
{
    //TEST repostory
    public class BookDbRepository : IBookStoreRepository<Book>
    {
        private readonly List<Book> books;
        private readonly BookStoreDbContext db;

        public BookDbRepository(BookStoreDbContext db)
        {
            this.db = db;
        }
        public void Add(Book entity)
        {
            //entity.Id = books.Max(b => b.Id) + 1;
            db.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var book = Find(id);
            db.Remove(book);
            db.SaveChanges();

        }

        public Book Find(int id)
        {

            var book = db.Books.Include(a => a.Auther).SingleOrDefault(b => b.Id == id);
            return book;
        }

        public IList<Book> List()
        {
            return db.Books.Include(a => a.Auther).ToList();
        }

        public IList<Book> SearchResult(string searchPhase)
        {
            searchPhase = searchPhase.ToLower();
            List<Book> _books = db.Books.Include(a => a.Auther).Where(b => b.Title.ToLower().Contains(searchPhase) || b.Description.ToLower().Contains(searchPhase) || b.Auther.FullName.ToLower().Contains(searchPhase)).ToList();
            return _books;
        }

        public void Update(int id, Book entity)
        {
            db.Update(entity);
            db.SaveChanges();
        }
    }
}
