namespace ExerciseTracker.Models;

public class ExerciseDTO
{
    public int Id { get; set; }

    public DateTime DateStart { get; set; }

    public DateTime DateEnd { get; set; }

    public TimeSpan Duration { get; }

    public string Comments { get; set; } = string.Empty;
}