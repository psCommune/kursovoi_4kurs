using kursovoi_4kurs.Domain.Entities;

namespace kursovoi_4kurs.Domain.Services
{
    public interface ITracksService
    {
        Task<string> LoadFile (Stream file, string path);
        Task<string> LoadPhoto (Stream file, string path);
        Task AddTrack (Track track);
        Task AddPlaylist (Playlist playlist);
        Task UpdateTrack (Track track);
        Task DeleteTrack (Track track);
    }
}
