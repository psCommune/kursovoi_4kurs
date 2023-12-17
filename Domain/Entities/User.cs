﻿using System.ComponentModel.DataAnnotations;

namespace eLibrary.Domain.Entities
{
    public class User:Entity
    {
        [StringLength(100)]
        public string Fullname { get; set; } = null!;

        [StringLength(100)]
        public string Login { get; set; } = null!;

        [StringLength(256)]
        public string Password { get; set; } = null!;

        [StringLength(100)]
        public string Salt { get; set; } = null!;

        [StringLength(250)]
        public string? Photo { get; set; }
        public List<Playlist> Playlists { get; set; } = null!;
        /*public int UserAge { get; set; }*/
        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;
    }
}
