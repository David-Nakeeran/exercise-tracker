namespace ExerciseTracker.Models;

public class ExerciseDTO
{
    public long Id { get; set; }

    public DateTime DateStart { get; set; }

    public DateTime DateEnd { get; set; }

    public TimeSpan Duration => DateEnd - DateStart;

    public string Comments { get; set; } = string.Empty;
}