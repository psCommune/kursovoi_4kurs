using eLibrary.Domain.Entities;

namespace eLibrary.ViewModels
{
    public class BooksCatalogViewModel
    {
        public List<Book> Books { get; set; }
        public List<Category> Categories { get; set; }
        public List<Track> Tracks { get; set; }
        public List<Author> Authors { get; set; }
        public List<Playlist> Playlists { get; set; }
    }
}
