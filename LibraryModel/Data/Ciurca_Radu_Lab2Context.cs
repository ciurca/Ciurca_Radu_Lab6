using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LibraryModel.Models;

namespace LibraryModel.Data
{
    public class Ciurca_Radu_Lab2Context : DbContext
    {
        public Ciurca_Radu_Lab2Context(DbContextOptions<Ciurca_Radu_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Book> Book { get; set; } = default!;
        public DbSet<Customer> Customer { get; set; } = default!;
        public DbSet<Genre> Genre { get; set; } = default!;
        public DbSet<Authors> Authors { get; set; } = default!;
        public DbSet<Order> Order { get; set; } = default!;

        public DbSet<Publisher> Publisher { get; set; } = default!;
        public DbSet<PublishedBook> PublishedBooks { get; set; } = default!;
        public DbSet<City> City { get; set; } = default!;

    }
}

