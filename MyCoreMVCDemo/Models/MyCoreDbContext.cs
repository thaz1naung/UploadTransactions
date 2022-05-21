using Microsoft.EntityFrameworkCore;
using MyCoreMVCDemo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCoreMVCDemo.Models
{
    public class MyCoreDbContext : DbContext
    {
        public MyCoreDbContext(DbContextOptions<MyCoreDbContext> options)
                  : base(options)
        {

        }
        public virtual DbSet<DataTransaction> DataTransaction { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataTransaction>(entity =>
            {
                entity.Property(e => e.TransGUID)
                    .IsUnicode(false);                 
                 
                

                entity.Property(e => e.TransactionId).IsUnicode(false);

                entity.Property(e => e.Amount).IsUnicode(false);

                entity.Property(e => e.Status).IsUnicode(false);

                entity.Property(e => e.CurrencyCode).IsUnicode(false);

                entity.Property(e => e.TransactionDate).IsUnicode(false);
            });
        }
    }
}
