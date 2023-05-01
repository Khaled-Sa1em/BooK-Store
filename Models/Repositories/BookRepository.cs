using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Repositories
{
    //TEST repostory
    public class BookRepository : IBookStoreRepository<Book>
    {
        private readonly List<Book> books;
        public BookRepository()
        {
            books = new List<Book>()
            {
                new Book
                {
                    Id=1,Title="cSharp",Description="learn c sharp from scratch",
                    ImageUrl="OIP.jpeg"

                },new Book
                {
                    Id=2,Title="js",Description="do js with us",
                    ImageUrl="OIP1.jpeg"

                },new Book
                {
                    Id=3,Title="java",Description="type java like a pro",
                    ImageUrl="OIP3.jpeg"
                }
            };
        }
        public void Add(Book entity)
        {
            entity.Id = books.Max(b => b.Id) + 1;
            books.Add(entity);
        }

        public void Delete(int id)
        {
            var book = Find(id);
            books.Remove(book);
        }

        public Book Find(int id)
        {

            var book = books.SingleOrDefault(b => b.Id == id);
            return book;
        }

        public IList<Book> List()
        {
            return books;
        }

        public IList<Book> SearchResult(string searchPhase)
        {
            //List<Book> _books = new List<Book>();
            //List<int> list = new List<int>();
            List<Book> _books = new();
            searchPhase = searchPhase.ToLower();
            //if (searchPhase == null || searchPhase[0] == ' ')
            //{
            //    return _books;
            //}
            foreach (var item in books)
            {
                if (item.Title.ToLower().Contains(searchPhase) || item.Description.ToLower().Contains(searchPhase))
                {
                    _books.Add(item);
                }
                // i could refactor this when i change Auther to type =>>>  Auther? Auther nullable
                if (item.Auther != null && item.Auther.FullName.ToLower().Contains(searchPhase))
                {
                    _books.Add(item);
                }
            }

            return _books;
        }

        public void Update(int id, Book entity)
        {
            // DRY
            var book = Find(id);

            book.Title = entity.Title;
            //book.Id = entity.Id;
            book.Description = entity.Description;
            book.Auther = entity.Auther;
            book.ImageUrl = entity.ImageUrl;
        }
    }
}
