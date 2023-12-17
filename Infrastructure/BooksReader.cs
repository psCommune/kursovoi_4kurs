using eLibrary.Domain.Entities;
using eLibrary.Domain.Services;

namespace eLibrary.Infrastructure
{
    public class BooksReader :IBooksReader
    {
        private readonly IRepository<Book> books;
        private readonly IRepository<Category> categories;

        public BooksReader (IRepository<Book> books, IRepository<Category> categories)
        {
            this.books = books;
            this.categories = categories;
        }

        public async Task<Book?> FindBookAsync (int bookId) => await books.FindAsync(bookId);//Метод поиска книги

        public async Task<List<Book>> GetAllBooksAsync () => await books.GetAllAsync();//Метод получения всех книг
        public async Task<List<Book>> FindBooksAsync (string searchString, int categoryId) => (searchString, categoryId) switch //Метод фильтрации книг с новым switch через pattern matching
        {
            ("" or null, 0) => await books.GetAllAsync(),
            (_, 0) => await books.FindWhere(book => book.Title.Contains(searchString) || book.Author.Contains(searchString)),
            (_, _) => await books.FindWhere(book => book.CategoryId == categoryId &&
                (book.Title.Contains(searchString) || book.Author.Contains(searchString))),
        };

        public async Task<List<Category>> GetCategoriesAsync () => await categories.GetAllAsync(); //Получение категорий
    }
}
