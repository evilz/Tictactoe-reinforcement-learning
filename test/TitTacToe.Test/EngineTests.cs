using TicTacToe.Engine;

namespace TitTacToe.Test;

public class EngineTests
{
    [Fact]
    public void Test_DefaultGame()
    {
        var game = new TicTacToe.Engine.TicTacToe();
        Assert.IsAssignableFrom<GameState.InProgress>(game.State);
        var inprogress = (GameState.InProgress)game.State;
        Assert.Empty(inprogress.History);
        Assert.Equal(Players.X, inprogress.CurrentPlayer);
    }

    [Fact]
    public void Test_PlayerX_Win_Game()
    {
        var game = new TicTacToe.Engine.TicTacToe();
        game.Play(GridPosition.BottomLeft);
        game.Play(GridPosition.BottomCenter);
        game.Play(GridPosition.Middle);
        game.Play(GridPosition.TopLeft);
        game.Play(GridPosition.TopRight);

        Assert.IsAssignableFrom<GameState.Finished>(game.State);
        var finished = (GameState.Finished)game.State;
        Assert.Equal(new[]{GridPosition.BottomLeft,GridPosition.BottomCenter,GridPosition.Middle,
        GridPosition.TopLeft,GridPosition.TopRight}, finished.History);
        Assert.IsAssignableFrom<GameOutcome.Won>(finished.Outcome);
        var outcome = (GameOutcome.Won)finished.Outcome;
        Assert.Equal(Players.X, outcome.By);
    }

    [Fact]
    public void Test_MoveError_PositionAlreadyUsed()
    {
        var game = new TicTacToe.Engine.TicTacToe();
        game.Play(GridPosition.BottomLeft);
        var validation = game.Play(GridPosition.BottomLeft);
        Assert.NotNull(validation);
        Assert.Equal(MoveError.PositionAlreadyUsed, validation);
    }

    [Fact]
    public void Test_Tie_Game()
    {

        var game = new TicTacToe.Engine.TicTacToe();
        game.Play(GridPosition.BottomLeft);
        game.Play(GridPosition.BottomCenter);
        game.Play(GridPosition.BottomRight);
        game.Play(GridPosition.CenterLeft);
        game.Play(GridPosition.CenterRight);
        game.Play(GridPosition.TopRight);
        game.Play(GridPosition.TopLeft);
        game.Play(GridPosition.Middle);
        game.Play(GridPosition.TopCenter);

        Assert.IsAssignableFrom<GameState.Finished>(game.State);
        var finished = (GameState.Finished)game.State;
        Assert.IsAssignableFrom<GameOutcome.Tie>(finished.Outcome);

        var validation = game.Play(GridPosition.BottomLeft);
        Assert.NotNull(validation);
        Assert.Equal(MoveError.AlreadyFinished, validation);
    }
}