using Spectre.Console;
using Flashcards.kilozdazolik.Enums;
using Flashcards.kilozdazolik.Controller;
using Flashcards.kilozdazolik.Data;

namespace Flashcards.kilozdazolik;

public class UserInterface
{
    private static StackController _stackController = new();
    internal static void MainMenu()
    {
        while (true)
        {
            var choice = AnsiConsole.Prompt(new SelectionPrompt<MenuAction>().Title("What do you want to do [green]next[/]?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to choose an option)[/]")
                .AddChoices(Enum.GetValues<MenuAction>()));

            switch (choice)
            {
                case MenuAction.Exit:
                    Environment.Exit(0);
                    break;
                case MenuAction.ViewAllStacks:
                    _stackController.ViewAllStacks();
                    break;
                case MenuAction.ManageStacks:
                    ManageStacks();
                    break;
                    
            }
        }
    }

    private static void ManageStacks()
    {
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<StackAction>()
                .Title("What do you want to do [green]next[/]?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to choose an option)[/]")
                .AddChoices(Enum.GetValues<StackAction>()));

        switch (choice)
        {
            case StackAction.Insert:
                _stackController.CreateStack();
                break;
            case StackAction.Update:
                _stackController.EditStack();
                break;
            case StackAction.Delete:
                _stackController.DeleteStack();
                break;
        }
    }
}