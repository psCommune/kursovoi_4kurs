﻿using eLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eLibrary.Data
{
    public class ELibraryContext:DbContext
    {
        public ELibraryContext (DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}