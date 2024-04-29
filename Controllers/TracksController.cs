using kursovoi_4kurs.Domain.Entities;
using kursovoi_4kurs.Domain.Services;
using kursovoi_4kurs.Infrastructure;
using kursovoi_4kurs.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace kursovoi_4kurs.Controllers
{
    public class TracksController : Controller
    {
        private readonly ITracksReader reader;
        private readonly ITracksService tracksService;
        private readonly IWebHostEnvironment appEnvironment;
        public TracksController(ITracksReader reader, ITracksService tracksService, IWebHostEnvironment appEnvironment)
        {
            this.reader = reader;
            this.tracksService = tracksService;
            this.appEnvironment = appEnvironment;
        }

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
            // загружаем список плейлистов (List<Playlist>)
            var playlists = await reader.GetPlaylistsAsync();
            // получаем элементы для <select> с помощью нашего листа категорий
            // (List<SelectListItem>)
            var items = playlists.Select(c => new SelectListItem { Text = c.Title, Value = c.Id.ToString() });
            // добавляем список в модель представления
            trackVm.Playlists.AddRange(items);
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

                track.Filename = await tracksService.LoadFile(trackVm.File.OpenReadStream(), Path.Combine(wwwroot, "audio"));
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
                Title = track.Title,
                FileString = track.Filename,
                PhotoString = track.ImageUrl,
                PlaylistId = track.PlaylistId 
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
            // выведем сообщение если трек не найден
            if (track is null)
            {
                ModelState.AddModelError("not_found", "Трек не найден!");
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
                        Path.Combine(wwwroot, "audio")
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

        [HttpGet]
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
        public async Task<IActionResult> DeleteTrackPost(int id)
        {
            var track = await reader.FindTrackAsync(id);
            try
            {
                await tracksService.DeleteTrack(track);
            }
            catch (Exception)
            {
                ModelState.AddModelError("database", "Ошибка при удалении");
                return View(track);
            }
            return RedirectToAction("Index", "Tracks");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddPlaylist()
        {
            var viewModel = new PlaylistViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddPlaylist(PlaylistViewModel playlistVm)
        {
            if (!ModelState.IsValid)
            {
                return View(playlistVm);
            }
            try
            {
                var playlist = new Playlist
                {
                    Title = playlistVm.Title,
                    Description = playlistVm.Description,
                    CreateDate = DateTime.Now
                };
                string wwwroot = appEnvironment.WebRootPath; // получаем путь до wwwroot

                playlist.ImageUrl = await tracksService.LoadPhoto(playlistVm.Photo.OpenReadStream(), Path.Combine(wwwroot, "images", "tracks"));
                await tracksService.AddPlaylist(playlist);
            }
            catch (IOException)
            {
                ModelState.AddModelError("ioerror", "Не удалось сохранить файл.");
                return View(playlistVm);
            }
            catch
            {
                ModelState.AddModelError("database", "Ошибка при сохранении в базу данных.");
                return View(playlistVm);
            }
            return RedirectToAction("Index", "Tracks");
        }
    }
}
