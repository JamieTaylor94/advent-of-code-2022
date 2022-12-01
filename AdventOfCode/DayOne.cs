using Xunit;

namespace AdventOfCode;

public class DayOne
{
    private static readonly IEnumerable<string> Text = ResourceParser.SplitByEmptyLine(1);
    
    [Fact]
    public void CalorieCounting_PartOne()
    {
        var maxCalories = GetCalorieCounts(Text).Max();

        Assert.Equal(74198, maxCalories);
    }
    
    [Fact]
    public void CalorieCounting_PartTwo()
    {
        var maxCalories = GetCalorieCounts(Text)
            .OrderByDescending(x => x)
            .Take(3)
            .Sum();

        Assert.Equal(209914, maxCalories);
    }

    private static IEnumerable<int> GetCalorieCounts(IEnumerable<string> inventories)
    {
        return inventories.Select(inventory => inventory.Split().Sum(int.Parse));
    }

}