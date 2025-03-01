using ExerciseTracker.Data;
using ExerciseTracker.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExerciseTracker.Repository;

public class ExerciseRepository<T> : IExerciseRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;

    public ExerciseRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<int> AddExerciseAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        return await _context.SaveChangesAsync(); // returns number entries written to db
    }

    public async Task<int> DeleteExerciseAsync(T entity)
    {

        _dbSet.Remove(entity);
        return await _context.SaveChangesAsync();
    }

    public async Task<T?> GetExerciseByIdAsync(long id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<List<T>?> GetExerciseListAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<int> UpdateExerciseAsync(long id, T entity)
    {
        var savedExercise = await _dbSet.FindAsync(id);

        if (savedExercise != null)
        {
            _dbSet.Entry(savedExercise).CurrentValues.SetValues(entity);
            return await _context.SaveChangesAsync();
        }
        return 0;
    }
}
