using System.Diagnostics;
using Utils;

var solver = new Solver();
var file = await solver.client.GetPuzzle("1");
var input = file.Select(s => solver.client.TryParsePuzzleLine(s)).ToArray();

//
// Day One - Part 1
// Check total amount of depth changes from input
//

var depthChanges = 0;
int? lastMeasurement = null;

foreach (var (item, index) in input.Select((item, index) => (item, index)))
{
    if (index == 0) continue;

    lastMeasurement ??= item;

    if (lastMeasurement < item) depthChanges++;

    lastMeasurement = item;
}

Console.WriteLine($"Answer is: {depthChanges}");

//
// Day One - Part 2
// Check total amount of depth changes using ranges
//

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

Console.WriteLine($"Answer is: {depthChanges}");
