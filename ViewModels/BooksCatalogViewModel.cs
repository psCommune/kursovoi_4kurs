using eLibrary.Domain.Entities;

namespace eLibrary.ViewModels
{
    public class BooksCatalogViewModel
    {
        public List<Book> Books { get; set; }
        public List<Category> Categories { get; set; }
    }
}
