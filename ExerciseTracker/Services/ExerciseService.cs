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

    internal async Task<Exercise> GetExerciseByIdAsync(long id)
    {
        var exercise = await _exerciseRepo.GetExerciseByIdAsync(id);
        return exercise;
    }

    internal async Task<bool> IsExerciseAddedAsync(ExerciseDTO exerciseDTO)
    {
        var exercise = new Exercise
        {
            DateStart = exerciseDTO.DateStart,
            DateEnd = exerciseDTO.DateEnd,
            Comments = exerciseDTO.Comments
        };
        var exerciseAdded = await _exerciseRepo.AddExerciseAsync(exercise);
        if (exerciseAdded != 1) return false;
        return true;
    }

    internal async Task<bool> IsExercisesUpdatedAsync(long id, Exercise exercise)
    {
        int exerciseUpdated = await _exerciseRepo.UpdateExerciseAsync(id, exercise);
        if (exerciseUpdated != 1) return false;
        return true;
    }

    internal async Task<bool> IsExerciseDeletedAsync(Exercise exercise)
    {
        int exerciseDeleted = await _exerciseRepo.DeleteExerciseAsync(exercise);
        if (exerciseDeleted != 1) return false;
        return true;
    }
}
