using eLibrary.Domain.Entities;
using eLibrary.Domain.Services;
using System.Security.Cryptography;
using System.Text;

namespace eLibrary.Infrastructure
{
    public class UserService : IUserService
    {
        public async Task<User?> GetUserAsync (string username, string password)//проверка логина и пароля
        {
            username = username.Trim();
            User? user = (await users.FindWhere(u => u.Login == username)).FirstOrDefault();
            if (user is null)
            {
                return null;
            }
            string hashPassword = GetSha256(password, user.Salt);
            if (user.Password != hashPassword)
            {
                return null;
            }
            return user;
        }
        public async Task<bool> IsUserExistsAsync (string username)//проверка наличия пользователя в базе данных
        {
            username = username.Trim();
            User? found = (await users.FindWhere(u => u.Login == username)).FirstOrDefault();
            return found is not null;
        }

        public async Task<User> RegistrationAsync (string fullname, string username, string password)//регистрация
        {
            //проверяем, есть ли пользователь с таким же username
            bool userExists = await IsUserExistsAsync(username);
            if (userExists)
                throw new ArgumentException("Username already exists");

            // находим роль "клиент"
            Role? clientRole = (await roles.FindWhere(r => r.Name == "client")).FirstOrDefault();

            if (clientRole is null)
                throw new InvalidOperationException("Role 'client' not found in database");

            // добавляем пользователя
            User toAdd = new User
            {
                Fullname = fullname,
                Login = username,
                Salt = GetSalt(),
                RoleId = clientRole.Id
            };
            toAdd.Password = GetSha256(password, toAdd.Salt);

            return await users.AddAsync(toAdd);
        }
        
        private readonly IRepository<User> users;
        private readonly IRepository<Role> roles;
        public UserService (IRepository<User> usersRepository, IRepository<Role> rolesRepository)
        {
            users = usersRepository;
            roles = rolesRepository;
        }
        private string GetSalt () => DateTime.UtcNow.ToString() + DateTime.Now.Ticks;//Соль - склейка из: текущая дата и время, и текущая дата и время в виде 100-наносекундных тиков
        private string GetSha256 (string password, string salt)//метод для хэширования
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password + salt);//склеиваем пароль и соль, после чего их преобразуем в массив байт
            byte[] hashBytes = SHA256.HashData(passwordBytes);//вычисляем хэш
            return Convert.ToBase64String(hashBytes);//кодируем в виде Base-64 строки
        }
    }
}
