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

    internal async Task<ExerciseDTO> GetExerciseByIdAsync(long id)
    {
        return await _exerciseService.GetExerciseByIdAsync(id);
    }
}