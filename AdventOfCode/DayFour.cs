using Xunit;

namespace AdventOfCode;

public class DayFour
{
    private static readonly string[] Text = ResourceParser.SplitByLine(4);
    [Fact]
    public void CampCleanupPartOne()
    {
        var assignmentCoordinator = new AssignmentCoordinator(Text);
        var result = assignmentCoordinator.AlreadyFullyCoveredCount();
        Assert.Equal(448, result);
    }
    
    [Fact]
    public void CampCleanupPartTwo()
    {
        var assignmentCoordinator = new AssignmentCoordinator(Text);
        var result = assignmentCoordinator.OverlappingPairsCount();
        Assert.Equal(794, result);
    }
}

public class AssignmentCoordinator
{
    private readonly IEnumerable<Pair> _pairs;

    public AssignmentCoordinator(string[] assignments)
    {
        _pairs = assignments.Select(a => new Pair(a.Split(',')));
    }
    
    public int AlreadyFullyCoveredCount()
    {
        return _pairs.Count(p => p.IsAlreadyCovered);
    }
        
    public int OverlappingPairsCount()
    {
        return _pairs.Count(p => p.IsOverlapping);
    }
}

public class Pair
{
    private readonly Assignment _assignmentOne;
    private readonly Assignment _assignmentTwo;
    
    public Pair(string[] pair)
    {
        var parsedPairs = pair
            .Select(p => p.Split('-').Select(int.Parse).ToList())
            .ToList();

        _assignmentOne = CreateAssignment(parsedPairs.First());
        _assignmentTwo = CreateAssignment(parsedPairs.Last());
    }

    public bool IsOverlapping => _assignmentOne.Start <= _assignmentTwo.End && _assignmentTwo.Start <= _assignmentOne.End;

    public bool IsAlreadyCovered
    {
        get
        {
            var firstCoversSecond =
                _assignmentOne.Start <= _assignmentTwo.Start && _assignmentOne.End >= _assignmentTwo.End;
            var secondCoversFirst =
                _assignmentTwo.Start <= _assignmentOne.Start && _assignmentTwo.End >= _assignmentOne.End;
            
            return firstCoversSecond || secondCoversFirst;
        }
    }
    
    private Assignment CreateAssignment(List<int> assignments) => new Assignment
    {
        Start = assignments.First(),
        End = assignments.Last()
    };
}

public class Assignment
{
    public int Start { get; set; }
    public int End { get; set; }
}