using ExerciseTracker.Utilities;

namespace ExerciseTracker.Coordinators;

class ApplicationCoordinator
{
    private readonly UserInput _userInput;

    public ApplicationCoordinator(UserInput userInput)
    {
        _userInput = userInput;
    }

    internal void Start()
    {
        bool isAppActive = true;

        while (isAppActive)
        {
            var userSelection = _userInput.ShowMenu();

            switch (userSelection)
            {
                case "View all exercises":

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
}