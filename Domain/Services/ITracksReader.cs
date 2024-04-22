using eLibrary.Domain.Entities;

namespace eLibrary.Domain.Services
{
    public interface ITracksReader
    {
        Task<List<Track>> GetAllTracksAsync();
        Task<List<Track>> FindTracksAsync(string searchString, int playlistId);
        Task<Track?> FindTrackAsync(int trackId);
        Task<List<Playlist>> GetPlaylistsAsync();
    }
}
