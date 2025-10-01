using Flashcards.kilozdazolik.Data;
using Flashcards.kilozdazolik.Models;
using Spectre.Console;

namespace Flashcards.kilozdazolik.Controller;

public class StackController
{
    private StackRepository _stackRepository = new();

    public void ViewAllStacks()
    {
        List<Stack> allStack = _stackRepository.GetAllStacks();

        if (allStack.Any())
        {
            var table = new Table();
            table.Border(TableBorder.Rounded);

            table.AddColumn("[yellow]ID[/]");
            table.AddColumn("[yellow]Name[/]");

            foreach (var stack in allStack)
            {
                table.AddRow(
                    $"[green]{stack.StackId.ToString()}[/]",
                    $"[blue]{stack.Name}[/]"
                );
            }

            AnsiConsole.Write(table);
        }
        else
        {
            AnsiConsole.MarkupLine("There are no stacks available");
        }
    }

    public void CreateStack()
    {
        bool success = false;
        do
        {
            AnsiConsole.MarkupLine("[yellow]Create a New Stack[/]");
            string input = AnsiConsole.Prompt(new TextPrompt<string>("Please enter the name of the new stack:"));


            var stack = new Stack()
            {
                Name = input
            };

            try
            {
                _stackRepository.InsertStack(stack);
                AnsiConsole.MarkupLine("[green]Stack successfully created![/]");
                success = true;
            }
            catch (InvalidOperationException ex)
            {
                AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine("[red]Something went wrong while creating the stack.[/]");
                success = true; // it will stop looping if unexpected error happens
            }
        } while (!success);
    }

    public void EditStack()
    {
        var allStacks = _stackRepository.GetAllStacks();
    
        if (allStacks.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No stacks are available to edit.[/]");
            Console.ReadKey();
            return;
        }
        
        var stackToEdit = AnsiConsole.Prompt(
            new SelectionPrompt<Stack>()
                .Title("Select a [cyan]STACK[/] to edit:")
                .UseConverter(s => $"{s.Name}")
                .AddChoices(allStacks)
        );

        AnsiConsole.Clear();

        bool success = false;
        do
        {
            string newStackName = AnsiConsole.Prompt(
                new TextPrompt<string>("Please enter the new name of the stack ([yellow]Q[/] to cancel):")
            );
            
            if (newStackName.Equals("Q", StringComparison.OrdinalIgnoreCase))
            {
                AnsiConsole.MarkupLine("[yellow]Edit cancelled, returning to main menu.[/]");
                return;
            }
            
            stackToEdit.Name = newStackName;

            try
            {
                _stackRepository.UpdateStack(stackToEdit);
                AnsiConsole.MarkupLine("[green]Stack successfully updated![/]");
                success = true;
            }
            catch (InvalidOperationException ex)
            {
                AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            }
            catch (Exception)
            {
                AnsiConsole.MarkupLine("[red]Something went wrong while updating the stack.[/]");
                success = true; // stop looping for unexpected errors
            }

        } while (!success);
    }

    
    public void DeleteStack() {}
}