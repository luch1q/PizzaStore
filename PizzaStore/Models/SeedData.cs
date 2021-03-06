﻿using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace PizzaStore.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices
                .GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            if (!context.Products.Any())
            {
                context.Categories.AddRange(
                    new Category
                    {
                        Name = "35cm"
                    });
                context.Products.AddRange(
                    new Product
                    {
                        Name = "Pizza1", Description = "nice pizza",
                        CategoryID = 1, Price = 270
                    },
                    new Product
                    {
                        Name = "Pizza2",
                        Description = "nice pizza2",
                        CategoryID = 1,
                        Price = 50.04M
                    },
                    new Product
                    {
                        Name = "Pizza3",
                        Description = "nice pizza3",
                        CategoryID = 1,
                        Price = 10.03M
                    },
                    new Product
                    {
                        Name = "Pizza4",
                        Description = "nice pizza4",
                        CategoryID = 1,
                        Price = 10.03M
                    },
                    new Product
                    {
                        Name = "Pizza5",
                        Description = "nice pizza5",
                        CategoryID = 1,
                        Price = 10.03M
                    },
                    new Product
                    {
                        Name = "Pizza6",
                        Description = "nice pizza6",
                        CategoryID = 1,
                        Price = 10.03M
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
