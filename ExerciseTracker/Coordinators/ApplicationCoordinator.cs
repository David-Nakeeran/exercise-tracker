using ExerciseTracker.Controllers;
using ExerciseTracker.Display;
using ExerciseTracker.Models;
using ExerciseTracker.Utilities;

namespace ExerciseTracker.Coordinators;

class ApplicationCoordinator
{
    private readonly UserInput _userInput;

    private readonly ExerciseController _exerciseController;

    private readonly DisplayManager _displayManager;

    public ApplicationCoordinator(UserInput userInput, ExerciseController exerciseController, DisplayManager displayManager)
    {
        _userInput = userInput;
        _exerciseController = exerciseController;
        _displayManager = displayManager;
    }

    internal async Task Start()
    {
        bool isAppActive = true;

        while (isAppActive)
        {
            var userSelection = _userInput.ShowMenu();

            switch (userSelection)
            {
                case "View all exercises":
                    await AllExercises();
                    break;
                case "Create exercise":

                    break;
                case "Update exercise":

                    break;
                case "Delete exercise":

                    break;
                case "Quit application":
                    isAppActive = false;
                    break;
            }
        }
    }

    internal async Task<List<ExerciseDTO>> GetAllExercises()
    {
        var exercises = await _exerciseController.GetAllExercisesAsync();
        return exercises;
    }

    internal async Task AllExercises()
    {
        var exercises = await GetAllExercises();
        _displayManager.RenderGetAllExercisesTable(exercises);
        _userInput.WaitForUserInput();
    }

    internal async Task<Dictionary<long, long>> GetKeyValuePairsExercises()
    {
        var exercises = await GetAllExercises();
        var keyValuePairs = new Dictionary<long, long>();
        long displayId = 1;

        foreach (var exercise in exercises)
        {
            keyValuePairs[displayId] = exercise.Id;
            displayId++;
        }
        return keyValuePairs;
    }

    internal async Task<ExerciseDTO?> GetExercise(long id)
    {
        var idPairs = await GetKeyValuePairsExercises();
        if (!idPairs.ContainsKey(id))
        {
            _displayManager.IncorrectId();
            _userInput.WaitForUserInput();
            return null;
        }

        long exerciseId = idPairs[id];

        return await _exerciseController.GetExerciseByIdAsync(exerciseId);
    }
}