using Utils;

var solver = new Solver();
var file = await solver.client.GetPuzzle("3");

var lineLength = file.First().Length;

var verticalSlice = Enumerable.Range(0, lineLength)
    .Select(inputLength => file
        .Select(line => line[inputLength]))
    .ToList();

var sliceOrderedByOccurrence = verticalSlice
    .Select((vertical, indexInString) => {
        return new {
            indexInString,
            occurrences = vertical
                .GroupBy(line => line)
                .Select(group => new {
                    i = group.Key.ToString(),
                    count = group.Count()
                })
                .OrderByDescending(i => i.count)
                .ToArray()
        };
    }).ToArray();

var gammaRate = Convert.ToInt32(string.Join("", 
    sliceOrderedByOccurrence
        .Select(x => x.occurrences.First().i)
    ), 2);
    
var epsilonRate = Convert.ToInt32(string.Join("", 
    sliceOrderedByOccurrence
        .Select(x => x.occurrences.Last().i)
), 2);

Console.WriteLine(gammaRate * epsilonRate);