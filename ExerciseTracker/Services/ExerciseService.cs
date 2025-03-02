using ExerciseTracker.Interface;
using ExerciseTracker.Interfaces;
using ExerciseTracker.Models;

namespace ExerciseTracker.Services;

public class ExerciseService
{
    private readonly IExerciseRepository<Exercise> _exerciseRepo;
    private readonly IExerciseMapper _exerciseMapper;

    public ExerciseService(IExerciseRepository<Exercise> exerciseRepository, IExerciseMapper exerciseMapper)
    {
        _exerciseRepo = exerciseRepository;
        _exerciseMapper = exerciseMapper;
    }

    internal async Task<List<ExerciseDTO>> GetAllExercisesAsync()
    {
        var exercises = await _exerciseRepo.GetExerciseListAsync();
        var exercisesDTO = exercises.Select(exercise =>
            _exerciseMapper.ExerciseToDTO(exercise)).ToList();
        return exercisesDTO;
    }

    internal async Task<ExerciseDTO> GetExerciseByIdAsync(long id)
    {
        var exercise = await _exerciseRepo.GetExerciseByIdAsync(id);
        var exerciseDTO = _exerciseMapper.ExerciseToDTO(exercise);
        return exerciseDTO;
    }

}
