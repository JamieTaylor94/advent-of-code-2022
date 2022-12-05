using Xunit;

namespace AdventOfCode;

public class DayThree
{
    private static readonly string[] Text = ResourceParser.SplitByLine(3);
    private static readonly RucksackOrganiser RucksackOrganiser = new RucksackOrganiser(Text);

    [Fact]
    public void RucksackReorgPartOne()
    {
        var result = RucksackOrganiser.TotalSharedItems();
        Assert.Equal(7701, result);
    }

    [Fact]
    public void RucksackReorgPartTwo()
    {
        var result = RucksackOrganiser.SumOfAuthGroups();
        Assert.Equal(2644, result);
    }
}

public class RucksackOrganiser
{
    private const string Letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private readonly string[] _ruckSacks;

    public RucksackOrganiser(string[] ruckSacks)
    {
        _ruckSacks = ruckSacks;
    }

    public int TotalSharedItems()
    {
        var ruckSacks = SeparateCompartments(_ruckSacks);
        return ruckSacks.Sum(SumOfSharedItemsByRucksack);
    }

    public int SumOfAuthGroups()
    {
        var total = 0;

        for (var i = 0; i < _ruckSacks.Length; i += 3)
        {
            var common = _ruckSacks[i]
                .Intersect(_ruckSacks[i + 1])
                .Intersect(_ruckSacks[i + 2]);
            
            total += CalculatePriority(common.First());
        }

        return total;
    }

    private int SumOfSharedItemsByRucksack(string[] ruckSack)
    {
        var firstCompartment = ruckSack[0];
        var secondCompartment = ruckSack[1];
        var common = firstCompartment.Intersect(secondCompartment);
        return CalculatePriority(common.First());
    }

    private IEnumerable<string[]> SeparateCompartments(string[] ruckSack)
    {
        return ruckSack.Select(r =>
        {
            var halfLength = r.Length / 2;
            return new[]
            {
                r.Substring(0, halfLength),
                r.Substring(halfLength, r.Length - halfLength)
            };
        });
    }

    private int CalculatePriority(char item)
    {
        return Letters.IndexOf(item) + 1;
    }
}