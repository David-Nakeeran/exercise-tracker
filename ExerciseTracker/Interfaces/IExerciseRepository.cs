namespace ExerciseTracker.Interfaces;

public interface IExerciseRepository<T> where T : class
{
    public Task<List<T>?> GetExerciseListAsync();

    public Task<T?> GetExerciseByIdAsync(long id);

    public Task<int> AddExerciseAsync(T entity);

    public Task<int> UpdateExerciseAsync(long id, T entity);

    public Task<int> DeleteExerciseAsync(T entity);
}