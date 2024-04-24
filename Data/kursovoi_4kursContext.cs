using kursovoi_4kurs.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace kursovoi_4kurs.Data
{
    public class kursovoi_4kursContext:DbContext
    {
        public kursovoi_4kursContext (DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}
