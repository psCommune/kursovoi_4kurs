using eLibrary.Domain.Entities;
using eLibrary.Domain.Services;
using eLibrary.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eLibrary.Controllers
{
    public class BooksController :Controller
    {
        [Authorize]
        public async Task<IActionResult> Index (string ?searchString = "", int categoryId = 0)
        {
            var viewModel = new BooksCatalogViewModel
            {
                Books = await reader.FindBooksAsync(searchString, categoryId),
                Categories = await reader.GetCategoriesAsync()
            };

            return View(viewModel);
        }

        private readonly IBooksReader reader;
        private readonly IBooksService booksService;
        private readonly IWebHostEnvironment appEnvironment;
        public BooksController (IBooksReader reader, IBooksService booksService, IWebHostEnvironment appEnvironment)
        {
            this.reader = reader;
            this.booksService = booksService;
            this.appEnvironment = appEnvironment;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddBook ()
        {
            var viewModel = new BookViewModel();

            // загружаем список категорий (List<Category>)
            var categories = await reader.GetCategoriesAsync();

            // получаем элементы для <select> с помощью нашего листа категорий
            // (List<SelectListItem>)
            var items = categories.Select(c =>
                new SelectListItem { Text = c.Name, Value = c.Id.ToString() });

            // добавляем список в модель представления
            viewModel.Categories.AddRange(items);
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddBook (BookViewModel bookVm)
        {
            if (!ModelState.IsValid)
            {
                return View(bookVm);
            }

            try
            {
                var book = new Book
                {
                    Author = bookVm.Author,
                    Title = bookVm.Title,
                    CategoryId = bookVm.CategoryId,
                    PagesCount = bookVm.PageCount,
                    Description = bookVm.Description
                };
                string wwwroot = appEnvironment.WebRootPath; // получаем путь до wwwroot

                book.Filename =
                    await booksService.LoadFile(bookVm.File.OpenReadStream(), Path.Combine(wwwroot, "books"));
                book.ImageUrl =
                    await booksService.LoadPhoto(bookVm.Photo.OpenReadStream(), Path.Combine(wwwroot, "images", "books"));
                await booksService.AddBook(book);
            }
            catch (IOException)
            {
                ModelState.AddModelError("ioerror", "Не удалось сохранить файл.");
                return View(bookVm);
            }
            catch
            {
                ModelState.AddModelError("database", "Ошибка при сохранении в базу данных.");
                return View(bookVm);
            }

            return RedirectToAction("Index", "Books");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateBook (int bookId)
        {
            var book = await reader.FindBookAsync(bookId);
            if (book is null)
            {
                return NotFound();
            }
            var bookVm = new UpdateBookViewModel
            {
                Id = book.Id,
                Author = book.Author,
                Title = book.Title,
                CategoryId = book.CategoryId,
                PageCount = book.PagesCount,
                FileString = book.Filename,
                PhotoString = book.ImageUrl
            };

            var categories = await reader.GetCategoriesAsync();
            var items = categories.Select(c =>
                new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
            bookVm.Categories.AddRange(items);

            return View(bookVm);
        }
    }
}
