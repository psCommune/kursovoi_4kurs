using kursovoi_4kurs.Data;
using kursovoi_4kurs.Domain.Entities;
using kursovoi_4kurs.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace kursovoi_4kurs.Infrastructure
{
    public class EFRepository<T> :IRepository<T> where T : Entity
    {
        public async Task<T> AddAsync (T entity)//добавление элементов
        {
            context.Entry(entity).State = EntityState.Added;
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync (T entity)//изменение элементов
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync (T entity)//удаление элементов
        {
            context.Entry(entity).State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }

        public async Task<T?> FindAsync (int id)//поиск элемента
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> FindWhere (Expression<Func<T, bool>> predicate)//получение элементов с учетом фильтрации
        {
            return await context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<List<T>> GetAllAsync ()//получение всех элементов
        {
            return await context.Set<T>().ToListAsync();
        }

       
        private readonly kursovoi_4kursContext context;

        public EFRepository (kursovoi_4kursContext context)
        {
            this.context = context;
        }
    }
}
