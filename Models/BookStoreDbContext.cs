// Microsoft.EntityFrameworkCore =>> version 5
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    // i will use it in the startup class as a service to give me ability to inject it any were i want 
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        {

        }

        // Tables
        public DbSet<Book> Books { get; set; }
        public DbSet<Auther> Authers { get; set; }
    }
}
