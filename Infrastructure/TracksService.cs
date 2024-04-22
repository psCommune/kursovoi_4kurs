using eLibrary.Domain.Entities;
using eLibrary.Domain.Services;
using static System.Reflection.Metadata.BlobBuilder;

namespace eLibrary.Infrastructure
{
    public class TracksService : ITracksService
    {
        private readonly IRepository<Track> tracks;
        public TracksService(IRepository<Track> tracks)
        {
            this.tracks = tracks;
        }
        public async Task AddTrack(Track track)
        {
            await tracks.AddAsync(track);
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
