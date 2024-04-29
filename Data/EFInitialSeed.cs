using kursovoi_4kurs.Domain.Entities;

namespace kursovoi_4kurs.Data
{
    public class EFInitialSeed
    {
        public static void Seed (kursovoi_4kursContext context)//Сделаем посев начальными значениями в случае, если база данных пуста:
        {
            if(!context.Roles.Any())
            {
                Role client = new Role
                {
                    Name = "client"
                };
                Role admin = new Role
                {
                    Name = "admin"
                };
                context.Roles.Add(client);
                context.Roles.Add(admin);

                context.SaveChanges();
            }
            if (!context.Playlists.Any())
            {
                Playlist firstPlaylist = new Playlist
                {
                    Title = "Первый плейлист",
                    Description = "Смотрю что работает"
                };
                Playlist secondPlaylist = new Playlist
                {
                    Title = "Второй плейлист",
                    Description = "Вроде с этим все хорошо"
                };
                context.Playlists.Add(firstPlaylist);
                context.Playlists.Add(secondPlaylist);

                context.SaveChanges();
            }
            if (!context.Authors.Any())
            {
                Author firstAuthor = new Author
                {
                    Pseudonym = "Алексей Никитин"
                };
                Author secondAuthor = new Author
                {
                    Pseudonym = "Анрей Панин"
                };
                context.Authors.Add(firstAuthor);
                context.Authors.Add(secondAuthor);

                context.SaveChanges();
            }
            if (!context.Tracks.Any() && context.Playlists.Any())
            {
                Track firstTrack = new Track
                {
                    Title = "Первая запись",
                    Author = "Супер музыкант",
                    ImageUrl = "first.png",
                    Filename = "1.mp3",
                    PlaylistId = 1
                };
                Track secondTrack = new Track
                {
                    Title = "Вторая запись",
                    Author = "Другой музыкант",
                    ImageUrl = "second.png",
                    Filename = "2.mp3",
                    PlaylistId = 2
                };
                Track thirdTrack = new Track
                {
                    Title = "Третья запись",
                    Author = "Супер музыкант",
                    ImageUrl = "third.png",
                    Filename = "3.mp3"
                };
                Track fourthTrack = new Track
                {
                    Title = "key",
                    Author = "C418",
                    ImageUrl = "volume.png",
                    Filename = "key.mp3"
                };
                context.Tracks.Add(firstTrack);
                context.Tracks.Add(secondTrack);
                context.Tracks.Add(thirdTrack);
                context.Tracks.Add(fourthTrack);
            }    
            if(!context.Users.Any())
            {
                User firstClietn = new User
                {
                    Fullname = "Даня",
                    Login = "firstClietn",
                    Password = "iZAderbGYYoCSNmhalMuyA5bbX9lilHs9GzVh4o+qcY=",
                    Salt = "27.04.2024 12:57:57638498374774999880",
                    RoleId = 1
                };
                User firstAdmin = new User
                {
                    Fullname = "Даниил",
                    Login = "firstAdmin",
                    Password = "iZAderbGYYoCSNmhalMuyA5bbX9lilHs9GzVh4o+qcY=",
                    Salt = "27.04.2024 12:57:57638498374774999880",
                    RoleId = 2
                };
                context.Users.Add(firstClietn);
                context.Users.Add(firstAdmin);
            }
            context.SaveChanges();
        }
    }
}
