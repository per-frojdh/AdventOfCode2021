using System.Diagnostics;
using Utils;

var stopWatch = new Stopwatch();

var solver = new Solver();
var file = await solver.client.GetPuzzle("1");
var input = file.Select(s => solver.client.TryParsePuzzleLine(s)).ToArray();

//
// Day One - Part 1
// Check total amount of depth changes from input
//

stopWatch.Start();
var depthChanges = 0;
int? lastMeasurement = null;

foreach (var (item, index) in input.Select((item, index) => (item, index)))
{
    if (index == 0) continue;

    lastMeasurement ??= item;

    if (lastMeasurement < item) depthChanges++;

    lastMeasurement = item;
}

stopWatch.Stop();
Console.WriteLine($"Answer is: {depthChanges}");
Console.WriteLine($"Solution took {stopWatch.ElapsedMilliseconds}ms");

//
// Day One - Part 2
// Check total amount of depth changes using ranges
//
stopWatch.Reset();
stopWatch.Start();

depthChanges = 0;
lastMeasurement = null;

foreach (var (item, index) in input.Select((item, index) => (item, index)))
{
    var windowEnd = index + 3;

    if (windowEnd > input.Length) break;

    var sum = input[index..windowEnd].Sum();

    lastMeasurement ??= sum;

    if (lastMeasurement < sum) depthChanges++;

    lastMeasurement = sum;
}

stopWatch.Stop();
Console.WriteLine($"Answer is: {depthChanges}");
Console.WriteLine($"Solution took {stopWatch.ElapsedMilliseconds}ms");