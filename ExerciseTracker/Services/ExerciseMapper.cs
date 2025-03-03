using ExerciseTracker.Interface;
using ExerciseTracker.Models;

namespace ExerciseTracker.Services;

public class ExerciseMapper : IExerciseMapper
{
    public ExerciseDTO ExerciseToDTO(Exercise exercise) =>
        new ExerciseDTO
        {
            Id = exercise.Id,
            DateStart = exercise.DateStart,
            DateEnd = exercise.DateEnd,
            Comments = exercise.Comments
        };
}