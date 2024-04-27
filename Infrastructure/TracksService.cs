﻿using kursovoi_4kurs.Domain.Entities;
using kursovoi_4kurs.Domain.Services;
using static System.Reflection.Metadata.BlobBuilder;

namespace kursovoi_4kurs.Infrastructure
{
    public class TracksService : ITracksService
    {
        private readonly IRepository<Track> tracks;
        private readonly IRepository<Playlist> playlists;
        public TracksService(IRepository<Track> tracks, IRepository<Playlist> playlists)
        {
            this.tracks = tracks;
            this.playlists = playlists;
        }
        public async Task AddTrack(Track track)
        {
            await tracks.AddAsync(track);
        }

        public async Task AddPlaylist(Playlist playlist)
        {
            await playlists.AddAsync(playlist);
        }

        public async Task DeleteTrack(Track track)
        {
            await tracks.DeleteAsync(track);
        }

        public async Task UpdateTrack(Track track)
        {
            await tracks.UpdateAsync(track);
        }

        private async Task CopyFromStream(Stream stream, string filename)
        {
            using (var writer = new FileStream(filename, FileMode.Create))
            {
                int count = 0;
                byte[] buffer = new byte[1024];
                do
                {
                    count = await stream.ReadAsync(buffer, 0, buffer.Length);
                    await writer.WriteAsync(buffer, 0, count);

                } while (count > 0);
            }
        }

        public async Task<string> LoadFile(Stream file, string path)
        {
            var filename = Path.GetRandomFileName() + ".mp3";
            var fullname = Path.Combine(path, filename);

            await CopyFromStream(file, fullname);

            return filename;
        }

        public async Task<string> LoadPhoto(Stream file, string path)
        {
            var filename = Path.GetRandomFileName() + ".png";
            var fullname = Path.Combine(path, filename);

            await CopyFromStream(file, fullname);

            return filename;
        }

        
    }
}
