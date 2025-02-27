using ExerciseTracker.Interfaces;
using ExerciseTracker.Models;

namespace ExerciseTracker.Services;

public class ExerciseService
{
    private readonly IExerciseRepository<Exercise> _exerciseService;

    public ExerciseService(IExerciseRepository<Exercise> exerciseRepository)
    {
        _exerciseService = exerciseRepository;
    }

}
