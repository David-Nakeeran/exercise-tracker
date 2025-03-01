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
}