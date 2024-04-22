using eLibrary.Domain.Entities;
using eLibrary.Domain.Services;
using eLibrary.Infrastructure;
using eLibrary.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eLibrary.Controllers
{
    public class TracksController : Controller
    {
        [Authorize]
        public async Task<IActionResult> Index (string ?searchString = "", int playlistId = 0)
        {
            var viewModel = new TracksCatalogViewModel
            {
                Tracks = await reader.FindTracksAsync(searchString, playlistId),
                Playlists = await reader.GetPlaylistsAsync()
            };
            return View(viewModel);
        }

        private readonly ITracksReader reader;
        private readonly ITracksService tracksService;
        private readonly IWebHostEnvironment appEnvironment;
        public TracksController(ITracksReader reader, ITracksService tracksService, IWebHostEnvironment appEnvironment)
        {
            this.reader = reader;
            this.tracksService = tracksService;
            this.appEnvironment = appEnvironment;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddTrack()
        {
            var viewModel = new TrackViewModel();

            // загружаем список плейлистов (List<Playlist>)
            var playlists = await reader.GetPlaylistsAsync();

            // получаем элементы для <select> с помощью нашего листа плейлистов
            // (List<SelectListItem>)
            var items = playlists.Select(c => new SelectListItem { Text = c.Title, Value = c.Id.ToString() });

            // добавляем список в модель представления
            viewModel.Playlists.AddRange(items);
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddTrack(TrackViewModel trackVm)
        {
            if (!ModelState.IsValid)
            {
                return View(trackVm);
            }
            try
            {
                var track = new Track
                {
                    Author = trackVm.Author,
                    Title = trackVm.Title,
                    PlaylistId = trackVm.PlaylistId
                };
                string wwwroot = appEnvironment.WebRootPath; // получаем путь до wwwroot

                track.Filename = await tracksService.LoadFile(trackVm.File.OpenReadStream(), Path.Combine(wwwroot, "tracks"));
                track.ImageUrl = await tracksService.LoadPhoto(trackVm.Photo.OpenReadStream(), Path.Combine(wwwroot, "images", "tracks"));
                await tracksService.AddTrack(track);
            }
            catch (IOException)
            {
                ModelState.AddModelError("ioerror", "Не удалось сохранить файл.");
                return View(trackVm);
            }
            catch
            {
                ModelState.AddModelError("database", "Ошибка при сохранении в базу данных.");
                return View(trackVm);
            }

            return RedirectToAction("Index", "Tracks");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateTrack(int trackId)
        {
            var track = await reader.FindTrackAsync(trackId);
            if (track is null)
            {
                return NotFound();
            }
            var trackVm = new UpdateTrackViewModel
            {
                Id = track.Id,
                Author = track.Author,
                Title = track.Title
                /*PlaylistId = track.PlaylistId*/ //кофликт в Track - нельзя приравнивать если там ставить "?"
            };

            var playlists = await reader.GetPlaylistsAsync();
            var items = playlists.Select(c => new SelectListItem { Text = c.Title, Value = c.Id.ToString() });
            trackVm.Playlists.AddRange(items);

            return View(trackVm);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateTrack(UpdateTrackViewModel trackVm)
        {
            // загружаем список плейлистов (List<Playlist>)
            var playlists = await reader.GetPlaylistsAsync();
            // получаем элементы для <select> с помощью нашего листа категорий
            // (List<SelectListItem>)
            var items = playlists.Select(c => new SelectListItem { Text = c.Title, Value = c.Id.ToString() });
            // добавляем список в модель представления
            trackVm.Playlists.AddRange(items);
            // если модель не валидна, то возвращаем пользователя на форму
            if (!ModelState.IsValid)
            {
                return View(trackVm);
            }
            // находим трек по Id
            var track = await reader.FindTrackAsync(trackVm.Id);
            // если трек почему-то не найден, то выведем сообщение
            if (track is null)
            {
                ModelState.AddModelError("not_found", "Аудио не найдено!");
                return View(trackVm);
            }
            try
            {
                // заполняем поля трека
                track.Author = trackVm.Author;
                track.PlaylistId = trackVm.PlaylistId;
                track.Title = trackVm.Title;
                // получаем путь до wwwroot
                string wwwroot = appEnvironment.WebRootPath;
                // если формой был передан файл, то меняем его
                if (trackVm.File is not null)
                {
                    track.Filename = await tracksService.LoadFile(
                        trackVm.File.OpenReadStream(),
                        Path.Combine(wwwroot, "tracks")
                    );
                }
                // если формой было передано изображение, то меняем его
                if (trackVm.Photo is not null)
                {
                    track.ImageUrl = await tracksService.LoadPhoto(
                        trackVm.Photo.OpenReadStream(),
                        Path.Combine(wwwroot, "images", "tracks")
                    );
                }
                // обновляем файл
                await tracksService.UpdateTrack(track);
            }
            catch (IOException)
            {
                ModelState.AddModelError("ioerror", "Не удалось сохранить файл.");
                return View(trackVm);
            }
            catch
            {
                ModelState.AddModelError("database", "Ошибка при сохранении в базу данных.");
                return View(trackVm);
            }
            return RedirectToAction("Index", "Tracks");
        }

        /*[HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteTrack(int trackId)
        {
            var track = await reader.FindTrackAsync(trackId);
            if (track == null)
            {
                return NotFound();
            }
            return View(track);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteTrackPost(int trackId)
        {
            var track = await reader.FindTrackAsync(trackId);
            try
            {
                await TracksService.DeleteTrack(track);
            }
            catch (Exception)
            {
                ModelState.AddModelError("database", "Ошибка при удалении");
                return View(track);

            }
            return RedirectToAction("Index", "Tracks");
        }*/
    }
}
