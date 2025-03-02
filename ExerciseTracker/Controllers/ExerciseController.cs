using ExerciseTracker.Models;
using ExerciseTracker.Services;

namespace ExerciseTracker.Controllers;

public class ExerciseController
{
    private readonly ExerciseService _exerciseService;

    public ExerciseController(ExerciseService exerciseService)
    {
        _exerciseService = exerciseService;
    }
    internal async Task<List<ExerciseDTO>> GetAllExercisesAsync()
    {
        return await _exerciseService.GetAllExercisesAsync();
    }

    internal async Task<Exercise> GetExerciseByIdAsync(long id)
    {
        return await _exerciseService.GetExerciseByIdAsync(id);
    }

    internal async Task<bool> IsExerciseDeleteAsync(Exercise exercise)
    {
        return await _exerciseService.IsExerciseDeletedAsync(exercise);
    }
}