using Spectre.Console;
using TicTacToe.Engine;

public interface IAgent
{
    GridPosition Move(GameState.InProgress inprogress);
}


public class Human : IAgent
{
    public GridPosition Move(GameState.InProgress inprogress)
    {
        var selectedMode = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Choose a [green]position[/]:")
            .PageSize(9)
            .AddChoices(inprogress.History.GetGrid().EmptyCells.Select(x => x.ToString())));

        return Enum.Parse<GridPosition>(selectedMode);
    }
}

public class RandomAI : IAgent
{

    public GridPosition Move(GameState.InProgress inprogress)
    {
        var emptyCells = inprogress.History.GetGrid().EmptyCells.ToArray();
        var selected = Random.Shared.Next(emptyCells.Length);
        return emptyCells[selected];
    }
}



