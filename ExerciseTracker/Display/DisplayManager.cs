using ExerciseTracker.Models;
using ExerciseTracker.Utilities;
using Spectre.Console;

namespace ExerciseTracker.Display;

class DisplayManager
{

    internal void RenderGetAllExercisesTable(List<ExerciseDTO> exercises)
    {
        Console.Clear();
        var table = new Table();
        table.AddColumns("DisplayId", "Start Date", "End Date", "Duration", "Comments");
        int count = 1;
        foreach (var exercise in exercises)
        {
            var start = exercise.DateStart.ToString("dd-MM-yyyy HH:mm");
            var end = exercise.DateEnd.ToString("dd-MM-yyyy HH:mm");
            table.AddRow($"{count}", $"{start}", $"{end}", $"{exercise.Duration.ToString(@"hh\:mm")}", $"{exercise.Comments}");
            count++;
        }
        AnsiConsole.Write(table);
    }

    internal void RenderGetShiftByIdTable(ExerciseDTO exercise)
    {
        Console.Clear();
        var table = new Table();
        table.AddColumns("Start Date", "End Date", "Duration", "Comments");

        var start = exercise.DateStart.ToString("dd-MM-yyyy HH:mm");
        var end = exercise.DateEnd.ToString("dd-MM-yyyy HH:mm");
        table.AddRow($"{start}", $"{end}", $"{exercise.Duration.ToString(@"hh\:mm")}", $"{exercise.Comments}");

        AnsiConsole.Write(table);
    }

    internal void ShowMessage(string message)
    {
        AnsiConsole.WriteLine(message);
    }

    internal void IncorrectId()
    {
        AnsiConsole.WriteLine("Id entered does not match, returning to main menu");
    }
}