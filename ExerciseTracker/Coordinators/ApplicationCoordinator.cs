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

    private readonly Validation _validation;

    public ApplicationCoordinator(UserInput userInput, ExerciseController exerciseController, DisplayManager displayManager, Validation validation)
    {
        _userInput = userInput;
        _exerciseController = exerciseController;
        _displayManager = displayManager;
        _validation = validation;
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
                    await CreateExercise();
                    break;
                case "Update exercise":
                    await UpdateExercise();
                    break;
                case "Delete exercise":
                    await DeleteExercise();
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

    internal async Task<Exercise?> GetExercise(long id)
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

    internal async Task CreateExercise()
    {
        var startTime = _userInput.GetExerciseTimes("Please enter the start date and time in 'dd-mm-yyyy hh:mm' format, for example '24-02-2025 13:15'");
        var endTime = _userInput.GetExerciseTimes("Please enter the end date and time in 'dd-mm-yyyy hh:mm' format, for example '24-02-2025 15:30' later than the start");
        while (!_validation.IsEndTimeLaterThanStartTime(startTime, endTime))
        {
            endTime = _userInput.GetExerciseTimes("Please enter the end date and time in 'dd-mm-yyyy hh:mm' format, for example '24-02-2025 15:30' later than the start");
        }
        var comments = _userInput.GetComments("Please enter comments about the exercise");

        var createdExercise = new ExerciseDTO
        {
            DateStart = startTime,
            DateEnd = endTime,
            Comments = comments
        };

        var result = await _exerciseController.IsExerciseAddedAsync(createdExercise);

        if (result)
        {
            _displayManager.ShowMessage("Exercise created successfully");
            _userInput.WaitForUserInput();
        }
        else
        {
            _displayManager.ShowMessage("Failed to create exercise, returning to main menu...");
            _userInput.WaitForUserInput();
        }
    }

    internal async Task UpdateExercise()
    {
        await AllExercises();

        long displayId = _userInput.GetId("Please enter the id of exercise you wish to update or enter 0 to return to main menu");
        if (displayId == 0) return;

        Exercise? exerciseObject = await GetExercise(displayId);

        if (exerciseObject == null)
        {
            _displayManager.ShowMessage("Exercise does not exist, returning to main menu...");
            _userInput.WaitForUserInput();
            return;
        }

        var startTime = _userInput.GetExerciseTimes("Please enter the start date and time in 'dd-mm-yyyy hh:mm' format, for example '24-02-2025 13:15'");
        var endTime = _userInput.GetExerciseTimes("Please enter the end date and time in 'dd-mm-yyyy hh:mm' format, for example '24-02-2025 15:30' later than the start");
        while (!_validation.IsEndTimeLaterThanStartTime(startTime, endTime))
        {
            endTime = _userInput.GetExerciseTimes("Please enter the end date and time in 'dd-mm-yyyy hh:mm' format, for example '24-02-2025 15:30' later than the start");
        }

        var comments = _userInput.GetComments("Please enter comments about the exercise");

        exerciseObject.DateStart = startTime;
        exerciseObject.DateEnd = endTime;
        exerciseObject.Comments = comments;

        var updatedExercise = await _exerciseController.IsExerciseUpdatedAsync(exerciseObject.Id, exerciseObject);

        if (updatedExercise)
        {
            _displayManager.ShowMessage("Exercise has been updated successfully");
            _userInput.WaitForUserInput();
        }
        else
        {
            _displayManager.ShowMessage("Exercise has failed to update, returning to main menu...");
            _userInput.WaitForUserInput();
        }
    }

    internal async Task DeleteExercise()
    {
        await AllExercises();

        var displayId = _userInput.GetId("Please enter the id of exercise or enter 0 to return to main menu");
        if (displayId == 0) return;

        var idPairs = await GetKeyValuePairsExercises();

        if (!idPairs.ContainsKey(displayId))
        {
            _displayManager.IncorrectId();
            _userInput.WaitForUserInput();
            return;
        }

        long exerciseId = idPairs[displayId];
        var exerciseToBeDeleted = await GetExercise(exerciseId);

        var result = await _exerciseController.IsExerciseDeleteAsync(exerciseToBeDeleted);

        if (result)
        {
            _displayManager.ShowMessage("Exercise deleted successfully");
            _userInput.WaitForUserInput();
        }
        else
        {
            _displayManager.ShowMessage("Exercise doesn't exist, returning to main menu...");
            _userInput.WaitForUserInput();
        }
    }
}