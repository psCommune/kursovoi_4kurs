using kursovoi_4kurs.Domain.Entities;

namespace kursovoi_4kurs.Domain.Services
{
    public interface ITracksReader
    {
        Task<List<Track>> GetAllTracksAsync();
        Task<List<Track>> FindTracksAsync(string searchString, int playlistId);
        Task<Track?> FindTrackAsync(int trackId);
        Task<List<Playlist>> GetPlaylistsAsync();
    }
}
