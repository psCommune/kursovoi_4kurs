namespace kursovoi_4kurs.Domain.Entities
{
    public class TrackPlaylist: Entity
    {
        public int PlaylistId { get; set; }
        public Playlist Playlist { get; set; }

        public int TrackId { get; set; }
        public Track Track { get; set; }
    }
}
