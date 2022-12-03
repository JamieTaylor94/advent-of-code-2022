using Xunit;

namespace AdventOfCode;

public class DayTwo
{
    private static readonly string[] Text = ResourceParser.SplitByLine(2);

    [Fact]
    public void RockPaperScissors_PartOne()
    {
        var result = RockPaperScissors.PlayPartOne(Text);
        Assert.Equal(11386, result);
    }
    
    [Fact]
    public void RockPaperScissors_PartTwo()
    {
        var result = RockPaperScissors.PlayPartTwo(Text);
        Assert.Equal(13600, result);
    }
}

public class RockPaperScissors
{
    private static readonly Dictionary<string, string> Moves = new()
    {
        { "A", "Rock" },
        { "B", "Paper" },
        { "C", "Scissors" },
        { "X", "Rock" },
        { "Y", "Paper" },
        { "Z", "Scissors" },
    };

    private static readonly Dictionary<string, int> RoundPoints = new()
    {
        { "Win", 6 },
        { "Draw", 3 },
        { "Lose", 0 }
    };

    private static readonly Dictionary<string, int> MovePoints = new()
    {
        { "Rock", 1 },
        { "Paper", 2 },
        { "Scissors", 3 }
    };

    private static readonly Dictionary<string, string> SuggestedStrategy = new()
    {
        { "X", "Lose" },
        { "Y", "Draw" },
        { "Z", "Win" }
    };

    public static int PlayPartOne(string[] game)
    {
        var score = 0;

        foreach (var round in game)
        {
            var players = round.Split(' ');
            var elfMove = Moves[players[0]];
            var myMove = Moves[players[1]];

            var roundResult = GetRoundResult(elfMove, myMove);
            var roundPoints = RoundPoints[roundResult];
            var movePoints = MovePoints[myMove];

            score += roundPoints + movePoints;
        }

        return score;
    }
    
    public static int PlayPartTwo(string[] game)
    {
        var score = 0;

        foreach (var round in game)
        {
            var players = round.Split(' ');
            var elfMove = Moves[players[0]];

            var strategy = SuggestedStrategy[players[1]];
            var myMove = FindMoveToPlay(elfMove, strategy);

            var roundResult = GetRoundResult(elfMove, myMove);
            var roundPoints = RoundPoints[roundResult];
            var movePoints = MovePoints[myMove];

            score += roundPoints + movePoints;
        }

        return score;
    }
    
    private static string GetRoundResult(string elfMove, string myMove)
    {
        if (elfMove == myMove) return "Draw";

        switch (elfMove)
        {
            case "Scissors" when myMove == "Paper":
            case "Paper" when myMove == "Rock":
            case "Rock" when myMove == "Scissors":
                return "Lose";
            default:
                return "Win";
        }
    }

    private static string FindMoveToPlay(string elfMove, string strategy)
    { 
        if (strategy == "Draw") return elfMove;

        return elfMove switch
        {
            "Rock" => strategy == "Win" ? "Paper" : "Scissors",
            "Paper" => strategy == "Win" ? "Scissors" : "Rock",
            "Scissors" => strategy == "Win" ? "Rock" : "Paper",
        };
    }
}