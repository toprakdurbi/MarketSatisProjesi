using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketSatisProjesi.Models
{
    public class AppDbContext : DbContext 
        // classa entity frameworkten gelen dbcontext class ı inherite edillir (miras / kalıtım)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //hangi db'ye tanımlanacağı adres tanımlanır.
            optionsBuilder.UseSqlServer(@"Server=TOPRAK\MSSQLSERVER01;Database=MarketDB;Integrated Security=True; TrustServerCertificate=true;");

        }
    }
}
