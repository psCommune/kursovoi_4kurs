using eLibrary.Domain.Entities;
using eLibrary.Domain.Services;

namespace eLibrary.Infrastructure
{
    public class TracksReader : ITracksReader
    {
        private readonly IRepository<Track> tracks;
        private readonly IRepository<Playlist> playlists;

        public TracksReader(IRepository<Track> tracks, IRepository<Playlist> playlists)
        {
            this.tracks = tracks;
            this.playlists = playlists;
        }
        public async Task<Track?> FindTrackAsync(int trackId) => await tracks.FindAsync(trackId);//Метод поиска трека
        public async Task<List<Track>> GetAllTracksAsync() => await tracks.GetAllAsync();//Метод получения всех аудио
        public async Task<List<Track>> FindTracksAsync(string searchString, int playlistId) => (searchString, playlistId) switch //Метод фильтрации аудио с новым switch через pattern matching
        {
            ("" or null, 0) => await tracks.GetAllAsync(),
            (_, 0) => await tracks.FindWhere(track => track.Title.Contains(searchString) || track.Author.Contains(searchString)),
            (_, _) => await tracks.FindWhere(track => track.PlaylistId == playlistId && (track.Title.Contains(searchString) || track.Author.Contains(searchString)))
        };

        public async Task<List<Playlist>> GetPlaylistsAsync() => await playlists.GetAllAsync();//Получение плейлистов
    }
}
