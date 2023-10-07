﻿using AspNetCoreMvc_ETicaret_DataAccess.Identity;
using AspNetCoreMvc_ETicaret_Entity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wissen.Bright.BlogProject.App.Entity.Entities;

namespace AspNetCoreMvc_ETicaret_DataAccess.Context
{
    public class ECommerceDbContext : IdentityDbContext<AppUser , AppRole, int>
    {
        public ECommerceDbContext(DbContextOptions options) : base(options) { }

        DbSet<Products> Products { get; set; }
        DbSet<ProductSpecs> ProductSpecs { get; set; }
        DbSet<Categories> Categories { get; set; }
        DbSet<Cart> Carts { get; set; }
        DbSet<CartLine> CartLines { get; set; }
        DbSet<Sale> Sale { get; set; }
        DbSet<SaleDetails> SaleDetails { get; set; }
        DbSet<FilterSpecs> FilterSpecs { get; set; }
        DbSet<Comment> Comments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasOne(d => d.Products)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ProductsId);
            });
            base.OnModelCreating(modelBuilder);
        }

    }
}
