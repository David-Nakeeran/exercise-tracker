using ExerciseTracker.Models;

namespace ExerciseTracker.Interface;

public interface IExerciseMapper
{
    ExerciseDTO ExerciseToDTO(Exercise exercise);
}